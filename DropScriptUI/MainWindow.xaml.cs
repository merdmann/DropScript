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

namespace UIDropScript
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UIAbout about = null;

        private enum PageIdentifier { E_Run, E_Settings };
        private PageIdentifier current = PageIdentifier.E_Run;

        private IDictionary<PageIdentifier, Page> pages = new Dictionary<PageIdentifier, Page>();

        public MainWindow()
        {
            InitializeComponent();

            this.about = new UIAbout();

            this.pages[PageIdentifier.E_Run] = new UIRun();
            this.pages[PageIdentifier.E_Settings] = new UISettings();
            this.current = PageIdentifier.E_Run;

            mainWindow.Left = SystemParameters.VirtualScreenWidth - mainWindow.Width; ;
            mainWindow.Top = SystemParameters.VirtualScreenTop;

            topFrame.NavigationService.Navigate(pages[this.current]);
        }


        private void settingsMouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            this.current = PageIdentifier.E_Settings;

            topFrame.NavigationService.Navigate(pages[this.current]);
            this.Show();
        }

        private void aboutMouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            about.Show();
        }

        private void keyDownHandler(object sender, KeyEventArgs e)
        {
            Size s = new Size();

            /*
                switch( e.Key ) {
                    case Key.OemPlus:
                        s = this.adjustWindow(s, +10);
                        break;
                    
                    case Key.OemMinus:
                        s = this.co<untdown.adjustWindow(s, -10);
                        break;

                    default:
                        break;
                }
             */

            if (s.Width > 0)
            {
                mainWindow.Height = s.Height + 10;
                mainWindow.Width = s.Width + 20;
                mainWindow.Top = SystemParameters.VirtualScreenTop;
                mainWindow.Left = SystemParameters.VirtualScreenWidth - mainWindow.Width;

                //this.countdown.updateUI();
                mainWindow.Show();
            }

            e.Handled = true; 
        }

        private void lbExitMouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
