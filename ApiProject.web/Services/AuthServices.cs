using ApiProject.web.Insfrastructure;
using ApiProject.web.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiProject.web.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly DbInfo _dbInfo;

        public readonly IConfiguration _config;

        public AuthServices(DbInfo dbInfo, IConfiguration config)
        {
            _dbInfo = dbInfo;
            _config = config;
        }


        public async Task<ServiceResponse<string>> Login(string CompanyName, string Password)
        {
            var response = new ServiceResponse<string>();
            var Company = await _dbInfo.CompanyTable.FirstOrDefaultAsync(u => u.CompanyName.ToLower() == CompanyName.ToLower());
            //User name auth validation
            if (Company == null)
            {
                response.Success = false;
                response.Message = "Company not found";
            }
            //Password auth 
            else if (!VerifyPasswordHash(Password, Company.PasswordHash, Company.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong Password";
            }
            else
            {
                response.Message = "Logged In";
                response.Data = CreateToken(Company);
            }

            return response;
        }

        public async Task<ServiceResponse<int>> Register(Company Company, string Password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            if (await UserAlreadyExist(Company.CompanyName))
            {
                response.Success = false;
                response.Message = "User Already Exists";
                return response;
            }
            CreatePasswordHash(Password, out byte[] PasswordHash, out byte[] PasswordSalt);

            Company.PasswordSalt = PasswordSalt;
            Company.PasswordHash = PasswordHash;

            _dbInfo.CompanyTable.Add(Company);
            await _dbInfo.SaveChangesAsync();

            response.Data = Company.Id;
            return response;
        }

        public async Task<bool> UserAlreadyExist(string CompanyName)
        {
            if (await _dbInfo.CompanyTable.AnyAsync(u => u.CompanyName.ToLower() == CompanyName.ToLower()))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }
        private bool VerifyPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(PasswordSalt))
            {
                var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                return (ComputeHash.SequenceEqual(PasswordHash));
            }
        }
       
        private string CreateToken(Company company)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,company.Id.ToString()),
                new Claim(ClaimTypes.Name,company.CompanyName),
                new Claim(ClaimTypes.Role,company.Role.ToString())
            };
            SymmetricSecurityKey Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            SigningCredentials Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = (DateTime.Now.AddMinutes(1)),
                SigningCredentials = Creds
            };
            
            JwtSecurityTokenHandler tokenHandler=new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
