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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace FluentDesignSystemSample.Views.ConnectedAnimations
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ConnectedAnimations3rdPage : Page
    {

        public ConnectedAnimations3rdPage()
        {
            this.InitializeComponent();
        }

        public Item Item { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Item = e.Parameter as Item;
            base.OnNavigatedTo(e);
        }
    }
}
