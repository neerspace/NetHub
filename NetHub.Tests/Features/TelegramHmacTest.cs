using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace NetHub.Tests.Features;

public class TelegramHmacTest
{
	private const string BotToken = "5533270293:AAHgLpnuGBefId7MS6OYl_V7mWo2XnKxcCU";
	private const string ExpectedHash = "03186de82740cde521944fae676ecb0001d9938c0f93d43084e972e11ce23722";
	
	[Fact]
	public void HmacTest()
	{
		var info = new Dictionary<string, string>
		{
			{"auth_date", "1660607268"},
			{"first_name", "tweeker"},
			{"id", "302865773"},
			{"photo_url", "https://t.me/i/userpic/320/D_eRfoBP9C3le0jYp4bmyxRaWLyKSlti81O3gYBPzWM.jpg"},
			{"username", "vuradzu"},
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