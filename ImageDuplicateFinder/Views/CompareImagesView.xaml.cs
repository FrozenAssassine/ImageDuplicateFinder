using ImageDuplicateFinder.Dialogs;
using ImageDuplicateFinder.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.Appointments;

namespace ImageDuplicateFinder.Views
{ 
    public sealed partial class CompareImagesView : Page
    {
        private List<DuplicateImageItem> duplicates;

        public CompareImagesView()
        {
            this.InitializeComponent();
        }

        private void duplicatesGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            compareImageGrid.Items.Clear();
            var clickedDuplicate = e.ClickedItem as DuplicateImageItem;
            foreach(var item in clickedDuplicate.Path)
            {
                compareImageGrid.Items.Add(item);
            }
        }

        private void DeleteSelected_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            List<string> deleteItems = compareImageGrid.SelectedItems.Select(x=>x.ToString()).ToList();
            foreach (string path in deleteItems)
            {
                compareImageGrid.Items.Remove(path);
                duplicates[duplicatesGridView.SelectedIndex].Path.Remove(path);
                File.Delete(path);
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
    }
}
