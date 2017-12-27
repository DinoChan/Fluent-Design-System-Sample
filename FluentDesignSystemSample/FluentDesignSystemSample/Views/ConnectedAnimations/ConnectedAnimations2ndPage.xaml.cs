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
using Autofac;
using FluentDesignSystemSample.Services;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace FluentDesignSystemSample.Views.ConnectedAnimations
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ConnectedAnimations2ndPage : Page
    {
        private static List<Item> items;

        public ConnectedAnimations2ndPage()
        {
            this.InitializeComponent(); if (items == null)
            {
                items = new List<Item>
                {
                    new Item() { Title = "Test 1" },
                    new Item() { Title = "Test 2" },
                    new Item() { Title = "Test 3" },
                    new Item() { Title = "Test 4" },
                    new Item() { Title = "Test 5" },
                    new Item() { Title = "Test 6" },
                    new Item() { Title = "Test 7" },
                    new Item() { Title = "Test 8" },
                    new Item() { Title = "Test 9" },
                    new Item() { Title = "Test 10" },
                    new Item() { Title = "Test 11" },
                    new Item() { Title = "Test 12" },
                    new Item() { Title = "Test 13" },
                    new Item() { Title = "Test 14" },
                    new Item() { Title = "Test 15" },
                    new Item() { Title = "Test 16" },
                    new Item() { Title = "Test 17" },
                    new Item() { Title = "Test 18" }
                };
            }

            listView.ItemsSource = items;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var scope = App.Container.BeginLifetimeScope())
            {
                var navigationService = scope.Resolve<INavigationService>();
                navigationService.NavigateToPage<ConnectedAnimations3rdPage>(e.ClickedItem);
            }
        }
    }

    public class Item
    {
        public string Title { get; set; }
    }
}
