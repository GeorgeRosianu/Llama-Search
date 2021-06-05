using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Llama_Search_Alpha
{
    public class WordsInformation
    {
        private static WordsInformation wordsInformation;

        private Dictionary<string, Word> wordsDictionary;
        private BinaryFormatter formatter;

        private const string DATA_FILENAME = "wordsinformation.dat";

        public static WordsInformation Instance()
        {
            if (wordsInformation == null)
            {
                wordsInformation = new WordsInformation();
            }

            return wordsInformation;
        }

        private WordsInformation()
        {
            this.wordsDictionary = new Dictionary<string, Word>();
            this.formatter = new BinaryFormatter();
        }

        public void AddWord(string name, string category, string description, string imagePath)
        {
            if (this.wordsDictionary.ContainsKey(name))
            {
                MessageBox.Show("You had already added " + name + " before.");
            }
            else
            {
                this.wordsDictionary.Add(name, new Word(name, category, description, imagePath));
                MessageBox.Show("Word added successfully.");
            }
        }

        public void RemoveWord(string name)
        {
            if (!this.wordsDictionary.ContainsKey(name))
            {
                MessageBox.Show(name + " had not been added before.");
                this.wordsDictionary.Remove(name);
            }
            else
            {
                if (this.wordsDictionary.Remove(name))
                {
                    MessageBox.Show(name + " had been removed successfully.");
                }
                else
                {
                    MessageBox.Show("Unable to remove " + name);
                }
            }
        }

        public void Save()
        {
            try
            {
                FileStream writerFileStream = new FileStream(DATA_FILENAME, FileMode.Create, FileAccess.Write);
                this.formatter.Serialize(writerFileStream, this.wordsDictionary);
                writerFileStream.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to save.");
            }
        }

        public void Load()
        {
            if (File.Exists(DATA_FILENAME))
            {
                try
                {
                    FileStream readerFileStream = new FileStream(DATA_FILENAME, FileMode.Open, FileAccess.Read);
                    this.wordsDictionary = (Dictionary<String, Word>)this.formatter.Deserialize(readerFileStream);
                    readerFileStream.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("There seems to be a file that contains words information but somehow there is a problem with reading it.");
                }
            }
        }

        public Word GetWord(string name)
        {
            if (wordsDictionary.ContainsKey(name))
                return wordsDictionary[name];

            return null;
        }

        public List<string> getCategories()
        {
            List<string> categories = new List<string>();

            if (File.Exists(DATA_FILENAME))
            {
                try
                {
                    FileStream readerFileStream = new FileStream(DATA_FILENAME, FileMode.Open, FileAccess.Read);
                    this.wordsDictionary = (Dictionary<String, Word>)this.formatter.Deserialize(readerFileStream);
                    readerFileStream.Close();

                    foreach (Word word in wordsDictionary.Values)
                    {
                        if (!categories.Contains(word.Category))
                            categories.Add(word.Category);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("There seems to be a file that contains words information but somehow there is a problem with reading it.");
                }
            }
            return categories;
        }

        public List<string> getNames()
        {
            List<string> names = new List<string>();

            if (File.Exists(DATA_FILENAME))
            {
                try
                {
                    FileStream readerFileStream = new FileStream(DATA_FILENAME, FileMode.Open, FileAccess.Read);
                    this.wordsDictionary = (Dictionary<String, Word>)this.formatter.Deserialize(readerFileStream);
                    readerFileStream.Close();

                    foreach (Word word in wordsDictionary.Values)
                    {
                        if (!names.Contains(word.Name))
                            names.Add(word.Name);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("There seems to be a file that contains words information but somehow there is a problem with reading it.");
                }
            }
            return names;
        }
    }
}
