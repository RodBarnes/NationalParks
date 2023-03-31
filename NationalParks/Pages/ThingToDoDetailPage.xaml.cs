/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
namespace NationalParks.Pages;

public partial class ThingToDoDetailPage : ContentPage
{
	public ThingToDoDetailPage(ThingToDoDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}