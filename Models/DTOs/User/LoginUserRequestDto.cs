using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace TriviaGame.App.Models.DTOs.User
{
   public class LoginUserRequestDto
{
    [JsonPropertyName("gmail")]
    public string Gmail { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
}