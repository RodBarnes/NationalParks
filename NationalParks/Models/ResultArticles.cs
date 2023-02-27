namespace NationalParks.Models;

public partial class ResultArticles : Result
{
    public const string Term = "articles";
    public ICollection<Article> Data { get; set; }
}
