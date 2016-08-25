using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//анализатор
namespace ConsoleApplication1
{
    //в классе проверяется соответствует ли введенное выражение заданному алфавиту, также отлавливаются ошибочные - символы
    //нужно сделать, чтобы алфавит задавался в классе - наследнике

    class LexicalAnalysis
    {
        private string characters;
        private string lexema = "0123456789+";

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

    //класс - наследник, в нем выполняются специфические функции узкого анализа информации
    //написать конструктор
    class ArithmeticAnalysis : LexicalAnalysis
    {
        private static List<string> null_l = new List<string> { "No correct input\n" };
        private List<string> token = new List<string>();

        //демонстрация
        public void demo(string s)
        {
               List<string> m = get_token(s);
               show(m);
        }
        //возвращает токены, только для однозначных чисел, порядок следования числа и оператора не учитывается
        public List<string> get_token(string s)
        {
            string temp;
            int counter = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '+')
                { 
                    counter += 1;
                    if (counter == 1)
                    {
                        temp = "operator" + " : " + s[i];
                        token.Add(temp);
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
                    while (s[j] != '+')
                    {
                        if (counter == 1)
                        {
                            temp = "left" + " : " + s[j];
                            token.Add(temp);
                            counter += 1;
                            break;
                        }
                        else if (counter == 2)
                        {
                            temp = "right" + " : " + s[j];
                            token.Add(temp);
                            counter += 1;
                            break;
                        }
                    }
                }
            }
            else
            {
                return null_l;
            }
            return token;
        }

        //просто выводит список с токенами
        public void show(List<string> l)
        {
            for (int i = 0; i < l.Count; i++)
            {
                Console.WriteLine(l[i]);
            }
        }
    }

    class Parser
    {
        public void compiler(List<string> l)
        {
            int summa = 0;
            string oper = l[0];
            string left = l[1];
            string right = l[2];
            string o = "0";
            string le = "0";
            string r = "0";
            for (int i = 0; i < oper.Length; i++)
            {
                if (i == 7)
                {
                    le = left.Substring(i, 1);
                }
                else if (i == 8)
                {
                    r = right.Substring(i, 1);
                }
                else if(i == 11)
                {
                    o = oper.Substring(i, 1);
                }
            }

            if (o == "+")
            {
                int a = 0;
                int b = 0;
                a = Convert.ToInt32(le);
                b = Convert.ToInt32(r);
                summa = a + b;
                
            }
            
            Console.WriteLine(summa);
            
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
                //lexema.setCharacters(s);


                ArithmeticAnalysis number = new ArithmeticAnalysis();
                number.demo(lexema.Characters);

                List<string> token = new List<string>();
                token = number.get_token(lexema.Characters);

                Parser p = new Parser();
                p.compiler(token);

                //Console.ReadKey();
            }
        }
    }
}
