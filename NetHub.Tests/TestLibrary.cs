using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Telegram.Bot.Extensions.LoginWidget;
using Xunit;
using Xunit.Abstractions;

namespace NetHub.Tests;

public class TestLibrary
{
	private static readonly DateTime _unixStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	public long AllowedTimeOffset = 3600;
	private readonly HMACSHA256 _hmac;

	private readonly ITestOutputHelper _testOutputHelper;

	public TestLibrary(ITestOutputHelper testOutputHelper)
	{
		// using (SHA256 shA256 = SHA256.Create())
		// this._hmac = new HMACSHA256(shA256.ComputeHash(Encoding.ASCII.GetBytes(TOKEN)));
		// _testOutputHelper = testOutputHelper;
	}

	[Fact]
	public void TestLib()
	{
		var BotToken = "123:ABC";
		var info = new Dictionary<string, string>
		{
			{"auth_date", "1234567"},
			{"first_name", "abc"},
			{"id", "123456"},
			{"photo_url", "https://t.me/i/userpic/...jpg"},
			{"username", "usr"},
		};

		var loginWidget = new LoginWidget(BotToken);
		if (loginWidget.CheckAuthorization(info) == Authorization.Valid)
		{
			// user valid
		}
	}

	public Authorization CheckAuthorization(SortedDictionary<string, string> fields)
	{
		string s;
		string str;
		if (fields.Count < 3 || !fields.ContainsKey("id") || !fields.TryGetValue("auth_date", out s) ||
		    !fields.TryGetValue("hash", out str))
			return Authorization.MissingFields;
		if (str.Length != 64)
			return Authorization.InvalidHash;
		long result;
		if (!long.TryParse(s, out result))
			return Authorization.InvalidAuthDateFormat;
		if (Math.Abs(DateTime.UtcNow.Subtract(_unixStart).TotalSeconds - (double) result) > (double) this.AllowedTimeOffset)
			return Authorization.TooOld;
		fields.Remove("hash");
		StringBuilder stringBuilder = new StringBuilder(256);
		foreach (KeyValuePair<string, string> field in fields)
		{
			stringBuilder.Append(field.Key);
			stringBuilder.Append('=');
			stringBuilder.Append(field.Value);
			stringBuilder.Append('\n');
		}

		--stringBuilder.Length;
		byte[] hash = _hmac.ComputeHash(Encoding.UTF8.GetBytes(stringBuilder.ToString()));
		for (int index = 0; index < hash.Length; ++index)
		{
			if ((int) str[index * 2] != 87 + ((int) hash[index] >> 4) + (((int) hash[index] >> 4) - 10 >> 31 & -39) ||
			    (int) str[index * 2 + 1] != 87 + ((int) hash[index] & 15) + (((int) hash[index] & 15) - 10 >> 31 & -39))
				return Authorization.InvalidHash;
		}

		return Authorization.Valid;
	}
}