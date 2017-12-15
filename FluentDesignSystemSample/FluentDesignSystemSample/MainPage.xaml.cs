using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void AppNavFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void Navview_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            //if (args.IsSettingsInvoked)
            //{
            //    _navigationService.NavigateToSettingsAsync();
            //    return;
            //}

            //switch (args.InvokedItem as string)
            //{
            //    case "Browse videos":
            //        _navigationService.NavigateToPodcastsAsync();
            //        break;
            //    case "Now playing":
            //        _navigationService.NavigateToNowPlayingAsync();
            //        break;
            //    case "Favorites":
            //        _navigationService.NavigateToFavoritesAsync();
            //        break;
            //    case "Notes":
            //        _navigationService.NavigateToNotesAsync();
            //        break;
            //    case "Downloads":
            //        _navigationService.NavigateToDownloadsAsync();
            //        break;
            //}
        }
    }
}
