using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using NetHub.Application.Features.Public.Users.Sso;
using NetHub.Application.Interfaces;
using NetHub.Application.Options;
using NetHub.Core.DependencyInjection;
using NetHub.Core.Exceptions;

namespace NetHub.Infrastructure.Services.Internal.AuthorizationProviders;

[Inject]
public class TelegramAuthProvider : IAuthProviderValidator
{
	public ProviderType Type => ProviderType.Telegram;

	private readonly TelegramOptions _telegramOptions;

	public TelegramAuthProvider(IOptions<TelegramOptions> telegramOptionsAccessor)
	{
		_telegramOptions = telegramOptionsAccessor.Value;
	}


	public Task<bool> ValidateAsync(SsoEnterRequest request, SsoType type)
	{
		var metadata = request.ProviderMetadata;

		if (CheckDate(metadata["auth_date"]!))
			throw new ValidationFailedException("Data is outdated");

		var rawString = CombineString(metadata);
		var computedHash = HashHMAC(rawString);

		return Task.FromResult(string.Equals(metadata["hash"], computedHash, StringComparison.OrdinalIgnoreCase));
	}

	private static bool CheckDate(string epochSeconds)
	{
		return DateTime.UtcNow - DateTimeOffset.FromUnixTimeSeconds(int.Parse(epochSeconds)) >
		       TimeSpan.FromHours(2);
	}

	private string CombineString(IReadOnlyDictionary<string, string?> meta)
	{
		var orderedMeta = meta.OrderBy(p => p.Key)
			.Where(p => p.Key != "hash" && !string.IsNullOrEmpty(p.Value)).ToDictionary(pair => pair.Key, pair => pair.Value);

		var builder = new StringBuilder();

		for (var i = 0; i < orderedMeta.Count; i++)
		{
			TryAppend(orderedMeta.ElementAt(i).Key, string.IsNullOrEmpty(orderedMeta.ElementAtOrDefault(i + 1).Value));
		}


		// TryAppend("auth_date");
		// TryAppend("first_name");
		// TryAppend("id");
		// TryAppend("last_name");
		// TryAppend("photo_url");
		// TryAppend("username", true);

		return builder.ToString();

		void TryAppend(string key, bool isLast = false)
		{
			if (meta.ContainsKey(key) && !string.IsNullOrWhiteSpace(meta.GetValueOrDefault(key)))
				builder.Append($"{key}={meta[key]}{(isLast ? "" : "\n")}");
		}
	}

	private string HashHMAC(string message)
	{
		using var hasher = SHA256.Create();
		hasher.ComputeHash(Encoding.UTF8.GetBytes(_telegramOptions.BotToken));
		var keyBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(_telegramOptions.BotToken));

		var messageBytes = Encoding.UTF8.GetBytes(message);
		var hash = new HMACSHA256(keyBytes);
		var computedHash = hash.ComputeHash(messageBytes);
		return Convert.ToHexString(computedHash);
	}
}