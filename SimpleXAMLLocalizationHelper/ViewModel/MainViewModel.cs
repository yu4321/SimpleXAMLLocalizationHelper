﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using SimpleXAMLLocalizationHelper.Messages;
using System;
using System.Diagnostics;

namespace SimpleXAMLLocalizationHelper.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                Set(nameof(CurrentViewModel), ref _currentViewModel, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            CurrentViewModel = new StartViewModel();
            App.Logger.Info("\n*******************************************************************");
            App.Logger.Info("실행 - " + DateTime.Now.ToString());
            App.Logger.Info("*******************************************************************\n");
            Messenger.Default.Register<GotoPageMessage>(this, (x) => ReceivePageChangeMessage(x));
        }

        private void ReceivePageChangeMessage(GotoPageMessage action)
        {
            switch (action.NextPage)
            {
                case PageName.Start:
                    CurrentViewModel = new StartViewModel();
                    break;

                case PageName.Core:
                    var temp = new CoreViewModel();
                    if (temp.MainDataTable.Columns.Count > 0)
                    {
                        CurrentViewModel = temp;
                    }
                    break;

                case PageName.History:
                    Process.Start("notepad.exe", "logs/" + System.Reflection.Assembly.GetEntryAssembly().GetName().Name + ".log");
                    break;

                case PageName.Setting:
                    Process.Start("notepad.exe", "Setting.json");
                    break;
            }
            System.GC.Collect();
        }

        public override void Cleanup()
        {

            base.Cleanup();
        }
    }
}