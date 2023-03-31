/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.ViewModels;

public partial class MessageVM : ObservableObject
{
    [ObservableProperty] string text;
    [ObservableProperty] string buttonText;

    [ObservableProperty] bool isVisible;

    public MessageVM()
    {
        IsVisible = false;
        ButtonText = "OK";
        Text = "Message";
    }

    public void Show(string msg = "", string button = "")
    {
        if (!String.IsNullOrEmpty(msg))
        {
            Text = msg;
        }
        if (!String.IsNullOrEmpty(button))
        {
            ButtonText = button;
        }
        IsVisible = true;
    }

    [RelayCommand]
    public void HideMessage()
    {
        IsVisible = false;
    }
}
