using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.Dto;

namespace Site.Pages;

public partial class Finance
{
    [Inject]
    private HttpClient Http { get; set; }
    [Inject]
    private IConfiguration Configuration { get; set; }
    [Inject]
    private IJSRuntime JsRuntime { get; set; }

    //Properties
    private FinanceInputDto FinanceInput;
    private FinanceTypeInputDto FinanceTypeInput;
    private FinanceApiFilterDto FinanceListFilter;
    private FinancePaginationDto FinanceList;
    private List<FinanceTypeListDto> FinanceTypeList;

    protected override async Task OnInitializedAsync()
    {
        FinanceInput = new();
        FinanceTypeInput = new();
        FinanceListFilter = new();

        await FillFinanceTypes();

        await FillFinances();
    }
    private async Task HandleValidSubmit()
    {
        var response = await Http.PostAsJsonAsync($"{Configuration["WebApiBaseUrl"]}/Finance", FinanceInput);
        if (!response.IsSuccessStatusCode)
        {
            await JsRuntime.InvokeVoidAsync("alert", $"Could Not Getting Data! Status: {response.StatusCode}");
            return;
        }
        var result = await response.Content.ReadFromJsonAsync<ResponsePayload>();
        if (!result.Succeeded)
        {
            await JsRuntime.InvokeVoidAsync("alert", result.Message);
            return;
        }
        await JsRuntime.InvokeVoidAsync("CloseModal", "#financeModal");
        await JsRuntime.InvokeVoidAsync("ResetForm", "#frmSubmit");
        FinanceListFilter = new();
        FinanceInput = new();
        await FillFinances();
        StateHasChanged();
    }

    private async Task HandleValidSubmitFinanceType()
    {
        var response = await Http.PostAsJsonAsync($"{Configuration["WebApiBaseUrl"]}/FinanceType", FinanceTypeInput);
        if (!response.IsSuccessStatusCode)
        {
            await JsRuntime.InvokeVoidAsync("alert", $"Could Not Getting Data! Status: {response.StatusCode}");
            return;
        }
        var result = await response.Content.ReadFromJsonAsync<ResponsePayload>();
        if (!result.Succeeded)
        {
            await JsRuntime.InvokeVoidAsync("alert", result.Message);
            return;
        }
        await JsRuntime.InvokeVoidAsync("CloseModal", "#financeTypeModal");
        await JsRuntime.InvokeVoidAsync("ResetForm", "#frmFinanceTypeSubmit");
        FinanceTypeInput = new();
        await FillFinanceTypes();
        StateHasChanged();
    }

    private async Task GetById(int id)
    {
        var result = await Http.GetFromJsonAsync<ResponsePayload<FinanceInputDto>>(requestUri: $"{Configuration["WebApiBaseUrl"]}/Finance/ById/{id}");

        if (!result.Succeeded)
        {
            await JsRuntime.InvokeVoidAsync("alert", result.Message);
            return;
        }
        FinanceInput = result.Obj;
        StateHasChanged();
        await JsRuntime.InvokeVoidAsync("ShowModal", "#financeModal");
    }

    private async Task Remove(int id)
    {
        var result = await Http.GetFromJsonAsync<ResponsePayload>(requestUri: $"{Configuration["WebApiBaseUrl"]}/Finance/Remove/{id}");

        if (!result.Succeeded)
        {
            await JsRuntime.InvokeVoidAsync("alert", result.Message);
            return;
        }
        FinanceListFilter = new();
        await FillFinances();
        StateHasChanged();
    }

    private async Task OpenModal()
    {
        FinanceInput = new();
        StateHasChanged();
        await JsRuntime.InvokeVoidAsync("ShowModal", "#financeModal");
    }
    private async Task OpenFinanceTypeModal()
    {
        FinanceTypeInput = new();
        StateHasChanged();
        await JsRuntime.InvokeVoidAsync("ShowModal", "#financeTypeModal");
    }

    private async Task FillFinances()
    {
        var response = await Http.PostAsJsonAsync($"{Configuration["WebApiBaseUrl"]}/Finance/List", FinanceListFilter);

        if (!response.IsSuccessStatusCode)
        {
            await JsRuntime.InvokeVoidAsync("alert", $"Could Not Getting Data! Status: {response.StatusCode}");
            return;
        }
        var result = await response.Content.ReadFromJsonAsync<ResponsePayload<FinancePaginationDto>>();
        if (!result.Succeeded)
        {
            await JsRuntime.InvokeVoidAsync("alert", result.Message);
            return;
        }
        FinanceList = new()
        {
            PageIndex = result.Obj.PageIndex,
            TotalPages = result.Obj.TotalPages,
            List = result.Obj.List,
            WalletBalance = result.Obj.WalletBalance
        };
    }

    private async Task FillFinanceTypes()
    {
        var response = await Http.PostAsJsonAsync($"{Configuration["WebApiBaseUrl"]}/FinanceType/List",
         new PaginationDto()
         {
             Take = 1000
         });

        if (!response.IsSuccessStatusCode)
        {
            await JsRuntime.InvokeVoidAsync("alert", $"Could Not Getting Finance Types! Status: {response.StatusCode}");
            return;
        }
        var result = await response.Content.ReadFromJsonAsync<ResponsePayload<List<FinanceTypeListDto>>>();
        if (!result.Succeeded)
        {
            await JsRuntime.InvokeVoidAsync("alert", result.Message);
            return;
        }
        FinanceTypeList = result.Obj;
    }

    private async Task ChangePagination(int pageId)
    {
        if (FinanceListFilter is null)
            FinanceListFilter = new();
        FinanceListFilter.Skip = pageId;
        await FillFinances();
        StateHasChanged();
    }
}