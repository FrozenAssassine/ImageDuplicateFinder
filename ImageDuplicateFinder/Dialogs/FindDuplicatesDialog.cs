using ImageDuplicateFinder.Models;
using ImageDuplicateFinder.Views;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ImageDuplicateFinder.Dialogs
{
    internal class FindDuplicatesDialog
    {
        public async Task<Dictionary<string, DuplicateImageItem>> ShowAsync(string path)
        {
            var page = new FindDuplicatesPage();
            var cancellationrequest = new CancellationRequest();
            var dialog = new ContentDialog
            {
                Title = "Find Duplicates",
                CloseButtonText = "Cancel",
                XamlRoot = App.m_window.Content.XamlRoot,
                Content = page
            };
            dialog.Closed += (sender, args) =>
            {
                if (args.Result == ContentDialogResult.Secondary)
                {
                    cancellationrequest.Cancel = true;
                }
            };

            dialog.ShowAsync();
            try
            {
                var images = await page.FindDuplicates(path, cancellationrequest);
                dialog.PrimaryButtonText = "Done";
                return images;
            }
            catch (OperationCanceledException)
            {

                return null;
            }
        }
    }
}
