﻿using NetHub.Core.Enums;

namespace NetHub.Admin.Infrastructure.Models.Jwt;

public sealed record AuthVerificationResult(
    bool UserExists,
    string? UserName,
    AuthMethod PreferAuthMethod,
    IEnumerable<AuthMethod> AllowedAuthMethod
);