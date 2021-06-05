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
    /// Interaction logic for Games.xaml
    /// </summary>
    public partial class Games : UserControl
    {
        public Games()
        {
            InitializeComponent();
            setWord();
        }

        private void btnNextWord_Click(object sender, RoutedEventArgs e)
        {
            setWord();
            txtWord.Text = "";
        }

        private void setWord()
        {
            List<string> data = new List<string>();
            List<Word> words = new List<Word>();
            WordsInformation wi = WordsInformation.Instance();
            wi.Load();
            data = wi.getNames();
            foreach (var item in data)
            {
                words.Add(wi.GetWord(item));
            }
            Random r = new Random();
            int rInt = r.Next(0, data.Count() - 1);
            txtDesc.Text = words.ElementAt(rInt).Description;
        }
    }
}
