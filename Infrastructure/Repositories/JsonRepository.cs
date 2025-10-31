using System.Text.Json;

namespace Infrastructure.Repositories;

public class JsonRepository
{
    private readonly string _filePath;
    private readonly string _dataDirectory;
    private static JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public JsonRepository(string dataDirectory, string fileName)
    {
        _dataDirectory = dataDirectory;
        _filePath = Path.Combine(dataDirectory, fileName);
    }

    public static void EnsureInitialized(string dataDirectory, string filePath)
    {
        if (!Directory.Exists(dataDirectory))
            Directory.CreateDirectory(dataDirectory);


        if (!File.Exists(filePath))
            // Om inte filen products.json finns på den angivna filsökvägen, skapa den och skriv in en tom array i json-format (motsvarar en tom lista när den läses in)
            File.WriteAllText(filePath, "[]");

        // Om filen finns men är tom/whitespace, initiera den till en tom array
        string existing = File.ReadAllText(filePath);
        if (string.IsNullOrWhiteSpace(existing))
            File.WriteAllText(filePath, "[]");
    }

}