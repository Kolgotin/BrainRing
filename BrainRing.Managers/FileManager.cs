using BrainRing.Core.Game;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BrainRing.Managers;

public static class FileManager
{
    private static readonly JsonSerializerOptions Options;

    static FileManager()
    {
        Options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }

    public static async Task<bool> WriteFileManager(Pack pack, string filePath)
    {
        try
        {
            var jsonString = JsonSerializer.Serialize(pack, Options);
            await using var sw = new StreamWriter(filePath);
            await sw.WriteAsync(jsonString);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

        return true;
    }

    public static async Task<Pack?> ReadPackFile(string filePath)
    {
        using var sr = new StreamReader(filePath);
        var jsonString = await sr.ReadToEndAsync();
        var pack = JsonSerializer.Deserialize<Pack>(jsonString);
        return pack;
    }
}