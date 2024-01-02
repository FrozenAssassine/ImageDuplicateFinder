using Microsoft.UI.Xaml.Controls;

namespace ImageDuplicateFinder.Views
{ 
    public sealed partial class CompareImagesView : Page
    {
        public CompareImagesView()
        {
            this.InitializeComponent();
        }

        public GridView CompareImageGrid => compareImageGrid;
        public GridView DuplicatesGridView => duplicatesGridView;
    }
}
