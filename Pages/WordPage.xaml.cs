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
    /// Interaction logic for WordPage.xaml
    /// </summary>
    public partial class WordPage : UserControl
    {
        public WordPage(Word word)
        {
            InitializeComponent();

            if(word == null)
            {
                MessageBox.Show("word is null");
            }
            else
            {
                wordName.Text = word.Name;
                wordCategory.Text = word.Category;
                wordDescription.Text = word.Description;
            }
        }
    }
}
