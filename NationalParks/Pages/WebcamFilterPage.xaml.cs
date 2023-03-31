/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
namespace NationalParks.Pages;

public partial class WebcamFilterPage : ContentPage
{
	public WebcamFilterPage(WebcamListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}