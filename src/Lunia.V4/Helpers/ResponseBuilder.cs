using System.Net;

namespace Lunia.V4.Helpers;

internal sealed class ResponseBuilder
{
    internal sealed class FieldBuilder
    {
        public List<string> Values { get; } = [];

        public void Write<T>(T value) where T : notnull
        {
            Values.Add(value.ToString() ?? string.Empty);
        }
    }

    private const string DateFormat = "yyyy-MM-dd HH:mm:ss";
    private const char DelimField = (char) 8;
    private const char DelimSubField = (char) 11;

    private readonly List<string> _values = [];

    public void Write<T>(T value) where T : notnull
    {
        _values.Add(value.ToString() ?? string.Empty);
    }

    public void WriteDateTime(DateTimeOffset? dateTime)
    {
        dateTime ??= DateTimeOffset.MinValue;

        Write(dateTime.Value.ToString(DateFormat));
    }

    public void Write(Action<FieldBuilder> builder)
    {
        var fieldBuilder = new FieldBuilder();

        builder(fieldBuilder);

        Write(string.Join(DelimSubField, fieldBuilder.Values));
    }

    public IResult ToResponse()
    {
        var values = string.Join(DelimField, _values);
        var valuesEncoded = WebUtility.UrlEncode(values);
        
        return Response.Ok(valuesEncoded);
    }
}