using System.Net;

namespace Lunia.V4.Helpers;

internal sealed class RequestParser(IHttpContextAccessor httpContextAccessor)
{
    public Request Request
    {
        get
        {
            var context = httpContextAccessor.HttpContext;
            if (context is null)
            {
                throw new InvalidOperationException();
            }

            var serverName = context.Request.Headers["REQUESTER"].ToString();
            if (string.IsNullOrEmpty(serverName))
            {
                serverName = "(empty)";
            }

            return new Request(serverName, ParseParameters(context.Request.QueryString.Value));
        }
    }

    private static RequestParameters ParseParameters(string? queryString)
    {
        queryString ??= string.Empty;
        if (queryString.StartsWith('?'))
        {
            queryString = queryString[1..];
        }

        if (string.IsNullOrEmpty(queryString))
        {
            return new RequestParameters([]);
        }

        var parameterData = WebUtility.UrlDecode(queryString);
        var parameters = parameterData.Split('\b');

        return new RequestParameters(parameters);
    }
}