using System.Text.RegularExpressions;

namespace Korexio.Utilities;

/// <summary>
/// Provides extension methods for the <see cref="string"/> class.
/// </summary>
public static class StringExtensionMethods
{
    /// <summary>
    /// Splits the specified string into lines.
    /// </summary>
    /// <param name="value">The string value to split into lines.</param>
    /// <returns>An array of strings.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
    /// <remarks>
    /// The following characters are treated as line separators:
    /// <list type="table">
    /// <item><term><c>U+000A</c>, <c>'\n'</c></term> <description>Line Feed (LF)</description></item>
    /// <item><term><c>U+000C</c>, <c>'\f'</c></term> <description>Form Feed (FF)</description></item>
    /// <item><term><c>U+000D</c>, <c>'\r'</c></term> <description>Carriage Return (CR)</description></item>
    /// <item><term><c>U+0085</c>, <c>'\u0085'</c></term> <description>Next Line (NEL)</description></item>
    /// <item><term><c>U+2028</c>, <c>'\u2028'</c></term> <description>Line Separator (LSEP)</description></item>
    /// <item><term><c>U+2029</c>, <c>'\u2029'</c></term> <description>Paragraph Separator (PSEP)</description></item>
    /// </list>
    /// The character sequences <c>U+000D</c> <c>U+000A</c> (<c>"\r\n"</c>) and <c>U+000A</c> <c>U+000D</c> (<c>"\n\r"</c>)
    /// are treated as a single line separator instead of two individual line separators.
    /// </remarks>
    /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/#string-escape-sequences">String Escape Sequences</seealso>
    public static string[] ToLines(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return Regex.Split(value, @"\r\n|\n\r|[\n\f\r\u0085\u2028\u2029]");
    }
}
