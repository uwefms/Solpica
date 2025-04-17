using FileServer.Authentication;

namespace FileServer
{

	/// <summary>
	/// Custom middleware for API key authentication. API keys can be used in Query string or in header
    /// Keys can be found in appsettings.json file
	/// </summary>
	public class CustomApiKeyMiddleware
    {
        private readonly IConfiguration Configuration;
        private readonly RequestDelegate _next;
        

        public CustomApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            Configuration = configuration;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            bool lCheckQueryKey     = false;
            string? apiKeyFromHttp   = string.Empty; // common Api key from either header or query string

			// Check for API key in headers
			bool success = httpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var apiKeyFromHttpHeader);

			// if found in headers, then use it
			apiKeyFromHttp = apiKeyFromHttpHeader;

			// If not found in headers, check the query string
			if (!success)
            {
                success = httpContext.Request.Query.TryGetValue(AuthConstants.ApiKeyQueryName, out var apiKeyFromQuery);

                if (success)
                {
                    lCheckQueryKey          = true;					

					// if found in query string, then use it
					apiKeyFromHttp = apiKeyFromQuery;
				}
            }

			// if no key is found in either header or query string then return unauthorized
			if (!success)
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync("The Api Key for accessing this endpoint is not available");
                return;
            }

			// get the cheader key from appsettings.json first to avoid else condition in following if block
			var apiKey = Configuration.GetValue<string>(AuthConstants.ApiKeyHeaderSectionName);

			// if there is a query key to check - assign the query key to apiKey
			if (lCheckQueryKey) 
			{
				apiKey = Configuration.GetValue<string>(AuthConstants.ApiKeyQuerySectionName);
			}

			// if both keys are not equal to the common key then return unauthorized
			if (!apiKey!.Equals(apiKeyFromHttp))
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync("The authentication key is incorrect: Unauthorized access");
                return;
            }

			// Call the next delegate/middleware in the pipeline when the key is correct
			await _next(httpContext);
        }

	} // end of class

} // end of namespace
