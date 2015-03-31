/*Author : Raghuveer Sagar
 *
 *Date   : 3/16/2015 
 * 
 *Version: v1.0 
 * 
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Project2_Cs
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// Here we create an instance of the Client class
    /// </summary>
    /*public partial class App : Application
    {
    }*/

    public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      new Client();
    }
  }
}

