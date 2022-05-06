using System.Text;
using System.Text.Json;

namespace TheWallet.Tools;
public class JsonFactory<T>
{
    #region CTOR
    private readonly string _jsonFileAddress;
    public JsonFactory(string jsonFileAddress)
    {
        _jsonFileAddress = jsonFileAddress;
    }
    #endregion 

    #region Properties
    private List<T> Entities;
    #endregion
    public async Task<List<T>> GetAllAsync()
    {
        StringBuilder strJson = new();
        strJson.Append('[');
        strJson.Append(await File.ReadAllTextAsync(_jsonFileAddress));
        strJson.Append(']');
        Entities = JsonSerializer.Deserialize<List<T>>(strJson.ToString());
        return Entities;
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