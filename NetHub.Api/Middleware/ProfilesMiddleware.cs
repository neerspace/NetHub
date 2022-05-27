using Microsoft.EntityFrameworkCore;
using NetHub.Application.Extensions;
using NetHub.Core.Abstractions.Context;
using NetHub.Data.SqlServer.Entities;
using NLog;
using ILogger = NLog.ILogger;

namespace NetHub.Api.Middleware;

public class ProfilesMiddleware : IMiddleware
{
	private readonly ILogger _logger;
	private readonly IDatabaseContext _database;

	public ProfilesMiddleware(IDatabaseContext database)
	{
		_database = database;
		_logger = LogManager.GetCurrentClassLogger();
	}

	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		if (context.User.Identity is not {IsAuthenticated: true})
		{
			await next(context);
			return;
		}
		
		var user = context.User;

		var mail = user.GetEmail();
		var profile = await _database.Set<UserProfile>().FirstOrDefaultAsync(u => u.Email == mail);

		if (profile is null)
		{
			_logger.Info("User with {UserName} not registered", user.GetUsername());

			profile = new()
			{
				UserId = user.GetGlobalUserId(),
				UserName = user.GetUsername(),
				NormalizedUserName = user.GetUsername().ToUpper(),
				Email = user.GetEmail(),
				NormalizedEmail = user.GetEmail().ToUpper(),
				Registered = DateTime.Parse(user.GetRegistered()),
				PhoneNumber = user.GetPhoneNumber()
			};

			await _database.Set<UserProfile>().AddAsync(profile);

			await _database.SaveChangesAsync();
		}

		context.Items.Add("User", profile);

		await next(context);
	}
}