using ImageDuplicateFinder.Models;
using ImageDuplicateFinder.Views;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageDuplicateFinder.Dialogs
{
    internal class FindDuplicatesDialog
    {
        public async Task<Dictionary<string, DuplicateImageItem>> ShowAsync(string path)
        {
            var page = new FindDuplicatesPage();
            var dialog = new ContentDialog
            {
                Title = "Find Duplicates",
                CloseButtonText = "Cancel",
                XamlRoot = App.m_window.Content.XamlRoot,
                Content = page
            };
            dialog.ShowAsync();
            var res = await page.FindDuplicates(path);
            return res;
        }
    }
}
