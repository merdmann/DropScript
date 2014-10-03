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
using System.Drawing;
using System.Diagnostics;

using UIDropScript.Properties;
using UIDropScript.Helper;

namespace UIDropScript
{
    /// <summary>
    /// This is the user control provindig a drap and drop area.
    /// </summary>
    public partial class UIDropBox : UserControl
    {
        // class variables
        private static int index = 0;

        // instance variables
        private ApplicationData applData = null;
        private int id = -1;
        private Helper.Identifier label = null;

        enum TrafficLightState { E_NULL, E_RED, E_GREEN };

        private void SetTrafficLight(TrafficLightState s) {
            Bitmap bitmap = null;
            ImageBrush ib = new ImageBrush( );

            if (s == TrafficLightState.E_GREEN)
                bitmap = UIDropScript.Properties.Resources.empty_box;
            else
                bitmap = UIDropScript.Properties.Resources.filled_box;

            ib.ImageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()); // Image is an image source
            dropBox.Fill = ib;
        }

        #region constructors
        public UIDropBox()
        {
            this.id = index++;

            this.label = new Helper.Identifier("Label");
            this.label.Value = this.id.ToString();

            this.applData = ApplicationData.items[this.id];

            InitializeComponent();

            keyLabel.DataContext = this.label;

            if( this.applData.theScript == null)
                this.SetTrafficLight(TrafficLightState.E_RED);
            else
                this.SetTrafficLight(TrafficLightState.E_GREEN);
        }
        #endregion

        // call the about box
        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            base.OnGiveFeedback(e);
 
            if (e.Effects.HasFlag(DragDropEffects.Copy))
            {
                Mouse.SetCursor(Cursors.Cross);
            }
            else
            {
                Mouse.SetCursor(Cursors.No);
            }
            e.Handled = true;
        }

        // This handler is called in case we drop an object on the drop box.
        private void dropHandler(object sender, DragEventArgs e)
        {
            base.OnDrop(e);

            if (e.Data.GetDataPresent("FileName") && e.AllowedEffects.HasFlag(DragDropEffects.Copy))
            { 
                String[] files = (String[])e.Data.GetData(DataFormats.FileDrop);

                if (this.applData.theScript == null)
                {
                    this.applData.theScript = files[0];
                    this.SetTrafficLight(TrafficLightState.E_GREEN);
                }
                else
                {
                    foreach (String file in files)
                    {
                        System.Diagnostics.Process.Start("CMD.exe", "/C " + this.applData.theScript + " " + file);
                        Console.WriteLine("file: " + file);
                        Console.WriteLine("script:" + this.applData.theScript);
                    }

                    e.Effects = DragDropEffects.Copy;

                    this.SetTrafficLight(TrafficLightState.E_RED);
                }
            }
            e.Handled = true;
        }


        private void View_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void View_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Trace.WriteLine("The New command was invoked");
        }

        private void Reset_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Reset_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Trace.WriteLine("The New command was invoked");
        }
    }
}
