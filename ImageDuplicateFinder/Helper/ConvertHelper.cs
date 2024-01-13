using Microsoft.UI.Xaml;

namespace ImageDuplicateFinder.Helper;

internal class ConvertHelper
{
    public static Visibility BoolToVisibility(bool visible)
    {
        return visible ? Visibility.Visible : Visibility.Collapsed;
    }
}
