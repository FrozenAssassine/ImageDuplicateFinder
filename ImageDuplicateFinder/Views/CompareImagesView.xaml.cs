using ImageDuplicateFinder.Dialogs;
using ImageDuplicateFinder.Helper;
using ImageDuplicateFinder.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImageDuplicateFinder.Views
{ 
    public sealed partial class CompareImagesView : Page
    {
        private List<DuplicateImageItem> duplicates;
        private bool IsImageSelected;

        private DuplicateImageItem clickedItem;

        public CompareImagesView()
        {
            this.InitializeComponent();
        }

        private void duplicatesGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            compareImageGrid.Items.Clear();
            clickedItem = e.ClickedItem as DuplicateImageItem;
            foreach(var item in clickedItem.Path)
            {
                compareImageGrid.Items.Add(item);
            }
        }

        private void DeleteSelected_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (duplicates == null)
                return;

            List<string> deleteItems = compareImageGrid.SelectedItems.Select(x=>x.ToString()).ToList();
            foreach (string path in deleteItems)
            {
                compareImageGrid.Items.Remove(path);
                duplicates[duplicatesGridView.SelectedIndex].Path.Remove(path);
                File.Delete(path);
            }

            if (compareImageGrid.Items.Count <= 1 && clickedItem != null)
            {
                duplicates.Remove(clickedItem);
                duplicatesGridView.ItemsSource = null;
                duplicatesGridView.ItemsSource = duplicates;
            }

            if(duplicates.Count <= 0)
            {
                duplicatesGridView.ItemsSource = null;
                compareImageGrid.Items.Clear();
            }
        }

        private async void FindDuplicates_Click(object sender, RoutedEventArgs e)
        {
            var res = await new FindDuplicatesDialog().ShowAsync(pathTextbox.Text);
            duplicates = res.Where(x => x.Value.Path.Count > 1).Select(x => x.Value).ToList();

            duplicatesGridView.ItemsSource = duplicates;
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            App.m_window.ShowBackArrow = true;
            App.m_window.MainFrame.Navigate(typeof(AboutPage));
        }

        private async void PickFolder_Click(object sender, RoutedEventArgs e)
        {
            var res = await FolderPickerHelper.PickFolder();
            if (!res.success)
                return;

            pathTextbox.Text = res.path;
        }

        private void compareImageGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            deleteButton.IsEnabled = compareImageGrid.SelectedItem != null;
        }
    }
}
