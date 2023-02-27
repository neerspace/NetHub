using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Features.Public.Users.Sso;
using NetHub.Core.Abstractions.Context;
using Xunit;
using Xunit.Abstractions;

namespace NetHub.Tests.Features.User;

public class SsoTests
{
    private readonly IMediator _mediator;

    private readonly ITestOutputHelper _testOutputHelper;

    public SsoTests(ITestOutputHelper testOutputHelper)
    {
        _mediator = TestingEnvironment.ServiceProvider.GetRequiredService<IMediator>();
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task SsoRegisterWithoutData()
    {
        var query = new SsoEnterRequest
        {
        };
        var result = await _mediator.Send(query);
        _testOutputHelper.WriteLine(result.Item1.Username);
    }
}