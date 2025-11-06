using System.Text.Json;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Repositories;

public class JsonRepository : IJsonRepository
{
    private readonly string _filePath;
    private readonly string _dataDirectory;
    private static JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public JsonRepository()
    {
        _dataDirectory = Path.Combine(AppContext.BaseDirectory, "Data");
        _filePath = Path.Combine(_dataDirectory, "members.json");
    }

    public static void EnsureInitialized(string dataDirectory, string filePath)
    {
        if (!Directory.Exists(dataDirectory))
            Directory.CreateDirectory(dataDirectory);


        if (!File.Exists(filePath))
            // Om inte filen members.json finns på den angivna filsökvägen, skapa den och skriv in en tom array i json-format (motsvarar en tom lista när den läses in)
            File.WriteAllText(filePath, "[]");

        // Om filen finns men är tom/whitespace, initiera den till en tom array
        string existing = File.ReadAllText(filePath);
        if (string.IsNullOrWhiteSpace(existing))
            File.WriteAllText(filePath, "[]");
    }

    public async Task SaveContentToFileAsync(IEnumerable<Member> members)
    {
        if (!Directory.Exists(_dataDirectory))
        {
            Directory.CreateDirectory(_dataDirectory);
        }
        var json = JsonSerializer.Serialize(members, _jsonOptions);
        await File.WriteAllTextAsync(_filePath, json);

    }

    public async Task<ResponseResult<IEnumerable<Member>>> ReadAsync()
    {
        try
        {
            string json = await File.ReadAllTextAsync(_filePath);

            List<Member>? members = JsonSerializer.Deserialize<List<Member>>(json, _jsonOptions);
            return new ResponseResult<IEnumerable<Member>>
            {
                Success = true,
                Data = members ?? []
            };
        }
        catch 
        {
            return new ResponseResult<IEnumerable<Member>>
            {
                Success = false,
                Message = "Could not read file.",
                Data = []
            };
        }
     
    }
}