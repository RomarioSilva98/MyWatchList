// Helpers/PasswordHelper.cs
using Microsoft.AspNetCore.Identity;
using MyWatchList.Models;

namespace MyWatchList.Helpers;

public static class PasswordHelper
{
    private static readonly PasswordHasher<Usuario> _hasher = new();

    public static string HashPassword(Usuario usuario, string senha)
        => _hasher.HashPassword(usuario, senha);

    public static bool VerifyPassword(Usuario usuario, string senhaDigitada, string hashNoBanco)
    {
        var result = _hasher.VerifyHashedPassword(usuario, hashNoBanco, senhaDigitada);
        return result == PasswordVerificationResult.Success
            || result == PasswordVerificationResult.SuccessRehashNeeded;
    }

    // Heurística segura o bastante p/ Identity
    public static bool LooksHashed(string? value) => !string.IsNullOrEmpty(value) && value.StartsWith("AQAAAA");
}
