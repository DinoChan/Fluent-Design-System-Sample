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

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace FluentDesignSystemSample
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current { get; private set; }

        public MainPage()
        {
            this.InitializeComponent();
            Current = this;
            NavigationView.RegisterPropertyChangedCallback(NavigationView.IsPaneOpenProperty, OnNavigationViewIsPaneOpenPropertyChanged);
            SystemNavigationManager.GetForCurrentView().BackRequested += OnAppBackRequested;
            WindowTitle.EnableLayoutImplicitAnimations(TimeSpan.FromMilliseconds(100));
            this.Loaded += OnLoaded;
        }

        public TitleBarHelper TitleHelper
        {
            get
            {
                return TitleBarHelper.Instance;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(typeof(MaterialPage));
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            
            if (args.IsSettingsInvoked)
            {
                RootFrame.Navigate(typeof(SettingsPage));
                return;
            }

            switch (args.InvokedItem as string)
            {
                case "Depth":
                    RootFrame.Navigate(typeof(DepthPage));
                    break;
                case "Light":
                    RootFrame.Navigate(typeof(LightPage));
                    break;
                case "Material":
                    RootFrame.Navigate(typeof(MaterialPage));
                    break;
                case "Motion":
                    RootFrame.Navigate(typeof(MotionPage));
                    break;
                case "Scale":
                    RootFrame.Navigate(typeof(ScalePage));
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
                    //((NavigationViewItem)NavigationView.SettingsItem).IsSelected = true;
                    break;
            }
            DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = RootFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            });
        }


        private void OnNavigationViewIsPaneOpenPropertyChanged(DependencyObject sender, DependencyProperty dp)
        {
            TitleBarHelper.Instance.UpdateTitlePosition();
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
