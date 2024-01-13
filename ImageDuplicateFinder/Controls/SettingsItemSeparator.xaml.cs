using Microsoft.UI.Xaml.Controls;

namespace ImageDuplicateFinder.Controls;

public sealed partial class SettingsItemSeparator : UserControl
{
    public SettingsItemSeparator()
    {
        this.InitializeComponent();
    }

    public string Header { get; set; }
}
