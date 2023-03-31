/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
namespace NationalParks.Pages;

public partial class EventDetailPage : ContentPage
{
	public EventDetailPage(EventDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}