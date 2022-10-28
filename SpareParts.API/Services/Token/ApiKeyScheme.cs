using Microsoft.OpenApi.Models;

namespace SpareParts.API.Services.Token;

internal class ApiKeyScheme : OpenApiSecurityScheme
{
    public new string In { get; set; } = String.Empty;
    public new string Description { get; set; } = String.Empty;
    public new string Name { get; set; } = String.Empty;
    public new string Type { get; set; } = String.Empty;
}