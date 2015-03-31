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


/*This class is an implementation of Trie data structure for T9 dictionary.
 * Here the nodes can only have values 2-9.Even though the words may have different 
 * characters,they can have same T9 format(eg:home,good both have 4663).
 * A simple example is given below as a tree structure.
 * Each Node has an array of size 8.It also has a container for holding strings that finish 
 * at that node.It carries 2 counters.One for number of children.Second for number of  
 * finished strings for that node.
 * In that example node 3 has number of children=2.
 * container=[good,home]
 * node 8 has number of children=1.
 * container=[host]
 * Compression can achieve better space complexity.Still to be implemented.
 */

//		 4  
//		/ \
//	   6   4
//	   /\   hi
//	  6  7
//	 /    \
//	3      8
//	good   host
//	home



namespace Project2_Cs
{
    class T9_Trie
    {

        private T9_Trie()
        {

        }

        private TrieNode root = new TrieNode();
        private int[] char_to_num = { 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6,
			6, 6, 7, 7, 7, 7, 8, 8, 8, 9, 9, 9, 9 };


        public void insert(char[] str)
        {

            insert(root, str, 0);
        }

        private void insert(TrieNode node, char[] str, int index)
        {

            if (index == str.Length)
            {
                if (node.size_finished_strings == 0)
                {

                    node.finished_strings = new String[5];

                }
                if (node.finished_strings.Length == node.size_finished_strings)
                    node.finished_strings = resize(node.finished_strings,
                            node.size_finished_strings);
                node.finished_strings[node.size_finished_strings++] = new String(
                        str);

                return;

            }

            char c = str[index];
            int number_equivalent = char_to_num[c - 'a'] - 2;

            if (node.children[number_equivalent] == null)
            {

                node.children[number_equivalent] = new TrieNode();
                node.number_of_children++;

            }

            insert(node.children[number_equivalent], str, index + 1);

        }

        public String[] search(char[] search_str)
        {

            return search(root, search_str, 0);

        }

        private String[] search(TrieNode node, char[] search_str, int index)
        {
            if (node == null)
                return search_five(node, 5);
            if (index == search_str.Length)
            {

                //  if (node.size_finished_strings == 0)
                //    return search_five(node, 5);

                //if (node.finished_strings.Length > 5)
                //  return node.finished_strings;
                return search_five(node, 5);
            }

            char c = search_str[index];
            int children_index = c - '0' - 2;

            return search(node.children[children_index], search_str, index + 1);

        }


        private String[] resize(String[] old, int old_size)
        {
            // numberOfResizes++;
            String[] new_arr = new String[old_size + 5];
            for (int i = 0; i < old_size; i++)
                new_arr[i] = old[i];
            old = null;
            return new_arr;
        }

        public static T9_Trie Build_Trie(String fileName)
        {
            string word;
            T9_Trie t = new T9_Trie();
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            while ((word = file.ReadLine()) != null)
            {
                t.insert(word.ToCharArray());

            }

            file.Close();

            return t;

        }


        //<summary>
        //Search for atleast 5 elements.
        //If not found searches till the end of the tree(all words in that path).
        //</summary>
        public String[] search_five(TrieNode node, int num)
        {

            //System.out.println("Start search_five "+num);
            String[] temp = null;
            String[] temp1 = null;
            if (node == null)
                return null;
            if (node.size_finished_strings != 0)
            {
                //System.out.println("Finished_strings  "+node.size_finished_strings);
                num = num - node.size_finished_strings;
                temp = node.finished_strings;
                //System.out.println("num "+num);
                if (num <= 0)
                {

                    return temp;
                }


            }

            if (node.number_of_children == 0)
                return temp;
            else
            {
                //System.out.println("No of children "+node.number_of_children);
                temp1 = new String[5];
                int i = 0;
                if (temp != null)
                {
                    foreach (String x in temp)
                        if (x != null && i < 5)
                            temp1[i++] = x;
                    num = num - i;
                }

                int counter = -1;
                foreach (TrieNode child in node.children)
                {

                    counter++;
                    if (child != null)
                    {
                        //System.out.println(((char)('2'+counter)));
                        temp = search_five(child, num);
                        if (temp != null)
                        {
                            foreach (String x in temp)
                                if (x != null && i < 5)
                                    temp1[i++] = x;
                            num = num - i;
                        }

                        if (num == 0)
                        {
                            return temp1;
                        }



                    }
                }

                return temp1;
            }




        }



    }


    //<summary>
    //Represents the node of the trie.
    //
    //</summary>
    class TrieNode
    {
        public TrieNode[] children = new TrieNode[8];
        public int size_finished_strings = 0;
        public int number_of_children = 0;
        public String[] finished_strings = null;

    }

}
