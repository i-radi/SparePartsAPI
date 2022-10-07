using Microsoft.OpenApi.Models;

namespace SpareParts.API.Helpers;

internal class ApiKeyScheme : OpenApiSecurityScheme
{
    public new string In { get; set; }
    public new string Description { get; set; }
    public new string Name { get; set; }
    public new string Type { get; set; }
}