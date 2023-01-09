namespace NationalParks.Views;

public partial class DataTesterPage : ContentPage
{
	public DataTesterPage(DataTesterVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}