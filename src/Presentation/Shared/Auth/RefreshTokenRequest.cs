﻿using System.Text.Json.Serialization;

namespace Ecommerce.Shared.User
{
    public class RefreshTokenRequest
    {
        [JsonIgnore]
        public string? RefreshToken { get; set; } = string.Empty;

        public string CurrentToken { get; set; } = string.Empty;
    }
}
