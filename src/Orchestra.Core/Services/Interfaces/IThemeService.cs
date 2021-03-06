﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThemeService.cs" company="WildGums">
//   Copyright (c) 2008 - 2014 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orchestra.Services
{
    public interface IThemeService
    {
        bool ShouldCreateStyleForwarders();
        ThemeInfo GetThemeInfo();
    }
}
