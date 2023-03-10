## Background ##
Using anmials as data in the example...

App has multiple ContentPages that all display subclasses of Animals; etc., Monkey, Tiger, etc.  Tapping on an item in the list displays a ContentPage that is the detail of each animal.

The APIs are identical with the exceptionof specifying which specific animal is being requested (e.g., Monkey, Tiger, etc.) and this is specified in the `term` variable.

    $"{DomainUrl}{term}?{paramList}";

To reduce code, the dataservice is impleneted as a generic method that allows specifying the specific animal:

    public static async Task<T> GetItemsAsync<T>(string term, int start = 0, int limit = 10, filter = "")
    {
        T result = default;
        string url = BuildUrlWithFilter(term, start, limit, filter);

        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<T>();
        }

        return result;
    }

With models like:

    public partial class Animal
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public partial class Tiger : Animal
    {
        // Other properties unique to Tiger...
    }
    
    public partial class Monkey : Animal
    {
        // Other properties unique to Monkey...
    }


And result classes (for receiving the data from the DataService) like:

    public partial class Result
    {
        public int Total { get; set; }
        public int Limit { get; set; }
        public int Start { get; set; }
    }

    public partial class ResultTigers : Result
    {
        public const string Term = "tiger";
        public List<Tiger> Data { get; set; }
    }

    public partial class ResultMonkeys : Result
    {
        public const string Term = "monkey";
        public List<Monkey> Data { get; set; }
    }

Which is invoked from the VM as:

    [RelayCommand]
    public async Task GetItems()
    {
        ResultMonkeys resultMonkeys = await DataService.GetItemsAsync<ResultMonkeys>(ResultMonkeys.Term, Items.Count, LimitItems);
        foreach (Monkey item in resultMonkeys.Data)
        {
            item.FillMainImage();
            Items.Add(item);
        }
        TotalItems = resultMonkeys.Total;
    }


Now, to also reduce code duplication, each list `<ContentPage>` references a `<ContentView>` that contains the `<CollectionView>` (and all the rest of the XAML) for interacting with the list.

MonkeyListPage:

    <ContentPage
        xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
        xmlns:v="clr-namespace:Sample.Views"
        x:DataType="vm:MonkeyListVM"
        ...
        <views:ListView BindingContext="{Binding .}"/>
    </ContentPage>

TigerListPage:

    <ContentPage
        xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
        xmlns:v="clr-namespace:Sample.Views"
        x:DataType="vm:TigerListVM"
        ...
        <views:ListView BindingContext="{Binding .}"/>
    </ContentPage>

ListView.xaml:


    <ContentView
        xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
        xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
        xmlns:m="clr-namespace:Sample.Models"
        x:DataType="vm:ListVM"
        ...
        <CollectionView ItemsSource="{Binding Items}"
                SelectionMode="None"
                RemainingItemsThreshold="{Binding ItemsRefreshThreshold}"
                RemainingItemsThresholdReachedCommand="{Binding GetItemsCommand}">
            <CollectionView.ItemTemplate>
                <DataTemplate x:DataType="m:Animal">
                    <VerticalStackLayout>
                        <Image Source="{Binding MainImage}" Aspect="AspectFill" />
                        <Label Text="{Binding Title}" Style="{StaticResource LabelLargeBold}" />
                        <Label Text="{Binding Description}" Style="{StaticResource LabelMedium}" />
                    </VerticalStackLayout>
                </DataTemplate>
            </CollectionView.ItemTemplate>
        </CollectionView>
    </ContentView>

While the list contains actual animal types (e.g, `Tiger`), since this is XAML common to all animal lists, the `<CollectionView.DataTemplate>` uses `x:DataType="model:Animal"` since it only displays a tile showing an image of the animal, the name, and the description -- properties common to all animals.

 To continue reducing code duplication, each page VM is a subclass of a base VM that manages everything about the list objects.

MonkeyListVM:

    public partial class MonkeyListVM : ListVM
    {
        public MonkeyListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
        {
            BaseTitle = "Monkeys";
        }

        [RelayCommand]
        public async Task GetTimes()
        {
            if (IsBusy)
                return;

            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }

                IsBusy = true;

                ResultMonkeys result = await DataService.GetItemsAsync<ResultMonkeys>(ResultMonkey.Term, Items.Count, LimitItems);
                foreach (Monkey item in result.Data)
                {
                    Items.Add(item);
                }
                TotalItems = result.Total;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"GetItems failed: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

TigerListVM:

    public partial class TigerListVM : ListVM
    {
        public TigerListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
        {
            BaseTitle = "Tigers";
        }

        [RelayCommand]
        public async Task GetTimes()
        {
            if (IsBusy)
                return;

            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }

                IsBusy = true;

                ResultTigers result = await DataService.GetItemsAsync<ResultTigers>(ResultTigers.Term, Items.Count, LimitItems);
                foreach (Tiger item in result.Data)
                {
                    Items.Add(item);
                }
                TotalItems = result.Total;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"GetItems failed: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

ListVM:

    public partial class ListVM : BaseVM
    {
        public ListVM(IConnectivity connectivity, IGeolocation geolocation)
        {
            IsBusy = false;
            this.geolocation = geolocation;
            this.connectivity = connectivity;
        }
        ...
    }

So, while I'm still left implementing a separate list `<ContentPage>` for each animal type, these are basically empty except for a reference to the `<ContentView>` that actually presents the list.  Also, while I have to still implement a VM for each list page, these are all subclasses of a base VM that manages all the things common to animals lists.

So, far so good; animals are displayed and having the complete animal (e.g., Monkey, Tiger) in the list, it can be passed via `QueryProperty` when going to the detail page.


## The Issue ##

It is the `<CollectionView>` where the `RemainingItemsThresholdReachedCommand="{Binding GetItemsCommand}"` is implmented to fill the list.
  
But the base VM and the list view only know about a list of `Animal`.  It doesn't know about s But each list VM still must duplicate the code to invoke GetItems() since only that VM knows the subclass model in order to have that data available when going to the detail page.

Is there anyway to make GetItems a generic so the type could be passed and then it could be part of the base ListVM class?


The call to invoke DataService.GetItemsAsync(Model) has to know about the model type.  So, this would have to be done in each page VM.  It cannot be done in the base VM since it only knows about Animal in general.  So, either there's a big switch() in base VM so it can determine which type to use when getting the results -- definitely a stinky code smell -- or each page VM has to implement its own version GetItems<Model> since the page VM is the only one that really knows about the specific animal type.

But, implementing GetItems in each page means the <CollectionView> has to be implemented in each page.
