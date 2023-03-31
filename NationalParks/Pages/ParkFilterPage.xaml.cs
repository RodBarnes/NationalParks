/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
namespace NationalParks.Pages;

public partial class ParkFilterPage : ContentPage
{
	public ParkFilterPage(ParkListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}