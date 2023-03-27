namespace NationalParks.Models;

public partial class ResultArticles : Result
{
    public const Terms Term = Terms.articles;
    public List<Article> Data { get; set; }
}
