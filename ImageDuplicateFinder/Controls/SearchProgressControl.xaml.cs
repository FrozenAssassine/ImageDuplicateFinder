using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

namespace ImageDuplicateFinder.Controls;

public sealed partial class SearchProgressControl : UserControl, INotifyPropertyChanged
{
    private int _TotalImageCount = 0;
    public int TotalImageCount { get => _TotalImageCount; set { _TotalImageCount = value; RaisePropertyChanged("ImagesOfImages"); RaisePropertyChanged("TotalImageCount"); } }
    
    private int _CurrentImageIndex = 0;
    public int CurrentImageIndex { get => _CurrentImageIndex; set { _CurrentImageIndex = value; RaisePropertyChanged("ImagesOfImages"); RaisePropertyChanged("CurrentImageIndex"); } }
    
    private string _CurrentImagePath = "";
    public string CurrentImagePath { get => _CurrentImagePath; set { _CurrentImagePath = value; RaisePropertyChanged("CurrentImagePath"); } }

    private int _DuplicatesFound = 0;
    public int DuplicatesFound { get => _DuplicatesFound; set { _DuplicatesFound = value; RaisePropertyChanged("DuplicatesFoundText"); } }

    public string DuplicatesFoundText => "Found: " + _DuplicatesFound + " Duplicates";
    public string ImagesOfImages => _CurrentImageIndex + "/" + _TotalImageCount;

    public SearchProgressControl()
    {
        this.InitializeComponent();
    }

    private void RaisePropertyChanged(string name)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
