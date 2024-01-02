using ImageDuplicateFinder.Helper;
using ImageDuplicateFinder.Models;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageDuplicateFinder.Views;

public sealed partial class FindDuplicatesPage : Page
{
    public FindDuplicatesPage()
    {
        this.InitializeComponent();
    }

    public async Task<Dictionary<string, DuplicateImageItem>> FindDuplicates(string path)
    {
        var images = FindDuplicateHelper.GetImagesFromPath(path);

        searchProgress.TotalImageCount = images.Length;
        Dictionary<string, DuplicateImageItem> imagesList = new Dictionary<string, DuplicateImageItem>();

        await Task.Run(() =>
        {
            int foundDuplicates = 0;

            foreach (string imagePath in images)
            {
                var hash = FindDuplicateHelper.CalculateSHA256(imagePath);

                searchProgress.DispatcherQueue.TryEnqueue(() =>
                {
                    searchProgress.CurrentImageIndex++;
                    searchProgress.CurrentImagePath = imagePath;
                    searchProgress.DuplicatesFound = foundDuplicates;
                });

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