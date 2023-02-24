namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Article), "Model")]
public partial class ArticleDetailVM : DetailVM
{
    [ObservableProperty] Article article;
    [ObservableProperty] RelatedParksVM relatedParksVM;

    public ArticleDetailVM(IMap map) : base(map)
    {
        Title = "Article";
    }

    [RelayCommand]
    public void PopulateData()
    {
        Model = Article;

        RelatedParksVM = new RelatedParksVM("Related Parks", false, Article.RelatedParks);
    }
}
