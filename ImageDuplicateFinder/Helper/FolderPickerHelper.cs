using System;
using System.Threading.Tasks;
using Windows.Storage.Pickers;

namespace ImageDuplicateFinder.Helper
{
    internal class FolderPickerHelper
    {
        public static async Task<(string path, bool success)> PickFolder()
        {
            FolderPicker openPicker = new FolderPicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

            var folder = await openPicker.PickSingleFolderAsync();
            if (folder == null)
                return (null, false);
            return (folder.Path, true);
        }
    }
}
