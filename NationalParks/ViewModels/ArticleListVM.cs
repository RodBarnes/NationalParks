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
}
