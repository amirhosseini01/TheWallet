using System.IO;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Site.Dto;
using WebApi.Dto;

namespace Site.Pages;

public partial class Finance
{
    [Inject]
    private HttpClient Http { get; set; }
    [Inject]
    private IConfiguration Configuration { get; set; }
    [Inject]
    private IJSRuntime JsRuntime { get; set; }
    private FinanceInputDto FinanceInput;

    protected override async Task OnInitializedAsync()
    {
        FinanceInput = new();
    }
    private async Task HandleValidSubmit()
    {
        var response = await Http.PostAsJsonAsync($"{Configuration["WebApiBaseUrl"]}/Finance", FinanceInput);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<ResponsePayload>();
            await JsRuntime.InvokeVoidAsync("CloseModal", "#financeModal");
            await JsRuntime.InvokeVoidAsync("ResetForm", "#frmSubmit");
            await JsRuntime.InvokeVoidAsync("alert", result.Message);
        }
    }
}