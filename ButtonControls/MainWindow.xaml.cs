using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ButtonControls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //button in C# setzen
            //ToggleButton k2 = new ToggleButton { Content = "k2" };
            //Grid.SetColumn(k2, 1);
            //Grid.SetRow(k2, 1);
            //grd.Children.Add(k2);
            //Label lbl_btn = new Label() { Name = "lbl_btn", Content = "Vor dem click", VerticalContentAlignment = VerticalAlignment.Center };
            //Grid.SetColumn(lbl_btn, 1);
            //grd.Children.Add(lbl_btn);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lbl_btn.Content = "Button wurde geclickt";
            //ThicknessAnimation pressed = new ThicknessAnimation();
            //pressed.To = new Thickness(16, 39, 14, 37);
            //pressed.AutoReverse = true;
            //pressed.Duration = TimeSpan.Parse("0:0:0.1");
            //simpleButton.BeginAnimation(MarginProperty, pressed);
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            tgl_btn.Content = "Toggle gecklickt";
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            tgl_aktiv.Content = "Aktiv";
            tgl_inaktiv.Content = "";
            Image content = new Image();
            content.Source = new BitmapImage(new Uri("C:\\csharpWPF\\ButtonControls\\ButtonControls\\Properties\\Smiling_Emoji_with_Eyes_Opened.png"));
            toggle.Content = content;
            repeatUp.Click -= repeatUp_Click; repeatUp.Click += repeatDown_Click;
            repeatDown.Click += repeatUp_Click; repeatDown.Click -= repeatDown_Click;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            tgl_inaktiv.Content = "Nicht Aktiv";
            tgl_aktiv.Content = "";
            Image content = new Image();
            content.Source = new BitmapImage(new Uri("C:\\csharpWPF\\ButtonControls\\ButtonControls\\Properties\\Crying_Face_Emoji.png"));
            toggle.Content = content;
            repeatUp.Click += repeatUp_Click; repeatUp.Click -= repeatDown_Click;
            repeatDown.Click -= repeatUp_Click; repeatDown.Click += repeatDown_Click;
        }
        
        private void repeatUp_Click(object sender, RoutedEventArgs e)
        {
            rpt_btn.Content = Convert.ToInt32(rpt_btn.Content)+1;
        }

        private void repeatDown_Click(object sender, RoutedEventArgs e)
        {
            rpt_btn.Content = Convert.ToInt32(rpt_btn.Content) - 1;
        }
        
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            simpleButton.Background = Brushes.LightBlue;
        }

        private void simpleButton_MouseLeave(object sender, MouseEventArgs e)
        {
            simpleButton.Background = new SolidColorBrush(Color.FromArgb(255,00,104,204));
        }

        private void simpleButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            simpleButton.Margin = new Thickness(16, 41, 13, 35);
            simpleButton.ClearValue(EffectProperty);
        }

        private void simpleButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            simpleButton.Margin = new Thickness(14, 38, 15, 38);
            DropShadowEffect shade = new DropShadowEffect();
            shade.Color = Colors.Black;
            shade.BlurRadius = 5;
            shade.Direction = 315;
            shade.ShadowDepth = 5;
            shade.RenderingBias = RenderingBias.Performance;
            simpleButton.Effect = shade;
        }
    }
}
