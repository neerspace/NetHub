using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using static NetHub.Tests.TestingEnvironmentFactory;

namespace NetHub.Tests;

public static class TestingEnvironment
{
	private static IConfiguration? _cachedConfiguration;
	private static IWebHostEnvironment? _cachedEnvironment;
	private static IServiceProvider? _cachedServiceProvider;

	static TestingEnvironment()
	{
		Directory.SetCurrentDirectory("../../../../NetHub.Api");
	}

	public static IServiceProvider ServiceProvider => _cachedServiceProvider ??= BuildServiceProvider(Configuration, Environment);
	public static IConfiguration Configuration => _cachedConfiguration ??= BuildConfiguration();
	public static IWebHostEnvironment Environment => _cachedEnvironment ??= BuildEnvironment();
}