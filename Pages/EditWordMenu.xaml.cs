using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Llama_Search_Alpha.Pages
{
    /// <summary>
    /// Interaction logic for EditWordMenu.xaml
    /// </summary>
    public partial class EditWordMenu : UserControl
    {
        private string _searchText;

        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;

                OnPropertyChanged("SearchText");
                OnPropertyChanged("MyFilteredItems");
            }
        }

        public List<string> MyItems { get; set; }

        public IEnumerable<string> MyFilteredItems
        {
            get
            {
                if (SearchText == null) return MyItems;

                return MyItems.Where(x => x.ToUpper().StartsWith(SearchText.ToUpper()));
            }
        }

        public EditWordMenu()
        {
            InitializeComponent();

            WordsInformation wi = WordsInformation.Instance();
            MyItems = new List<string>(wi.getNames());

            this.DataContext = this;
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                wordImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void btnEditWord_Click(object sender, RoutedEventArgs e)
        {

            WordsInformation wi = WordsInformation.Instance();
            wi.Load();
            wi.RemoveWord(names.Text);
            try
            {
                SaveControlImage(wordImage, txtWord.Text + ".png");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            wi.AddWord(txtWord.Text, categories.Text, txtDesc.Text, txtWord.Text + ".png");
            wi.Save();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void names_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                names.Text = "";
            }
            else if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                WordsInformation wi = WordsInformation.Instance();
                Word word = wi.GetWord(names.Text);

                txtWord.Text = word.Name;
                categories.Text = word.Category;
                txtDesc.Text = word.Description;
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        private void SaveControlImage(FrameworkElement control, string filename)
        {
            Rect rect = VisualTreeHelper.GetDescendantBounds(control);

            DrawingVisual dv = new DrawingVisual();

            using (DrawingContext ctx = dv.RenderOpen())
            {
                VisualBrush brush = new VisualBrush(control);
                ctx.DrawRectangle(brush, null, new Rect(rect.Size));
            }

            int width = (int)control.ActualWidth;
            int height = (int)control.ActualHeight;
            RenderTargetBitmap rtb = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(dv);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                encoder.Save(fs);
            }
        }

        private void categories_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            WordsInformation wi = WordsInformation.Instance();
            data = wi.getCategories();
            categories.ItemsSource = data;
        }

        private void names_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            WordsInformation wi = WordsInformation.Instance();
            data = wi.getNames();
            names.ItemsSource = data;
        }
    }
}
