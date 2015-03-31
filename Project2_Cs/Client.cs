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

//
// <summary>
// This file is starting point of this application.
// It creates the instance of Model,View and Controller
// and does the wiring of the MVC comuunication.
// </summary>
//


namespace Project2_Cs
{


    //
    // <summary>
    //Abstraction for the View.
    //The View instance should implement this and 
    //define the methods for communication.
    //</summary>
    public interface View
    {
        void setController(Controller c);
        void showView();
        void changeText(String text);
    }


    //
    // <summary>
    //Abstraction for the Controller.
    //Controller instances should implement this and 
    //define the methods for communication.
    //</summary>

    public interface Controller
    {
        void newTextHandler(String button_pressed, bool time_elapsed);
        void setMode(bool mode);

        void nextWordinPredictive();
        void deleteChar();

        void insertSpace();
    }


    //
    //<summary> 
    //Abstraction for the Model.
    //Model instance should implement this and 
    //define the methods for communication.
    //</summary>
    public interface Model
    {

        void changeText(String button_pressed, bool same_key);
        String getText();
        void setMode(bool mode);
        void updateText_predictive();
        void handle_delete_char();

        void handle_space_char();
    }


    //<summary>
    //This is the starting point of the application.
    // This class creates the Model-View-Controller instances. 
    // Controller then manages the application after this. 
    // </summary>
    //
    class Client
    {
        public Client()
        {
            View v = new MainWindow();
            Model m = new PredictionAppModel();
            Controller c = new PredictionAppController(v, m);

        }
    }
}
