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

    public static bool WriteFileManager(Pack pack, string filePath)
    {
        try
        {
            var jsonString = JsonSerializer.Serialize(pack, Options);
            using var sw = new StreamWriter(filePath);
            sw.Write(jsonString);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

        return true;
    }

    public static Pack? ReadPackFile(string filePath)
    {
        using var sr = new StreamReader(filePath);
        var jsonString = sr.ReadToEnd();
        var pack = JsonSerializer.Deserialize<Pack>(jsonString);
        return pack;
    }
}