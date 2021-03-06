﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using XFTextpadApp.Models;

namespace XFTextpadApp.ViewModels
{
    public class ViewTextPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
       
        public TextItem TextItem { get; set; }

        public DelegateCommand DoneCommand { get; set; }

        public ViewTextPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;

            DoneCommand = new DelegateCommand(async () => await Done());
        }

        private async Task Done()
        {
            await _navigationService.GoBackAsync();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(nameof(TextItem)))
            {
                TextItem = (TextItem)parameters[nameof(Models.TextItem)];

                RaisePropertyChanged(nameof(TextItem));
            }
        }
    }
}
