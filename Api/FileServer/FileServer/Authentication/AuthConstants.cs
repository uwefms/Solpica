namespace FileServer.Authentication
{
	public static class AuthConstants
	{
		// Api Key using header  
		public const string ApiKeyHeaderSectionName = "Authentication:ApiHeaderKey";
		// to be used in header as X-Api-Key: xxxx
		public const string ApiKeyHeaderName = "X-Api-Key";

		// Api Key using query string
		public const string ApiKeyQuerySectionName = "Authentication:ApiQueryKey";
		// to be used in URL query string as ?ApiKey=xxxx
		public const string ApiKeyQueryName = "ApiKey";
	}
}
