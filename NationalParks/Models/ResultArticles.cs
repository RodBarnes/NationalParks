namespace NationalParks.Models;

public partial class ResultArticles : Result
{
    public const string Term = "articles";
    public List<Article> Data { get; set; }
}
