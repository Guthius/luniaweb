namespace Lunia.V4.Helpers;

internal sealed record Request(string ServerName, RequestParameters Parameters)
{
    public string GetString(int index, string defaultValue = "")
    {
        var result = Parameters[index];
        if (!string.IsNullOrEmpty(result))
        {
            return result;
        }
        
        return defaultValue;
    }

    public bool TryGetInt32(int index, out int value)
    {
        return int.TryParse(Parameters[index], out value);
    }
}