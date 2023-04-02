/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Article), "Model")]
public partial class ArticleDetailVM : DetailVM
{
    [ObservableProperty] Article article;
    [ObservableProperty] RelatedParksVM relatedParks;

    public ArticleDetailVM(IConnectivity connectivity, IMap map) : base(connectivity, map)
    {
        Title = "Article";
    }

    [RelayCommand]
    public void PopulateData()
    {
        Model = Article;

        RelatedParks = new RelatedParksVM("Related Parks", false, Article.RelatedParks);
    }
}
