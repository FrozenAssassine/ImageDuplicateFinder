using System;
using System.IO;
using System.Security.Cryptography;

namespace ImageDuplicateFinder.Helper;

internal class FindDuplicateHelper
{
    public static string[] GetImagesFromPath(string rootPath)
    {
        return Directory.GetFiles(rootPath, "*.jpg", SearchOption.AllDirectories);
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