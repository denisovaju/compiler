using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class LexicalAnalysis
    {
        protected const string INTEGER = "INTEGER";
        protected const string PLUS = "PLUS";
        protected const string MINUS = "MINUS";

        protected const string ADD = "+";
        protected const string SUBTRACTION = "-";

        protected List<string> token = new List<string>();

        private string characters;
        private string lexema = "0123456789+-add";

        public List<char> error = new List<char>();

        public string Lexema
        {
            get { return lexema; }
        }

        public string Characters
        {
            get { return characters; }
            set
            {
                foreach (char j in value)
                {
                    for (int i = 0; i < Lexema.Length; i++)
                    {
                        if (j == Lexema[i])
                        {
                            break;
                        }
                        else if (j != Lexema[i] && i == Lexema.Length - 1)
                        {
                            error.Add(j);
                            break;
                        }
                    }
                }
                if (error.Count == 0)
                {
                    characters = value;
                    Console.WriteLine("O-la-la");
                }
                else
                {
                    characters = "0";
                    for (int i = 0; i < error.Count; i++)
                    {
                        Console.WriteLine("Error symbols: " + error[i]);
                    }
                    Console.WriteLine("Input Mistake!\n");
                }
            }
        }

        public Dictionary<string, string> tokena = new Dictionary<string, string>();

        public string value1 = "0";
        public string value2 = "0";
        
        public Dictionary<string, string> get_next_token(string s)
        {
            int counter = 0;
            int index = 0;
            int c = 0;

            for (int i = 0; i < s.Length; i++ )
            {
                if (s.Contains(ADD) && counter != 1)
                {
                    index = s.IndexOf(ADD);
                    string temp = s.Remove(0, index+1);
                    if (temp.Contains(ADD))
                    {
                        break;
                    }
                    else
                    {
                        counter = 1;
                        continue;
                    }
                }
                else if (s.Contains(SUBTRACTION) && c != 1)
                {
                    index = s.IndexOf(SUBTRACTION);

                    string temp = s.Remove(0, index + 1);
                    if (temp.Contains(SUBTRACTION))
                    {
                        break;
                    }
                    else
                    {
                        c = 1;
                        continue;
                    }
                }
                else
                {
                    break;
                }
            }
            if ((counter == 1 ^ c == 1))
            {
                if (((!s.StartsWith(ADD)) && (!s.EndsWith(ADD))) && ((!s.StartsWith(SUBTRACTION)) && (!s.EndsWith(SUBTRACTION))))
                {
                    Console.WriteLine("True");
                    value1 = s.Substring(0, index);

                    //Console.WriteLine(value);
                    tokena.Add(value1, INTEGER);

                    //index++;
                    value2 = s.Remove(0, index + 1); ;
                    tokena.Add(value2, INTEGER);
                    //ICollection<string> keys = tokena.Keys;
                    return tokena;
                }
                else
                {
                    Console.WriteLine("Sorry, but you don't write '+' or '-' at the start or end");
                    return tokena;
                }
            }

            else
            {
                Console.WriteLine("Sorry, but you don't write '+' or '-' more one ");
                return tokena;
            }
        }

        public void show(Dictionary<string, string> dic)
        {
            ICollection<string> keys = dic.Keys;
            foreach (string j in keys)
            {
                Console.WriteLine(j);
                Console.WriteLine(dic[j]);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                LexicalAnalysis p = new LexicalAnalysis();
                Console.WriteLine("Enter your expression: " + p.Lexema);
                p.Characters = Console.ReadLine();

                Dictionary<string, string> tokena = new Dictionary<string, string>();

                tokena = p.get_next_token(p.Characters);
                p.show(tokena);

                //Console.ReadKey();
            }
        }
    }
}
