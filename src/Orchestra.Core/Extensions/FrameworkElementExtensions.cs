﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameworkElementExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2014 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orchestra
{
    using System.Windows;
    using Catel;
    using Catel.IoC;
    using Microsoft.Xaml.Behaviors;

    public static class FrameworkElementExtensions
    {
        public static TBehavior ApplyBehavior<TBehavior>(this DependencyObject dependencyObject)
            where TBehavior : Behavior
        {
            Argument.IsNotNull(() => dependencyObject);

            var behaviors = Interaction.GetBehaviors(dependencyObject);

            var typeFactory = dependencyObject.GetTypeFactory();
            var behavior = typeFactory.CreateInstanceWithParametersAndAutoCompletion<TBehavior>();

            behaviors.Add(behavior);

            return behavior;
        }
    }
}
