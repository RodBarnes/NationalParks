/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
namespace NationalParks.Pages;

public partial class NewsReleaseDetailPage : ContentPage
{
	public NewsReleaseDetailPage(NewsReleaseDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}