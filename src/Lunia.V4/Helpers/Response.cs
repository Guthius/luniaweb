using System.Text;
using LanguageExt;
using LanguageExt.Common;

namespace Lunia.V4.Helpers;

internal static class Response
{
    private const string ContentType = "text/plain";

    private static readonly Encoding ContentEncoding = Encoding.UTF8;

    public static IResult Ok(string content)
    {
        return TypedResults.Text(
            content,
            contentType: ContentType,
            contentEncoding: ContentEncoding,
            statusCode: 200);
    }

    public static IResult Ok() => Ok("Ok");

    public static IResult Error(Error error)
    {
        return TypedResults.Text(
            "ERROR NUMBER :" + error.Code,
            contentType: ContentType,
            contentEncoding: ContentEncoding,
            statusCode: 600 + error.Code);
    }

    public static IResult Error(Seq<Error> error) => Error(error.Head);

    public static IResult Error(string errorMessage)
    {
        return TypedResults.Text(
            "ERROR :" + errorMessage,
            contentType: ContentType,
            contentEncoding: ContentEncoding,
            statusCode: 699);
    }
}