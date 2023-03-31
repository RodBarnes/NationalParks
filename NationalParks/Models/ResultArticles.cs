/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public partial class ResultArticles : Result
{
    public const Terms Term = Terms.articles;
    public List<Article> Data { get; set; }
}
