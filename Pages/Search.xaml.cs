using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : UserControl, INotifyPropertyChanged
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

        public Search()
        {
            InitializeComponent();

            WordsInformation wi = WordsInformation.Instance();
            MyItems = new List<string>(wi.getNames());

            this.DataContext = this;
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
                searchTB.Text = "";
            }
            else if(e.Key == Key.Return || e.Key == Key.Enter)
            {
                WordsInformation wi = WordsInformation.Instance();
                this.Content = new WordPage(wi.GetWord(searchTB.Text));
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        //private void names_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    searchTB.Text = names.SelectedItem.ToString();
        //}
    }
}
