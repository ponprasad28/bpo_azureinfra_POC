﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RevitCapp.Commands;
using System.Windows.Input;
using System.Windows;
using RevitCapp.Model;

namespace RevitCapp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ApiCaller _apiCaller = new ApiCaller();

        private bool _isAuthenticated;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            set
            {
                if (_isAuthenticated != value)
                {
                    _isAuthenticated = value;
                    OnPropertyChanged(nameof(IsAuthenticated));

                    (ClickCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (LoginCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (LogoutCommand as RelayCommand)?.RaiseCanExecuteChanged();

                }
            }
        }

        private User _user;
        public User User
        {
            get => _user;
            set
            {
                if (_user != value)
                {
                    _user = value;
                    OnPropertyChanged(nameof(User));
                }
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand ClickCommand { get; }

        public MainViewModel()
        {
            LoginCommand = new RelayCommand(Login, () => !IsAuthenticated);
            LogoutCommand = new RelayCommand(LogOut, () => IsAuthenticated);
            ClickCommand = new RelayCommand(Click, () => IsAuthenticated);
        }

        private async void Login()
        {
            try
            {
                var token = await AzureAuthHelper.Instance.GetAccessTokenAsync();
                if (!string.IsNullOrEmpty(token))
                {
                    IsAuthenticated = true;
                    User = AzureAuthHelper.Instance.GetSignedInUser();
                    await _apiCaller.PostLogInfoAsync(User.Name, User.UserEmail);
                }
                else
                {
                    MessageBox.Show("Login failed: No token received.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed: User cancelled login");
            }
        }

        private async void LogOut()
        {
            await AzureAuthHelper.Instance.SignOutAsync();
            MessageBox.Show($"SignedOut User {User.Name}");
            User = new User("", "");
            IsAuthenticated = false;
        }

        private async void Click()
        {
            await _apiCaller.PostClickInfoAsync(User.Name, User.UserEmail);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
