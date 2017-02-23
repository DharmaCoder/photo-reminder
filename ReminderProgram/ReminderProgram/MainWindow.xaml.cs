using Microsoft.Win32; // for open file dialog
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ReminderProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //TODO - add sound effect option
        //TODO - add options for time intervals
        //TODO - Add a remove phrases Button
        //TODO - Add stop button
        //TODO - clean up garbage code (mvvm)

        private string imagePath;
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                ImagePath = value;
            }
        }

        //list of phrases from listbox
        private List<string> phrases = new List<string>();
        public List<string> Phrases
        {
            get
            {
                return phrases;
            }
            set
            {
                Phrases = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        //adds user phrases to listbox and saves listbox items to phrases list
        private void addPhraseButton_Click(object sender, RoutedEventArgs e)
        {
            if (addPhraseTextBox.Text != "")
            {
                phrasesListBox.Items.Add(addPhraseTextBox.Text);
                phrases.Add(addPhraseTextBox.Text);
                addPhraseTextBox.Clear();
                addPhraseTextBox.Focus();
            }
            else
            {
                MessageBox.Show("Please type a phrase!");
            }
        }

        private void addPictureButton_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Filter = "ImageFiles (*.bmp; *.jpg; *.jpeg,*.png)| *.BMP; *.JPG; *.JPEG; *.PNG";
            if (openFD.ShowDialog() == true)
            {
                imagePath = openFD.FileName;
                imagePathLabel.Content = imagePath;
                userPhotoBox.Source = new BitmapImage(new Uri(imagePath));
                
                //copy image to photo folder
                //System.IO.File.Copy(imagePath, )
            }
            else
            {

            }
        }

        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (imagePath != null && phrases.Count != 0)
            {
                ReminderWindow reminderWindow = new ReminderWindow();
                reminderWindow.Show();

                //prevent user from creating multiple windows
                if (reminderWindow.IsActive)
                {
                    startButton.IsEnabled = false;
                }
            }
            else
            {
                MessageBox.Show("Must enter a phrase and a photo!");
            }
        }
    }
}