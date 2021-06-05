using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Llama_Search_Alpha.Pages
{
    /// <summary>
    /// Interaction logic for AddWordMenu.xaml
    /// </summary>
    public partial class AddWordMenu : UserControl
    {

        public AddWordMenu()
        {
            InitializeComponent(); 
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

        private void btnAddWord_Click(object sender, RoutedEventArgs e)
        {
            
            WordsInformation wi = WordsInformation.Instance();
            wi.Load();
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

        private void categories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
