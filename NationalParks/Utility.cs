using System.Runtime.CompilerServices;
using System.Text;

namespace NationalParks;

public static class Utility
{
    public static string ParseException(Exception ex)
    {
        var sb = new StringBuilder();
        sb.Append($"{ex.Source}--{ex.Message}");

        var iex = ex.InnerException;
        while (iex != null)
        {
            sb.Append($"\n{iex.Source}--{iex.Message}");
            iex = iex.InnerException;
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string GetCurrentMethod()
    {
        var st = new StackTrace();
        var sf = st.GetFrame(1);

        return sf.GetMethod().Name;
    }
}
