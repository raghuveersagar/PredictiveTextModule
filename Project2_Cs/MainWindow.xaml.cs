/*Author : Raghuveer Sagar
 *
 *Date   : 3/16/2015 
 * 
 *Version: v1.0 
 * 
 */

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
using System.Diagnostics;

namespace Project2_Cs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Implemtation of View interface.
    /// </summary>,
    public partial class MainWindow : Window, View
    {

        Controller ctlr;
        Stopwatch stopWatch = new Stopwatch();
        public MainWindow()
        {
            InitializeComponent();
        }


        public void setController(Controller c)
        {
            ctlr = c;
        }

        void showView()
        {
            Show();

        }


        void View.showView()
        {
            Show();
        }
        //<summary>
        //Handles number buttons for both predictive and 
        //non-predictive operations
        //</summary>
        private void numberPadClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            String presentClick = b.Tag.ToString();
            TimeSpan ts = TimeSpan.FromMilliseconds(1000);
            bool timer_running = stopWatch.IsRunning;
            if (timer_running)
            {
                stopWatch.Stop();
                ts = stopWatch.Elapsed;
            }
            ctlr.newTextHandler(presentClick, ts < (TimeSpan.FromMilliseconds(900)));
            if (timer_running)
                stopWatch.Reset();

            stopWatch.Start();
        }
        //<summary>
        //
        //Responsible changing the text 
        //on view.
        //</summary>
        public void changeText(String text)
        {
            UserText.Text = text;




        }
        //<summary>
        //Handles "*" click for cycling the predicted words.
        //
        public void numberPadClickT9Cycle(object sender, RoutedEventArgs e)
        {
            ctlr.nextWordinPredictive();

        }


        //<summary>
        //
        //Handles check box toggling.
        //</summary>
        public void checkboxHandle(object sender, RoutedEventArgs e)
        {
            bool flag = (sender as CheckBox).IsChecked.Value;
            ctlr.setMode(flag);


        }


        //<summary>
        //Handles "<" click for deleting words or characters.
        //</summary>
        public void deleteChar(object sender, RoutedEventArgs e)
        {
            ctlr.deleteChar();
        }

        //<summary>
        //Handles "#" click for inserting space.
        //</summary>
        public void insertspace(object sender, RoutedEventArgs e)
        {
            ctlr.insertSpace();
        }



    }
}
