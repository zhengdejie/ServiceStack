using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ServiceStack.Text;

namespace ServiceStack.ServiceHost.Tests
{
	public class RequestPath
	{
		public RequestPath(string path, string host, string pathInfo, string rawUrl)
		{
			Path = path;
			Host = host;
			PathInfo = pathInfo;
			RawUrl = rawUrl;
			AbsoluteUri = "http://localhost" + rawUrl;
		}

		public string Path { get; set; }
		public string Host { get; set; }
		public string PathInfo { get; set; }
		public string RawUrl { get; set; }
		public string AbsoluteUri { get; set; }
	}

	[TestFixture]
	public class ServiceStackHandlerPathTests
	{
		public string ResolvePath(string mode, string path)
		{
			return WebHost.Endpoints.Extensions.HttpRequestExtensions.
				GetPathInfo(path, mode);
		}

		[Test]
		public void Can_resolve_root_path()
		{
			var results = new List<string> {
				ResolvePath(null, "/handler.all35"),
				ResolvePath(null, "/handler.all35/"),
				ResolvePath("api", "/location.api.wildcard35/api"),
				ResolvePath("api", "/location.api.wildcard35/api/"),
				ResolvePath("servicestack", "/location.servicestack.wildcard35/servicestack"),
				ResolvePath("servicestack", "/location.servicestack.wildcard35/servicestack/"),
			};

			Console.WriteLine(results.Dump());

			Assert.That(results.All(x => x == "/"));
		}

		[Test]
		public void Can_resolve_metadata_paths()
		{
			var results = new List<string> {
				ResolvePath(null, "/handler.all35/metadata"),
				ResolvePath(null, "/handler.all35/metadata/"),
				ResolvePath("api", "/location.api.wildcard35/api/metadata"),
				ResolvePath("api", "/location.api.wildcard35/api/metadata/"),
				ResolvePath("servicestack", "/location.servicestack.wildcard35/servicestack/metadata"),
				ResolvePath("servicestack", "/location.servicestack.wildcard35/servicestack/metadata/"),
			};

			Console.WriteLine(results.Dump());

			Assert.That(results.All(x => x == "/metadata"));
		}

		[Test]
		public void Can_resolve_metadata_json_paths()
		{
			var results = new List<string> {
				ResolvePath(null, "/handler.all35/json/metadata"),
				ResolvePath(null, "/handler.all35/json/metadata/"),
				ResolvePath("api", "/location.api.wildcard35/api/json/metadata"),
				ResolvePath("api", "/location.api.wildcard35/api/json/metadata/"),
				ResolvePath("servicestack", "/location.api.wildcard35/servicestack/json/metadata"),
				ResolvePath("servicestack", "/location.api.wildcard35/servicestack/json/metadata/"),
			};

			Console.WriteLine(results.Dump());

			Assert.That(results.All(x => x == "/json/metadata"));
		}
	}

}
