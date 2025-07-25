using System.Text;

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

    private const char DelimField = (char) 8;
    private const char DelimSubField = (char) 11;

    private readonly StringBuilder _stringBuilder = new();

    public void Write<T>(T value)
    {
        if (_stringBuilder.Length > 0)
        {
            _stringBuilder.Append(DelimField);
        }

        _stringBuilder.Append(value);
    }

    public void WriteDateTime(DateTimeOffset? dateTime)
    {
        dateTime ??= DateTimeOffset.MinValue;

        Write(dateTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
    }

    public void Write(Action<FieldBuilder> builder)
    {
        var fieldBuilder = new FieldBuilder();

        builder(fieldBuilder);

        Write(string.Join(DelimSubField, fieldBuilder.Values));
    }

    public IResult ToResponse()
    {
        return Response.Ok(_stringBuilder.ToString());
    }
}