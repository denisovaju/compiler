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

        protected const char ADD = '+';
        protected const char SUBTRACTION = '-';

        protected List<string> token = new List<string>();

        private string characters;
        private string lexema = "0123456789+-";

        public List<char> error = new List<char> ();       

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
               // Console.WriteLine("O-la-la");
            }
            else
            {
                characters = "0";
                for (int i = 0; i < error.Count; i++ )
                {
                    Console.WriteLine("Error symbols: " + error[i]);
                }
                Console.WriteLine("Input Mistake!\n");
            }
            }
        }    
        //возвращает токены, только для однозначных чисел, порядок следования числа и оператора не учитывается
        public List<string> get_token(string s)
        {
            string[] temp = new string[2];
            int counter = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ADD || s[i] == SUBTRACTION)
                { 
                    counter += 1;
                    if (counter == 1)
                    {
                        if (s[i] == ADD)
                        {
                            temp[0] = PLUS;
                            temp[1] = s[i] + "#";
                            token.AddRange(temp);                           
                        }
                        else if (s[i] == SUBTRACTION)
                        {
                            temp[0] = MINUS;
                            temp[1] = s[i] + "#";
                            token.AddRange(temp);  
                        }
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Mistake ++\n");
                        break;
                    }
                }
            }
            if (counter == 1)
            {
                for (int j = 0; j < s.Length; j++)
                {
                    //проверка для однозначного числа
                    //надо вместо символа использовать строки?
                    while (true)
                    {
                        if(s[j] == ADD)
                        {
                            break;
                        }
                        else if (s[j] == SUBTRACTION)
                        {
                            break;
                        }
                        else
                        {
                            //counter порядок числа, 1 - левое, 2 - правое
                            if (counter == 1)
                            {
                                temp[0] = INTEGER;
                                temp[1] = s[j] + "#";
                                token.AddRange(temp);
                                counter += 1;
                                break;
                            }
                            else if (counter == 2)
                            {
                                temp[0] = INTEGER;
                                temp[1] = s[j] + "#";
                                token.AddRange(temp);
                                counter += 1;
                                break;
                            }
                        }
                    }
                }
                return token;
            }
            else
            {
                token.Clear();
                token.Add("No correct input");
                return token;
            }
        }
    }

    class Parser : LexicalAnalysis
    {
        //опять для однозначных чисел
        public void compiler(List<string> l)
        {
            int a = 0;
            int b = 0;
            char operation = '+';
            int counter = 0;

            for (int i = 0; i < l.Count; i++)
            {
                if (i % 2 == 0)
                {
                    if (l[i] == "PLUS" || l[i] == "MINUS")
                    {
                        if (l[i] == "PLUS")
                        {
                            operation = ADD;
                        }
                        else
                        {
                            operation = SUBTRACTION;
                        }
                        counter += 1;
                    }
                    else if (l[i] == "INTEGER" && counter == 1)
                    {
                        i++;
                        a = Convert.ToInt32(l[i].Substring(0, 1));
                        counter += 1;
                    }
                    else if (l[i] == "INTEGER" && counter == 2)
                    {
                        i++;
                        b = Convert.ToInt32(l[i].Substring(0, 1)); 
                    }
                }
            }
            if (operation == ADD)
            {
                int result = a + b;
                Console.WriteLine(result);
            }
            else if (operation == SUBTRACTION)
            {
                int result = a - b;
                Console.WriteLine(result);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Parser p = new Parser();
                Console.WriteLine("Enter your expression: " + p.Lexema);
                p.Characters = Console.ReadLine();
                List<string> token = new List<string>();
                token = p.get_token(p.Characters);

                p.compiler(token);

                //Console.ReadKey();
            }
        }
    }
}
