using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class LexicalAnalysis
    {
        public const string INTEGER = "INTEGER";
        public const string PLUS = "PLUS";
        public const string MINUS = "MINUS";

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
    }

    class ArithmeticAnalysis : LexicalAnalysis
    {
        private List<string> token = new List<string>();

        //возвращает токены, только для однозначных чисел, порядок следования числа и оператора не учитывается
        public List<string> get_token(string s)
        {
            string[] temp = new string[2];
            int counter = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '+' || s[i] == '-')
                { 
                    counter += 1;
                    if (counter == 1)
                    {
                        if (s[i] == '+')
                        {
                            temp[0] = PLUS;
                            temp[1] = s[i] + "#";
                            token.AddRange(temp);                           
                        }
                        else if (s[i] == '-')
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
                    while (true)
                    {
                        if(s[j] == '+')
                        {
                            break;
                        }
                        else if (s[j] == '-')
                        {
                            break;
                        }
                        else
                        {
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

    class Parser
    {        
        //опять для однозначных чисел
        public void compiler(List<string> l)
        {
            int a = 0;
            int b = 0;
            string operation = "0";
            int counter = 0;

            for (int i = 0; i < l.Count; i++)
            {
                if (i % 2 != 0)
                {
                    for (int j = 0; j < l.Count; j++)
                    {
                        if (l[j].EndsWith("#"))
                        {
                            break;
                        }
                        else
                        {
                            if (counter == 0)
                            {
                                operation = l[i].Substring(0,1);
                                counter += 1;
                            }
                            else if (counter == 1)
                            {
                                a = Convert.ToInt32(l[i].Substring(0,1));
                                counter += 1;
                            }
                            else if (counter == 2)
                            {
                                b = Convert.ToInt32(l[i].Substring(0,1));                           
                            }
                        }
                    }
                }
            }
            if (operation == "+")
            {
                int result = a + b;
                Console.WriteLine(result);
            }
            else if (operation == "-")
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
                LexicalAnalysis lexema = new LexicalAnalysis();
                Console.WriteLine("Enter your expression: " + lexema.Lexema);
                lexema.Characters = Console.ReadLine();

                ArithmeticAnalysis number = new ArithmeticAnalysis();
                List<string> token = new List<string>();
                token = number.get_token(lexema.Characters);

                Parser p = new Parser();
                p.compiler(token);

                //Console.ReadKey();
            }
        }
    }
}
