using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using StepsRecorderAdvanced.Avalonia.GUI.Models;
using StepsRecorderAdvanced.Avalonia.GUI.ViewModels.Interfaces;
using StepsRecorderAdvanced.Core.Models.Extensions;
using StepsRecorderAdvanced.Core.ViewModels;
using Microsoft.Toolkit.Mvvm.Input;
using StepsRecorderAdvanced.Avalonia.GUI.Models.Interfaces;
using StepsRecorderAdvanced.Core.Models.Enums;
using Microsoft.Extensions.Configuration;
using StepsRecorderAdvanced.Core.ViewModels.Interfaces;

namespace StepsRecorderAdvanced.Avalonia.GUI.ViewModels
{
    public class SettingViewModel : ViewModelBase, ISettingViewModel
    {
        private bool settingVisible;

        #region member fields
        
        #endregion

        #region Constructor

        public SettingViewModel()
        {
            
        }

        #endregion

        #region properties

        public bool SettingVisible
        {
            get => settingVisible;
            set => SetProperty(ref settingVisible, value);
        }

        #endregion

        #region Commands

       

        #endregion

        #region helper methods

  
        #endregion

    }
}