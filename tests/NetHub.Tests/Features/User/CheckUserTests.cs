using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Features.Public.Users.CheckUserExists;
using NetHub.Application.Features.Public.Users.CheckUsername;
using NetHub.Application.Features.Public.Users.Sso;
using NetHub.Core.Abstractions.Context;
using NetHub.Tests.Seed;
using Xunit;
using Xunit.Abstractions;

namespace NetHub.Tests.Features.User;

public class CheckUserTests
{
    // private readonly ITestOutputHelper _testOutputHelper;
    // private readonly IMediator _mediator;
    //
    // private IDatabaseContext Context =>
    // 	TestingEnvironment.ServiceProvider.GetRequiredService<IDatabaseContext>();
    //
    // public CheckUserTests(ITestOutputHelper testOutputHelper)
    // {
    // 	_testOutputHelper = testOutputHelper;
    // 	_mediator = TestingEnvironment.ServiceProvider.GetRequiredService<IMediator>();
    // }
    //
    // #region FirstCase
    //
    // [Fact]
    // public async Task Google_Exists_Telegram_Does_Not()
    // {
    // 	Context.Set<IdentityUserLogin<long>>().AddRange(SeedLogins.FirstCase);
    //
    // 	var query = new CheckUserExistsRequest(SeedLogins.TelegramId123, ProviderType.Telegram, SeedLogins.GeekMail);
    // 	var result = await _mediator.Send(query);
    //
    // 	_testOutputHelper.WriteLine(result.ToString());
    // 	foreach (var provider in result.Providers)
    // 		_testOutputHelper.WriteLine(provider);
    //
    // 	Assert.True(result.IsExists);
    // 	// Assert.Contains(result.Providers, provider => provider == "google");
    // 	// Assert.DoesNotContain(result.Providers, provider => provider == "telegram");
    // }
    //
    // #endregion
    //
    // #region SecondCase
    //
    // [Fact]
    // public async Task Telegram_Exists_Google_Doesnt()
    // {
    // 	Context.Set<IdentityUserLogin<long>>().AddRange(SeedLogins.SecondCase);
    //
    // 	var query = new CheckUserExistsRequest(SeedLogins.GeekMail, ProviderType.Google, SeedLogins.GeekMail);
    // 	var result = await _mediator.Send(query);
    //
    // 	_testOutputHelper.WriteLine(result.ToString());
    // 	foreach (var provider in result.Providers)
    // 		_testOutputHelper.WriteLine(provider);
    //
    // 	Assert.True(result.IsExists);
    // }
    //
    // #endregion
    //
    // #region ThirdCase
    //
    // [Fact]
    // public async Task One_Telegram_Exists_Second_Doesnt()
    // {
    // 	Context.Set<IdentityUserLogin<long>>().AddRange(SeedLogins.ThirdCase);
    // 	
    // 	var query = new CheckUserExistsRequest()
    // }
    //
    // #endregion
    //
    //
    // [Fact]
    // public async Task Check_If_Username_Is_Free()
    // {
    // 	var query = new CheckUsernameRequest("tweeker");
    // 	var result = await _mediator.Send(query);
    //
    // 	Assert.False(result.IsAvailable);
    // }
}