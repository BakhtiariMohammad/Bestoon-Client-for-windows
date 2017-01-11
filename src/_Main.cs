using System;
using System.Net.Json;
using Bestoon_WinClient.UAC;
using Bestoon_WinClient.NET;

namespace Bestoon_WinClient
{
    class _Main
    {
       private static User _CurrentUser=null;
        static void Main(string[] args)
        {
            try
            {
                Console_Header();

                while (_CurrentUser==null)
                    _CurrentUser = new UsersMgmt().Login();

                Console.Clear();
                Console_Header();

                while (true)
                {
                    Console.Write(">>> ");
                    var Command=Console.ReadLine();

                    switch (Command)
                    {
                        case "--help":
                            Help();
                            break;

                        case "bestoonstat":
                            bestoonGeneralStat();
                            break;

                        case "bestoonexpense":
                            bestoonExpense();
                            break;
                        case "bestoonincome":
                            bestoonIncome();
                            break;

                        case "cls":
                            Console.Clear();
                            break;

                        case "exit":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine(Command+": command not found!");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static void Console_Header()
        {
            Console.WriteLine("Bestoon client for windows [http://bestoon.ir]");
            Console.WriteLine("Type --help for list of Commands.");
        }

        private static void Help()
        {
            Console.WriteLine("List of Commands:=======================================");
            Console.WriteLine("  bestoonstat             Print bestoon general status");
            Console.WriteLine("  bestoonexpense          Submit new Expense");
            Console.WriteLine("  bestoonincome           Submit new Income");
            Console.WriteLine("  cls                     Clear Screen");
            Console.WriteLine("  exit                    Exit");
            Console.WriteLine("========================================================");
        }

        private static void bestoonGeneralStat()
        {
            try
            {
                if (_CurrentUser.token != null)
                {
                    Console.WriteLine("Bestoon Stat:==========================================");

                    var HttpResponse =
                        HttpRequest.SubmitPostRequest("Http://Bestoon.ir/q/generalstat/", "token=" + _CurrentUser.token);

                    JsonTextParser parser = new JsonTextParser();
                    JsonObject obj = parser.Parse(HttpResponse);
                    Console.WriteLine(obj.ToString());

                    Console.WriteLine("========================================================");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static void bestoonExpense()
        {
            try
            {
                Console.WriteLine("bestoonExpense: DateTime<optional>, Text, Amount");

                Console.Write("Enter datetime<optional>:");
                string DateTime = Console.ReadLine();

                Console.Write("Enter text:");
                string Text = Console.ReadLine();

                Console.Write("Enter amount:");
                string amount = Console.ReadLine();

                string POSTdata;
                if (DateTime != string.Empty)
                    POSTdata = string.Format("token={0}&text={1}&amount={2}&date={3}", _CurrentUser.token, Text, amount, DateTime);
                else
                    POSTdata = string.Format("token={0}&text={1}&amount={2}", _CurrentUser.token, Text, amount);

                var HttpResponse =
                    HttpRequest.SubmitPostRequest("Http://Bestoon.ir/submit/expense/", POSTdata);
                Console.WriteLine(HttpResponse.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static void bestoonIncome()
        {
            try
            {
                Console.WriteLine("bestoonIncome: DateTime<optional>, Text, Amount");

                Console.Write("Enter datetime<optional>:");
                string DateTime = Console.ReadLine();

                Console.Write("Enter text:");
                string Text = Console.ReadLine();

                Console.Write("Enter amount:");
                string amount = Console.ReadLine();

                string POSTdata;
                if (DateTime != string.Empty)
                    POSTdata = string.Format("token={0}&text={1}&amount={2}&date={3}", _CurrentUser.token, Text, amount, DateTime);
                else
                    POSTdata = string.Format("token={0}&text={1}&amount={2}", _CurrentUser.token, Text, amount);

                var HttpResponse =
                    HttpRequest.SubmitPostRequest("Http://Bestoon.ir/submit/income/", POSTdata);
                Console.WriteLine(HttpResponse.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}