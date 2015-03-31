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

namespace Project2_Cs
{


    //<summary>
    //This is the implementaion of controller.
    //It conducts the application by trasferring data and signals between Model and View.
    //</summary>

    class PredictionAppController : Controller
    {
        View view;
        Model model;


        public PredictionAppController(View view, Model model)
        {
            this.view = view;
            this.model = model;
            this.view.setController(this);
            this.view.showView();
        }


        public void newTextHandler(string button_pressed, bool same_key)
        {
            //Console.WriteLine(button_pressed);
            //Console.WriteLine(same_key);
            model.changeText(button_pressed, same_key);
            view.changeText(model.getText());
        }

        public void setMode(bool mode)
        {
            //Console.WriteLine(mode);
            model.setMode(mode);

        }

        public void nextWordinPredictive()
        {

            model.updateText_predictive();
            view.changeText(model.getText());


        }

        public void deleteChar()
        {
            model.handle_delete_char();
            view.changeText(model.getText());
        }

        public void insertSpace()
        {
            model.handle_space_char();
            view.changeText(model.getText());
        }

    }

}