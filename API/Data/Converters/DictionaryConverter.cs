using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
namespace API.Data.Converters;

public class DictionaryConverter<K, V> : ValueConverter<Dictionary<K, V>, string> where K : notnull
{
    public DictionaryConverter()
        : base(
            dictionary => Serialize(dictionary),
            json => Deserialize(json))
    {
    }

    private static string Serialize(Dictionary<K, V> dictionary)
    {
        return JsonSerializer.Serialize(dictionary);
    }

    private static Dictionary<K, V> Deserialize(string json)
    {
        var deserialized = JsonSerializer.Deserialize<Dictionary<K, V>>(json);
        return string.IsNullOrEmpty(json)
            ? new Dictionary<K, V>()
            : deserialized ?? new Dictionary<K, V>();
    }
}