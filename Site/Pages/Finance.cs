using System.IO;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Site.Dto;

namespace Site.Pages;

public partial class Finance
{
    [Inject]
    private HttpClient Http { get; set; }

    public async Task Add()
    {
        var item = new FinanceInputDto()
        {
            Amount = 2000,
            PayDate = DateTime.Now,
            Type = "Wallet"
        };
        var response = await Http.PostAsJsonAsync("https://localhost:7230/Finance", item);
        Console.WriteLine(response.IsSuccessStatusCode);
        Console.WriteLine(response);
        if (response.IsSuccessStatusCode)
        {
            using Stream responseStream = await response.Content.ReadAsStreamAsync();
            Console.WriteLine("final");
            var finanlRes = await JsonSerializer.DeserializeAsync<FinanceInputDto>(responseStream);
            Console.WriteLine("yes");
        }
    }
}