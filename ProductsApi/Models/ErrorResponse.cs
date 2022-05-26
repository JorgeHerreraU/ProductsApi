using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace ProductsApi.Models;

[NotMapped]
public class ErrorResponse
{
    public string Message { get; set; } = "An error has occurred.";
    public int StatusCode { get; set; } = 500;
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

}
