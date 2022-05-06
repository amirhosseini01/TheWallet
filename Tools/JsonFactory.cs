using System.Text;
using System.Text.Json;

namespace TheWallet.Tools;
public class JsonFactory<T>
{
    #region CTOR
    private readonly string _jsonFileAddress;
    public JsonFactory(string jsonFileAddress)
    {
        _jsonFileAddress = jsonFileAddress ?? "C:\\Users\\Amir\\Desktop\\Finances.json";
    }
    #endregion 

    #region Properties
    public List<T> Entities { get; set; }
    #endregion
    public async Task GetAllAsync()
    {
        string json = await File.ReadAllTextAsync(_jsonFileAddress);
        Entities = JsonSerializer.Deserialize<List<T>>(json);
    }
    public void Add(T entity)
    {
        Entities?.Add(entity);
    }
    public void Add(List<T> entities)
    {
        Entities?.AddRange(entities);
    }
    public async Task SaveAsync(T entity)
    {
        await File.AppendAllTextAsync(_jsonFileAddress, JsonSerializer.Serialize(entity));
    }
    public async Task SaveAsync(List<T> entities)
    {
        StringBuilder jsonStr = new();
        foreach (var entity in entities)
        {
            jsonStr.Append(JsonSerializer.Serialize(entity));
        }
        await File.AppendAllTextAsync(_jsonFileAddress, jsonStr.ToString());
    }
}