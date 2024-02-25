﻿using System.Text.Json.Serialization;

namespace Ecommerce.Shared.Auth;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;

    [JsonIgnore]
    public string? RefreshToken { get; set; } = string.Empty;
}
