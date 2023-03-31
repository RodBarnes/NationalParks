/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
namespace NationalParks.Pages;

public partial class ThingToDoFilterPage : ContentPage
{
	public ThingToDoFilterPage(ThingToDoListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}