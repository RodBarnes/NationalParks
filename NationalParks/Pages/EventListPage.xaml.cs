/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
namespace NationalParks.Pages;

public partial class EventListPage : ContentPage
{
    readonly EventListVM _vm;

	public EventListPage(EventListVM vm)
	{
		InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (!_vm.IsPopulated)
        {
#pragma warning disable 4014
            _vm.PopulateData();
#pragma warning restore 4014
        }
    }
}