using ImageDuplicateFinder.Dialogs;
using ImageDuplicateFinder.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageDuplicateFinder;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
    }

    private async void myButton_Click(object sender, RoutedEventArgs e)
    {
        var res = await new FindDuplicatesDialog().ShowAsync(pathTextbox.Text);
        var duplicatesList = res.Where(x => x.Value.Path.Count > 1).Select(x => x.Value).ToList();

        duplicatesGridView.ItemsSource = duplicatesList;       
    }

    private void duplicatesGridView_ItemClick(object sender, Microsoft.UI.Xaml.Controls.ItemClickEventArgs e)
    {
        var clickedDuplicate = e.ClickedItem as DuplicateImageItem;
        compareImageGrid.ItemsSource = clickedDuplicate.Path;
    }
}