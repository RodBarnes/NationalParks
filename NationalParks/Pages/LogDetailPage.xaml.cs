/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
namespace NationalParks.Pages;

public partial class LogDetailPage : ContentPage
{
	public LogDetailPage(LogDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}