namespace NationalParks.ViewModels;

public partial class ArticleListVM : ListVM
{
    public ArticleListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Articles";
        Term = ResultArticles.Term;
        FilterName = "Article";
        AllowFilterStates = true;
    }

    [RelayCommand]
    public new async Task PopulateData()
    {
        await GetItems();
        await base.PopulateData();
    }

    [RelayCommand]
    public async Task GetItems()
    {
        if (IsBusy)
            return;

        ResultArticles result = await GetItems<ResultArticles>(ResultArticles.Term);
        foreach (Article item in result.Data)
        {
            item.FillMainImage();
            Items.Add(item);
        }
        TotalItems = result.Total;
        IsPopulated = true;
    }


    [RelayCommand]
    public new async Task GetClosest()
    {
        if (IsBusy)
            return;

        ShowClosestProgress = true;

        if (Items.Count < TotalItems)
        {
            // Get the rest of the items
            LimitItems = 50;
            while (TotalItems > Items.Count)
            {
                ProgressClosest = (double)Items.Count / (double)TotalItems;
                await GetItems();
            }
            LimitItems = 20;
        }

        await base.GetClosest();

        ShowClosestProgress = false;
    }
}
