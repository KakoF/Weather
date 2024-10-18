using Infrastructure.Configs;
using Microsoft.Extensions.Options;
using System.Web;

namespace Infrastructure.Clients.Handlers
{
	public class RefitApiKeyHandler : DelegatingHandler
	{
		private readonly string _appId;
		public RefitApiKeyHandler(IOptions<WeatherConfig> config)
		{
			_appId = config.Value.Key;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var uri = new UriBuilder(request.RequestUri!);
			var query = HttpUtility.ParseQueryString(uri.Query);
			query["appid"] = _appId;
			uri.Query = query.ToString();
			request.RequestUri = uri.Uri;

			return await base.SendAsync(request, cancellationToken);
		}
	}
}
public class RefitApiKeyHandler : DelegatingHandler
{
	private readonly string _appId;
	public RefitApiKeyHandler(IOptions<WeatherConfig> config)
	{
		_appId = config.Value.Key;
	}

	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		var uri = new UriBuilder(request.RequestUri!);
		var query = HttpUtility.ParseQueryString(uri.Query);
		query["appid"] = _appId;
		uri.Query = query.ToString();
		request.RequestUri = uri.Uri;

		return await base.SendAsync(request, cancellationToken);
	}
}
