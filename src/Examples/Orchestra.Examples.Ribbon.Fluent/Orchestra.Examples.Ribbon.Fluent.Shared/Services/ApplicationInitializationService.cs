﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationInitializationService.cs" company="Orchestra development team">
//   Copyright (c) 2008 - 2014 Orchestra development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orchestra.Examples.Ribbon.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Catel;
    using Catel.IoC;
    using Catel.Logging;
    using Catel.MVVM;
    using Models;
    using Orchestra.Services;
    using InputGesture = Catel.Windows.Input.InputGesture;

    public class ApplicationInitializationService : ApplicationInitializationServiceBase
    {
        private readonly IServiceLocator _serviceLocator;

        #region Constants
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        #endregion

        public ApplicationInitializationService(IServiceLocator serviceLocator)
        {
            Argument.IsNotNull(() => serviceLocator);

            _serviceLocator = serviceLocator;
        }

        public override async Task InitializeBeforeCreatingShell()
        {
            // Non-async first
            await RegisterTypes();
            await InitializeCommands();

            await RunAndWaitAsync(new Func<Task>[]
            {
                InitializePerformance
            });
        }

        private async Task InitializeCommands()
        {
            var commandManager = ServiceLocator.Default.ResolveType<ICommandManager>();

            commandManager.CreateCommandWithGesture(typeof(Commands.Application), "Exit");
            commandManager.CreateCommandWithGesture(typeof(Commands.Application), "About");

            commandManager.CreateCommandWithGesture(typeof(Commands.Demo), "LongOperation");
            commandManager.CreateCommandWithGesture(typeof(Commands.Demo), "ShowMessageBox");

            commandManager.CreateCommand("File.Open", new InputGesture(Key.O, ModifierKeys.Control), throwExceptionWhenCommandIsAlreadyCreated: false);
            commandManager.CreateCommand("File.SaveToImage", new InputGesture(Key.I, ModifierKeys.Control), throwExceptionWhenCommandIsAlreadyCreated: false);
            commandManager.CreateCommand("File.Print", new InputGesture(Key.P, ModifierKeys.Control), throwExceptionWhenCommandIsAlreadyCreated: false);

            var keyboardMappingsService = _serviceLocator.ResolveType<IKeyboardMappingsService>();
            keyboardMappingsService.AdditionalKeyboardMappings.Add(new KeyboardMapping("MyGroup.Zoom", "Mousewheel", ModifierKeys.Control));
        }

        public override async Task InitializeAfterCreatingShell()
        {
            Log.Info("Delay to show the splash screen");

            Thread.Sleep(2500);
        }

        private async Task InitializePerformance()
        {
            Log.Info("Improving performance");

            Catel.Data.ModelBase.DefaultSuspendValidationValue = true;
            Catel.Windows.Controls.UserControl.DefaultCreateWarningAndErrorValidatorForViewModelValue = false;
            Catel.Windows.Controls.UserControl.DefaultSkipSearchingForInfoBarMessageControlValue = true;
        }

        private async Task RegisterTypes()
        {
            var serviceLocator = _serviceLocator;

            serviceLocator.RegisterType<IAboutInfoService, AboutInfoService>();

            //throw new Exception("this is a test exception");
        }
    }
}