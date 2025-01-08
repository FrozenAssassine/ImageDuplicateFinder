using ImageDuplicateFinder.Helper;
using ImageDuplicateFinder.Models;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ImageDuplicateFinder.Views;

public class CancellationRequest
{
    public bool Cancel { get; set; }
}

public sealed partial class FindDuplicatesPage : Page
{
    public FindDuplicatesPage()
    {
        this.InitializeComponent();
    }

    public async Task<Dictionary<string, DuplicateImageItem>> FindDuplicates(string path, CancellationRequest cancelRequest)
    {
        var images = await FindDuplicateHelper.GetImagesFromPath(path);
        recursiveScanInfo.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        searchProgress.Visibility = Microsoft.UI.Xaml.Visibility.Visible;

        searchProgress.TotalImageCount = images.Count;
        Dictionary<string, DuplicateImageItem> imagesList = new Dictionary<string, DuplicateImageItem>();

        int updateCounter = 0;
        int currentImageIndex = 0;

        int foundDuplicates = 0;
        int updateInterval = Math.Clamp(images.Count / 10, 1, 100);

        var task = new Task(() =>
        {
            foreach (string imagePath in images)
            {
                if (cancelRequest.Cancel)
                    return;

                var hash = FindDuplicateHelper.CalculateSHA256(imagePath);
                updateCounter++;
                currentImageIndex++;

                if (currentImageIndex + updateInterval >= images.Count)
                    updateInterval = 0;

                if (updateCounter > updateInterval)
                {
                    updateCounter = 0;
                    searchProgress.DispatcherQueue.TryEnqueue(() =>
                    {
                        searchProgress.CurrentImageIndex = currentImageIndex;
                        searchProgress.CurrentImagePath = imagePath;
                        searchProgress.DuplicatesFound = foundDuplicates;
                    });
                }

                if (imagesList.ContainsKey(hash))
                {
                    imagesList[hash].Path.Add(imagePath);
                    foundDuplicates++;
                }
                else
                    imagesList.Add(hash, new DuplicateImageItem { Path = new List<string> { imagePath } });
            }
        });

        return imagesList;
    }
}