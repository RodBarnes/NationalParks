namespace NationalParks;

public class CodeInfo
{
    public string ObjectName { get; set; }
    public string MethodName { get; set; }

    public CodeInfo(string objectName, string methodName)
    {
        ObjectName = objectName;
        MethodName = methodName;
    }

    public CodeInfo(Type typ)
    {
        // !!!!!
        // This expects the type to be the value from MethodBase.GetCurrentMethod().DeclaringType
        // Anything else will not work because it expects to extract values from the name of this type.
        // !!!!!

        var str = typ.ToString();

        var objectStart = str.Split('.').Last();
        var objectEnd = objectStart.IndexOf('+');
        ObjectName = objectStart[..objectEnd];

        var methodStart = str.Split('<').Last();
        var methodEnd = methodStart.IndexOf('>');
        MethodName = methodStart[..methodEnd];
    }
}
