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
    //This is implementaion of the Model Interface.
    //It has the logic of the application.
    //It handles both predictive and non-predictive  
    //modes.
    //The prediction is handled using a data structure called Trie.
    //</summary>

    class PredictionAppModel : Model
    {
        String[] labels = new String[] { "1", "2abc", "3def", "4ghi", "5jkl", "6mno", "7pqrs",
            "8tuv", "9wxyz" };
        int[] cycle_values = new int[] { 1, 4, 4, 4, 4, 4, 5, 4, 5 };
        char[][] buttons = new char[11][];
        List<String> predictions = new List<String>();
        IEnumerator<String> predictions_enumerator;
        String old_data = "";
        char new_char = ' ';
        bool Ispredictive = false;


        //Set the file name containing words here
        T9_Trie trie = T9_Trie.Build_Trie("ewords.txt");





        String current_prediction = "";
        StringBuilder complete_text = new StringBuilder("");
        String actual_T9_numbers_predictive = "";
        bool IspredictionListEmpty = true;
        public bool MODE
        {

            set
            {
                if (complete_text.Length > 0)
                    handle_space_char();

                Ispredictive = value;
            }

        }

        public String Text
        {

            get { return complete_text.ToString(); }

        }

        //<summary>
        //This method  cycle through the list
        //of predicted words
        //</summary>

        public String nextWord()
        {

            if (predictions_enumerator.MoveNext())
                return (current_prediction = predictions_enumerator.Current);
            else
            {
                predictions_enumerator = predictions.GetEnumerator();
                return nextWord();
            }

        }

        int cycle = 0;
        public PredictionAppModel()
        {
            foreach (String x in labels)
            {
                char[] temp = x.ToCharArray();
                buttons[temp[0] - '0'] = temp;


            }

        }

        public String getText()
        {


            return Text;
        }

        public void setMode(bool IsChecked)
        {
            MODE = IsChecked;

        }

        public void changeText(String data, bool same_key)
        {

            if (Ispredictive)
            {
                handlePredictive(data);
            }
            else
            {
                //Console.WriteLine("Non-predictive Mode");
                handleNonPredictive(data, same_key);
            }

        }

        public void handle_delete_char()
        {
            if (Ispredictive)
            {
                handle_delete_char_predictive();
            }
            else
            {
                handle_delete_char_non_predictive();

            }


        }

        public void handle_delete_char_non_predictive()
        {
            if (complete_text.ToString() == "")
                return;

            complete_text.Remove(complete_text.Length - 1, 1);


        }

        public void handle_delete_char_predictive()
        {

            if (complete_text.ToString() == "")
                return;
            char[] comp_temp = complete_text.ToString().ToCharArray();
            if (comp_temp[complete_text.Length - 1] == ' ')
            {
                int counter = complete_text.Length - 2;
                while (counter >= 0 && comp_temp[counter--] != ' ') ;
                complete_text.Remove(counter + 1, complete_text.Length - (counter + 1));
                if (complete_text.ToString() != "")
                    handle_space_char();

                return;
            }

            if (actual_T9_numbers_predictive == "")
                return;
            else if (actual_T9_numbers_predictive.Length == 1)
            {
                actual_T9_numbers_predictive = "";
                handlePredictive("");
                return;

            }

            //Console.WriteLine("Deleting");
            String temp_data = actual_T9_numbers_predictive.Substring(actual_T9_numbers_predictive.Length - 2, 1);
            actual_T9_numbers_predictive = actual_T9_numbers_predictive.Remove(actual_T9_numbers_predictive.Length - 2, 2);
            handlePredictive(temp_data);

        }

        public void updateText_predictive()
        {
            String word_to_add = "";
            if (IspredictionListEmpty)
            {
                //Console.WriteLine("Empty Prediction List");
                StringBuilder sb_temp = new StringBuilder("");
                int counter = 0;
                while (counter++ < actual_T9_numbers_predictive.Length)
                {
                    sb_temp.Append("-");
                }
                if (current_prediction != "")
                    complete_text.Remove(complete_text.Length - current_prediction.Length, current_prediction.Length);
                word_to_add = sb_temp.ToString();
                current_prediction = word_to_add;
                //return;

            }
            else
            {
                if (current_prediction == "")
                    word_to_add = nextWord();
                else
                {
                    //Console.WriteLine("Removing" + current_prediction);
                    complete_text.Remove(complete_text.Length - current_prediction.Length, current_prediction.Length);
                    word_to_add = nextWord();
                    //Console.WriteLine("Adding" + current_prediction);
                }
            }
            //Console.WriteLine("Word to add"+word_to_add);
            complete_text.Append(word_to_add);

        }
        //int MAX_LENGTH = 0;


        //<summary>
        //Handles predictive text insertion.
        //
        //</summary>
        public void handlePredictive(String data)
        {
            if (data.Equals("1"))
                return;
            actual_T9_numbers_predictive += data;
            //trie.search(predicted_word.ToCharArray());
            //Console.WriteLine("Searching for " + actual_T9_numbers_predictive +
            //  " and its length is " + actual_T9_numbers_predictive.Length);

            predictions.Clear();


            String[] temp_arr = null;
            if (actual_T9_numbers_predictive.Length != 0)
                temp_arr = trie.search((actual_T9_numbers_predictive).ToCharArray());
            if (temp_arr == null)
            {
                // Console.WriteLine("No Strings");
                IspredictionListEmpty = true;
                updateText_predictive();
                return;

            }
            IspredictionListEmpty = false;
            foreach (String temp in temp_arr)
            {
                if (temp != null)
                    predictions.Add(temp);




            }





            //foreach (String x in predictions)


            predictions_enumerator = predictions.GetEnumerator();
            //Console.WriteLine(counter +" "+child_pointer);
            updateText_predictive();
        }




        public void handle_space_char()
        {
            complete_text.Append(" ");
            if (Ispredictive)
            {
                current_prediction = "";
                actual_T9_numbers_predictive = "";
            }
            else
            {
                old_data = "";
            }

        }

        //<summary>
        //Handles non-predictive mode. 
        //It takes care of insertion.
        //</summary>

        public void handleNonPredictive(String data, bool same_key)
        {
            int buttonNumber = Int32.Parse(data);
            //Console.WriteLine("button pressed " + buttonNumber);
            //Console.WriteLine("Max cycle value"+(cycle_values[buttonNumber-2]));
            if (same_key && buttonNumber == 1 && cycle == 1)
                return;

            if (same_key && old_data.Equals(data))
            {
                //Console.WriteLine("cycle " + cycle);
                new_char = buttons[buttonNumber][cycle];
                //Console.WriteLine("newChar " + new_char);
                int length = complete_text.Length;
                complete_text.Remove(length - 1, 1);
                complete_text.Append(new_char);
                cycle++;
                if (cycle == cycle_values[buttonNumber - 1])
                    cycle = 0;


            }
            else
            {
                //Console.WriteLine("Different");
                //cycle = 0;
                new_char = buttons[buttonNumber][0];
                complete_text.Append(new_char);
                //Console.WriteLine("newChar " + new_char);
                cycle = 1;
            }

            old_data = data;


        }


    }
}
