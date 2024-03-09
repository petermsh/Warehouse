using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using TaskApp.Models;

namespace TaskApp.Common;

/// <summary>
/// Klasa odpowiedzialna za konwersję tekstu na wartość typu int
/// </summary>
internal sealed class TypeConverter : TypeConverter<int>
{
    public override int ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        return int.TryParse(text, out int vatRate) ? vatRate : 0;
    }

    public override string ConvertToString(int value, IWriterRow row, MemberMapData memberMapData)
    {
        return value.ToString();
    }
}