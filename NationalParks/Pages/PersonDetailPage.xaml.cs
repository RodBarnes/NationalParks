/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
namespace NationalParks.Pages;

public partial class PersonDetailPage : ContentPage
{
	public PersonDetailPage(PersonDetailVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}