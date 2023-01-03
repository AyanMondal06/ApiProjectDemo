using System.Text.Json.Serialization;

namespace ApiProject.web.Insfrastructure
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AppRoles
    {
        Admin=1,
        Company=2
    }
}
