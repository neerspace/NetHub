using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace NetHub.Tests;

public class TelegramHmacTest
{
	private const string BotToken = "123:ABC";
	private const string ExpectedHash = "blablabla";
	
	[Fact]
	public void HmacTest()
	{
		var info = new Dictionary<string, string>
		{
			{"auth_date", "1234567"},
			{"first_name", "abc"},
			{"id", "123456"},
			{"photo_url", "https://t.me/i/userpic/...jpg"},
			{"username", "usr"},
		};

		var dataString = CombineString(info);
		var computedHash = HashHMAC(dataString);

		Assert.Equal(ExpectedHash, computedHash.ToLower());
	}

	private string HashHMAC(string message)
	{
		using var hasher = SHA256.Create();
		var keyBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(BotToken));

		var messageBytes = Encoding.UTF8.GetBytes(message);
		var hash = new HMACSHA256(keyBytes);
		var computedHash = hash.ComputeHash(messageBytes);
		return Convert.ToHexString(computedHash);
	}

	private string CombineString(IReadOnlyDictionary<string, string> meta)
	{
		var builder = new StringBuilder();

		TryAppend("auth_date");
		TryAppend("first_name");
		TryAppend("id");
		TryAppend("last_name");
		TryAppend("photo_url");
		TryAppend("username", true);

		return builder.ToString();

		void TryAppend(string key, bool isLast = false)
		{
			if (meta.ContainsKey(key))
				builder.Append($"{key}={meta[key]}{(isLast ? "" : "\n")}");
		}
	}
}