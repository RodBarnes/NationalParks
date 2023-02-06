namespace NationalParks.Views;

public partial class FilterView : ContentView
{
	public FilterView()
	{
		InitializeComponent();
		//AdjustRegions();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        // This is called twice, once with -1, -1 then the next call with
        // the actual width and height.
        if (Height > 0)
        {
            AdjustRegionHeight(height);
        }
    }

    private void AdjustRegionHeight(double pageHeight)
	{
        double frameHeight;
        double bottomSpace = 44; 

        int cnt = 0;
		cnt += (StatesFilter.IsVisible) ? 1 : 0;
        cnt += (TopicsFilter.IsVisible) ? 1 : 0;
        cnt += (ActivitiesFilter.IsVisible) ? 1 : 0;

        double fullHeight = pageHeight - ButtonArea.Height.Value;
        frameHeight = (fullHeight / cnt) - bottomSpace;

		StatesFrame.HeightRequest = (StatesFilter.IsVisible) ? frameHeight : 0;
        TopicsFrame.HeightRequest = (TopicsFilter.IsVisible) ? frameHeight : 0;
        ActivitiesFrame.HeightRequest = (ActivitiesFilter.IsVisible) ? frameHeight : 0;
    }
}