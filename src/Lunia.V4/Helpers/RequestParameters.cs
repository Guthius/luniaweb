namespace Lunia.V4.Helpers;

internal sealed class RequestParameters(string[] values)
{
    public int Count => values.Length;
    
    public string this[int index]
    {
        get
        {
            if (index < 0 || index >= values.Length)
            {
                return string.Empty;
            }

            return values[index];
        }
    }
}