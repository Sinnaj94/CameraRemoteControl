using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media;



namespace FernauslöserApp
{
    
    public partial class MainPage : PhoneApplicationPage
    {
        
        public int zaehler = 0;
        public int timelapsezaehler = 0;
        DispatcherTimer timer;
        int a;
        
        int frames = 0;
        double fps;
        
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            updateFPS();
            fillListBox();
            
        }

        private void fillListBox()
        {
            //die erste zahl existiert schon, damit es nicht leer ist
            for (int i = 2; i <= 1000; i++)
            {
                listBoxZahlen.Items.Add(i);
            }
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void fotomachen()
        {
           
            rl.Stop();
            rl.Play();
            zaehler++;
            this.zaehlfeld.Text = zaehler + "";
        }

        private void timelapsefoto()
        {
            timelapsezaehler += 1;
            
           rl.Play();

        }

        private void delayfoto()
        {
            
            zs.Stop();
            zs.Play();
            zaehler++;
            this.zaehlfeld.Text = zaehler + "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fotomachen();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            delayfoto();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (startStop.Content.Equals("Start"))
            {
                //startStop.SetValue(BackgroundProperty, "#FFFFFFFF");
                timerErstellen();
                startStop.Content = "Stop";
                Info.Visibility = Visibility.Visible;
            }
            else
            {
               // startStop.SetValue(BackgroundProperty, "#FF00AA18");
                timer.Stop();
                timelapsezaehler = 0;
                startStop.Content = "Start";
                zaehlfeldTimelapse.Text = timelapsezaehler + "";
                Info.Visibility = Visibility.Collapsed;
                
            }
        }

        private void timerErstellen()
        {
            //timer um fotos zu machen
            timer = new DispatcherTimer();
            
            if (timer.IsEnabled) timer.Stop(); else timer.Start();
            setIntervall();

            //funktion timer
            a = int.Parse(anzahlTimelapse.Text);
            Info.Text = "Noch verbleibend: " + (a - timelapsezaehler);
            timer.Tick += new EventHandler(timerTickt);
            

            //timer um zeit anzuzeigen
            
           

            
            
            
                
        }

        private void timerTickt(object sender, EventArgs e)
        {
            if (timelapsezaehler + 1 <= a)
            {
                timelapsefoto();

            }
            else
            {
                timelapsezaehler = 0;
                timer.Stop();
                startStop.Content = "Start";
                Info.Visibility = Visibility.Collapsed;
            }
            Info.Text = "Noch verbleibend: " + (a - timelapsezaehler);
            zaehlfeldTimelapse.Text = timelapsezaehler + "";

        }

        private void setIntervall()
        {
            int index = listBoxZeitSchema.SelectedIndex+1;
           // int b = listBoxZahlen.SelectedItem;
            int b = listBoxZahlen.SelectedIndex + 1;
            
            
            
            switch (index)
            {
                case 1:
                    timer.Interval = TimeSpan.FromMilliseconds(b);
                    break;
                case 2:
                    timer.Interval = TimeSpan.FromSeconds(b);
                    break;
                case 3:
                    timer.Interval = TimeSpan.FromMinutes(b);
                    break;
                case 4:
                    timer.Interval = TimeSpan.FromHours(b);
                    break;
                case 5:
                    timer.Interval = TimeSpan.FromDays(b);
                    break;

      
            }
            

        }

        private void anzahlTimelapse_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateFPS();
            
        }

        private void updateFPS()
        {
            if (anzahlTimelapse.Text != "")
            {
                frames = int.Parse(anzahlTimelapse.Text);
            }
            else
            {
                frames = 0;
            }
            fps = 24;
            double sekunden = frames / fps;
            InfoDauer.Text = getDauerText(sekunden) + " (" + fps + "fps.)";
            
           
            
        }

        private String getDauerText(double sekunden)
        {
            if (sekunden == 0)
            {
                return "Keine Dauer.";
            }
            String e = "";
            
            int stunden = 0;
            int minuten = 0;
            
           
            while (sekunden >= 3600)
            {
                stunden++;
                sekunden -= 3600;
            }
            while (sekunden >= 60)
            {
                minuten++;
                sekunden -= 60;
            }
            if (sekunden != 0)
            {
                e = Math.Round(sekunden,2) + "s " + e;
            }

            if (minuten != 0)
            {
                e = minuten + "m " + e;
            }
            if (stunden != 0)
            {
                e = stunden + "h " + e;
            }
            return e;
            
        }

        private void listBoxHersteller_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
           
        }
    }
}