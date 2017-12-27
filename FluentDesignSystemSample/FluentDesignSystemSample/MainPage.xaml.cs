using FluentDesignSystemSample.Views;
using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FluentDesignSystemSample.Services;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace FluentDesignSystemSample
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page, INavigationRoot
    {
        private INavigationService _navigationService;


        public MainPage()
        {
            this.InitializeComponent();

            NavigationView.RegisterPropertyChangedCallback(NavigationView.IsPaneOpenProperty, OnNavigationViewIsPaneOpenPropertyChanged);
            SystemNavigationManager.GetForCurrentView().BackRequested += OnAppBackRequested;
            WindowTitle.EnableLayoutImplicitAnimations(TimeSpan.FromMilliseconds(100));
            this.Loaded += OnLoaded;
        }

        public TitleBarHelper TitleHelper => TitleBarHelper.Instance;

        public event EventHandler IsPaneOpenChanged;

        public bool IsPaneOpen => NavigationView.IsPaneOpen;

        public double CompactPaneLength => NavigationView.CompactPaneLength;

        public void InitializeNavigationService(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(typeof(MaterialPage));
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                _navigationService.NavigateToSettingsAsync();
                return;
            }

            switch (args.InvokedItem as string)
            {
                case "Depth":
                    _navigationService.NavigateToDepthAsync();
                    break;
                case "Light":
                    _navigationService.NavigateToLightAsync();
                    break;
                case "Material":
                    _navigationService.NavigateToMaterialAsync();
                    break;
                case "Motion":
                    _navigationService.NavigateToMotionAsync();
                    break;
                case "Scale":
                    _navigationService.NavigateToScaleAsync();
                    break;
            }
        }

        private void RootFrame_Navigated(object sender, NavigationEventArgs e)
        {
            switch (e.SourcePageType)
            {
                case Type c when e.SourcePageType == typeof(MaterialPage):
                    ((NavigationViewItem)NavigationView.MenuItems[0]).IsSelected = true;
                    break;
                case Type c when e.SourcePageType == typeof(LightPage):
                    ((NavigationViewItem)NavigationView.MenuItems[1]).IsSelected = true;
                    break;
                case Type c when e.SourcePageType == typeof(MotionPage):
                    ((NavigationViewItem)NavigationView.MenuItems[2]).IsSelected = true;
                    break;
                case Type c when e.SourcePageType == typeof(DepthPage):
                    ((NavigationViewItem)NavigationView.MenuItems[3]).IsSelected = true;
                    break;
                case Type c when e.SourcePageType == typeof(ScalePage):
                    ((NavigationViewItem)NavigationView.MenuItems[4]).IsSelected = true;
                    break;
                case Type c when e.SourcePageType == typeof(SettingsPage):
                    NavigationView.SelectedItem = NavigationView.SettingsItem;
                    break;
            }

        }


        private void OnNavigationViewIsPaneOpenPropertyChanged(DependencyObject sender, DependencyProperty dp)
        {
            IsPaneOpenChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnAppBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (RootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                RootFrame.GoBack();
            }
        }


    }
}
