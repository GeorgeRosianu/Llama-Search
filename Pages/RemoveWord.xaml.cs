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

namespace Llama_Search_Alpha.Pages
{
    /// <summary>
    /// Interaction logic for RemoveWord.xaml
    /// </summary>
    public partial class RemoveWord : UserControl
    {
        public RemoveWord()
        {
            InitializeComponent();
        }

        private void btnAddWord_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new AddWord();
        }

        private void btnEditWord_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new EditWord();
        }

        private void btnRemoveWord_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new RemoveWord();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            WordsInformation wi = WordsInformation.Instance();
            wi.Load();
            wi.RemoveWord(names.Text);
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
