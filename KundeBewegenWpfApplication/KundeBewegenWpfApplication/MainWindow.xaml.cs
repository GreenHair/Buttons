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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KundeBewegenWpfApplication
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AnimationClock _taktgeber = null;
        private bool wechsel = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bewegung_Click(object sender, RoutedEventArgs e)
        {  // der Start der Bewegung soll auf Click erfolgen
            
            // Bewegungsparameter festlegen
            DoubleAnimation kundeBewegung = new DoubleAnimation
            {
                From = 0,
                To = 80,
                Duration = TimeSpan.Parse("0:0:5")
            };            
            _taktgeber = kundeBewegung.CreateClock();
            kunde.ApplyAnimationClock(Canvas.TopProperty, _taktgeber);
            _taktgeber.Completed += taktgeberCompleted;    // event, der am Ende einmal ....
            _taktgeber.CurrentTimeInvalidated += taktgeberTickt;           
            _taktgeber.Completed += rechtsAbbiegen;
        }

        private void rechtsAbbiegen(object sender, EventArgs e)
        {            
           DoubleAnimation nachRechts = new DoubleAnimation
           {
                From = 0,
                To = 750,
                Duration = TimeSpan.Parse("0:0:10")
            };
            nachRechts.Completed += weiterRunter;
            kunde.BeginAnimation(Canvas.LeftProperty, nachRechts);                      
        }

        private void weiterRunter(object sender, EventArgs e)
        {                        
            double start;
            Double.TryParse(kunde.GetValue(Canvas.TopProperty).ToString(), out start);
            if(start >= 400)
            {
                MessageBox.Show("Fertig mit einkaufen");
            }
            else
            {
                DoubleAnimation kundeBewegung = new DoubleAnimation
                {
                    From = start,
                    To = start + 80,
                    Duration = TimeSpan.Parse("0:0:5")
                };

                if (wechsel)
                {
                    kundeBewegung.Completed -= rechtsAbbiegen;
                    kundeBewegung.Completed += linksAbbiegen;                    
                }
                else
                {
                    kundeBewegung.Completed += rechtsAbbiegen;
                    kundeBewegung.Completed -= linksAbbiegen;
                }
                wechsel = !wechsel;
                kunde.BeginAnimation(Canvas.TopProperty, kundeBewegung);               
            }            
        }

        private void linksAbbiegen(object sender, EventArgs e)
        {
            DoubleAnimation nachLinks = new DoubleAnimation
            {
                From = 750,
                To = 0,
                Duration = TimeSpan.Parse("0:0:10")
            };
            nachLinks.Completed += weiterRunter;
            kunde.BeginAnimation(Canvas.LeftProperty, nachLinks);
        }

        private void taktgeberCompleted(object sender, EventArgs e)
        {
            bewegung.Content = "angekommen";
        }

        private void taktgeberTickt(object sender, EventArgs e)
        {
            bewegung.Content = kunde.GetValue(Canvas.TopProperty); //_taktgeber.CurrentTime.ToString();
            double aktuellePosition;
            Double.TryParse((kunde.GetValue(Canvas.TopProperty).ToString()),out aktuellePosition);
            if(aktuellePosition > 200.0)
            {
                bewegung.Content="halbe Strecke geschafft";               
            }
            if (aktuellePosition > 210.0)
            {
                bewegung.Content = kunde.GetValue(Canvas.TopProperty);
            }
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            _taktgeber.Controller.Begin();
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            _taktgeber.Controller.Pause();
        }

        private void weiter_Click(object sender, RoutedEventArgs e)
        {
            _taktgeber.Controller.Resume();
        }

        private void ende_Click(object sender, RoutedEventArgs e)
        {
            _taktgeber.Controller.SkipToFill();
        }

        private void liste_Click(object sender, RoutedEventArgs e)
        {
            _taktgeber.Controller.Pause();
            MessageBox.Show("Anzeige der Einkaufsliste");
            _taktgeber.Controller.Resume();
        }

        private void kunde_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _taktgeber.Controller.Pause();
            MessageBox.Show("Anzeige der Einkaufsliste");
            _taktgeber.Controller.Resume();

        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            speedration.Content = e.NewValue;
            if (_taktgeber != null)
            {
                var bindung = new Binding();
                bindung.Source = _taktgeber.Controller;
                bindung.Path = new PropertyPath("SpeedRatio");
                slider.SetBinding(Slider.ValueProperty, bindung);
            }
        }
    }
}
