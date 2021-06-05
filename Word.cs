using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Llama_Search_Alpha
{
    [Serializable]
    public class Word
    {
        private string name;
        private string category;
        private string description;
        private string imagePath;

        public Word()
        {

        }

        public Word(string name, string category, string description, string imagePath)
        {
            this.name = name;
            this.category = category;
            this.description = description;
            this.imagePath = imagePath;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        
        public string Category
        {
            get
            {
                return this.category;
            }
            set
            {
                this.category = value;
            }
        }
        
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        public string ImagePath
        {
            get
            {
                return this.imagePath;
            }
            set
            {
                this.imagePath = value;
            }
        }
    }
}
