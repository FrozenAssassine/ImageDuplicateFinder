using ImageDuplicateFinder.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.IO;
using Windows.ApplicationModel;

namespace ImageDuplicateFinder;

public sealed partial class MainWindow : Window
{
    public bool ShowBackArrow { get => navigateBackButton.Visibility == Visibility.Visible; set => navigateBackButton.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
    public Frame MainFrame => navigationFrame;


    public MainWindow()
    {
        this.InitializeComponent();
        
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(titleBar);
        ShowBackArrow = false;
        Title = Package.Current.DisplayName;

        this.AppWindow.SetIcon(Path.Combine(Package.Current.InstalledLocation.Path, "Assets\\appicon.ico"));

        this.MainFrame.Navigate(typeof(CompareImagesView));
    }

    private void NavigateBack_Click(object sender, RoutedEventArgs e)
    {
        this.navigationFrame.GoBack();
    }
}