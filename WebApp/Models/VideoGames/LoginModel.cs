using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class LoginModel
{
    [Microsoft.Build.Framework.Required]
    public string Username { get; set; }

    [Microsoft.Build.Framework.Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}