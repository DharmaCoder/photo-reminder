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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ReminderProgram
{
    /// <summary>
    /// Interaction logic for ReminderWindow.xaml
    /// </summary>
    public partial class ReminderWindow : Window
    {
        //TODO - make timespans customizable maybe?

        //short timespans for testing purposes - change later
        private TimeSpan isShowing = TimeSpan.FromSeconds(3);
        private TimeSpan isHidden = TimeSpan.FromSeconds(3);

        private int currentPhrase = 10;
        private DispatcherTimer showTimer;

        List<string> userPhrases;


        public ReminderWindow()
        {
            InitializeComponent();
            userPhrases = ((MainWindow)Application.Current.MainWindow).Phrases;
        }

        private void userImage_Loaded(object sender, RoutedEventArgs e)
        {
            //accessing value of image path from MainWindow
            string testPath = ((MainWindow)Application.Current.MainWindow).ImagePath;
            try
            {
                userImage.Source = new BitmapImage(new Uri(testPath));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var screenWidth = SystemParameters.VirtualScreenWidth;
            Left = screenWidth - 425;
            Top = 50;

            Visibility = Visibility.Collapsed;

            showTimer = new DispatcherTimer();
            showTimer.Interval = TimeSpan.FromSeconds(5);
            showTimer.Tick += charTimer_Tick;
            showTimer.Start();

        }

        void charTimer_Tick(object sender, EventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Visibility = Visibility.Collapsed;
                showTimer.Interval = isHidden;
            }
            else
            {
                phrasesTextBlock.Text = GetNextPhrase();
                Visibility = Visibility.Visible;
                showTimer.Interval = isShowing;
            }
        }

        private void phrasesTextBlock_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private string GetNextPhrase()
        {
            currentPhrase++;
            if (currentPhrase > userPhrases.Count - 1)
            {
                currentPhrase = 0;
            }

            return userPhrases[currentPhrase];
        }
    }
}
