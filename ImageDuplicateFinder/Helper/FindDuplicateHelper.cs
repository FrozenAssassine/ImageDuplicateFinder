using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ImageDuplicateFinder.Helper;

internal class FindDuplicateHelper
{
    public static async Task<List<string>> GetImagesFromPath(string rootPath)
    {
        var imageTypes = new[] { "*.jpg", "*.jpeg", "*.png", "*.gif", "*.bmp" }; // Add more image types as needed
        var allImages = new List<string>();

        try
        {
            await Task.Run(() =>
            {
                ProcessFolder(rootPath, imageTypes, allImages);
            });
        }
        catch (Exception ex)
        {
        }
        return allImages;
    }

    private static void ProcessFolder(string folderPath, string[] imageTypes, List<string> allImages)
    {
        try
        {
            var folderImages = new List<string>();

            foreach (var type in imageTypes)
            {
                folderImages.AddRange(Directory.GetFiles(folderPath, type));
            }

            allImages.AddRange(folderImages);

            foreach (var subfolder in Directory.GetDirectories(folderPath))
            {
                ProcessFolder(subfolder, imageTypes, allImages);
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            Debug.WriteLine($"Error in folder '{folderPath}': {ex.Message}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in folder '{folderPath}': {ex.Message}");
        }
    }

    public static string CalculateSHA256(string filePath)
    {
        using (var sha256 = SHA256.Create())
        {
            using (var stream = File.OpenRead(filePath))
            {
                byte[] hash = sha256.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }
    }
}