I have an app which retrieves data from different APIs but each returns data of a very similar structure.  The app provides 12 list pages to display each of the different results - A, B, C, etc.  But the contents of each of these pages is identical at the base level (Title, Description) and these are the only properties displayed in the list.  Tapping an item in the list opens a detail page which then shows the full content of the model.

The models are like:

	public partial class BaseModel
	{
		public string Title { get; set; }
		public string Description { get; set; }
	}

	public partial class ModelA : BaseModel
	{
		// Other properties unique to A...
	}
	
	public partial class ModelB : BaseModel
	{
		// Other properties unique to B...
	}

The API URLs all take the form:

    $"{DomainUrl}{term}?api_key={keyvalue}{paramList}";

And the result structures are like this where the Term identifies the specific API to be used:

	public partial class Result
	{
		public int Total { get; set; }
		public int Limit { get; set; }
		public int Start { get; set; }
	}

	public partial class ResultA : Result
	{
		public const string Term = "atype";
		public List<ModelA> Data { get; set; }
	}

	public partial class ResultB : Result
	{
		public const string Term = "btype";
		public List<ModelB> Data { get; set; }
	}

From the page, it calls this method to populate the list: 

	[RelayCommand]
    public async Task GetItems()
    {
		ResultA resultA = await DataService.GetItemsAsync<ResultA>(ResultA.Term, Items.Count, LimitItems);
		foreach (Park item in resultA.Data)
		{
			item.FillMainImage();
			Items.Add(item);
		}
		TotalItems = resultA.Total;
	}

Which invokes this method to call the API:

    public static async Task<T> GetItemsAsync<T>(string term, int start = 0, int limit = 10)
    {
        T result = default;
        string url = BuildUrlWithFilter(term, start, limit);

        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<T>();
        }

        return result;
    }

Each list page binds to the BaseModel as they only need to display those properties.  But I'm still left with creating 12 separate pages, one for each model type (that are effectively identical) so, to reduce the redundant code, I put the majority of the XAML into a view and reference that in each page.  So, far so good; all works great.

But each list VM still must duplicate the code to invoke GetItems() since only that VM knows the subclass model in order to have that data available when going to the detail page.

Is there anyway to make GetItems a generic so the type could be passed and then it could be part of the base ListVM class?



