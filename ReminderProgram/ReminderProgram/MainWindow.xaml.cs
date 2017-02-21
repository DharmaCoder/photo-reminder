using Microsoft.Win32; // for open file dialog
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
        //TODO - clean up garbage code

        private string imagePath;
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                ImagePath = imagePath;
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
                phrases = Phrases;
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
            try
            {
                OpenFileDialog openFD = new OpenFileDialog();
                openFD.Filter = "ImageFiles (*.bmp; *.jpg; *.jpeg,*.png)| *.BMP; *.JPG; *.JPEG; *.PNG";
                if (openFD.ShowDialog() == true)
                {
                    imagePath = openFD.FileName;
                    imagePathLabel.Content = imagePath;
                    userPhotoBox.Source = new BitmapImage(new Uri(imagePath));
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            }
            else
            {
                MessageBox.Show("Yeah quit trying to break shit.");
            }
        }
    }
}
