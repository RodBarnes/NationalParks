namespace NationalParks.ViewModels;

public partial class ListVM : BaseVM
{
    protected readonly int limitItems = 20;
    protected int startItems = 0;
    protected int totalItems = 0;

    public FilterVM Filter { get; set; }

    [ObservableProperty] int itemsRefreshThreshold = -1;

    private bool isPopulated = false;
    public bool IsPopulated
    {
        get => isPopulated;
        set
        {
            if (value == true)
            {
                ItemsRefreshThreshold = 2;
                Title = GetTitle();
            }
            else
            {
                ItemsRefreshThreshold = -1;
                startItems = 0;
            }
            isPopulated = value;
        }
    }
    protected string BaseTitle { get; set; }

    [RelayCommand]
    async Task GoToFilter(string pageName)
    {
        await Shell.Current.GoToAsync(pageName, true, new Dictionary<string, object>
        {
            {"VM", this }
        });
    }

    protected string GetTitle()
    {
        string tmp = $"{BaseTitle}";
        if (totalItems > 0)
        {
            tmp += $"  ({totalItems}";
            if (Filter is not null && Filter.IsFiltered)
            {
                tmp += $", filtered";
            }
            tmp += ")";
        }

        return tmp;
    }
}
