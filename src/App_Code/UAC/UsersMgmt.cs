using System;
using Bestoon_WinClient.NET;
using System.Web.Script.Serialization;

namespace Bestoon_WinClient.UAC
{
    class UsersMgmt
    {
        public User Login()
        {
            try
            {
                Console.Write("Enter your UserName: ");
                string _UserName = Console.ReadLine();

                Console.Write("Enter your Password: ");
                string _Password = string.Empty;

                ConsoleKeyInfo key;

                do
                {
                    key = Console.ReadKey(true);

                    // Backspace and Enter Should Not Work
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        _Password += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write("\b");
                    }
                }
                while (key.Key != ConsoleKey.Enter);

                string HttpResponce =
                HttpRequest.SubmitPostRequest
                    ("http://bestoon.ir/accounts/login/", string.Format("username={0}&password={1}", _UserName.Trim(), _Password.Trim()));

                //Convert Json Object to User Object
                var CurrentUser =
                    new JavaScriptSerializer()
                    .Deserialize<User>(HttpResponce);

                if (CurrentUser.token != null)
                {
                    CurrentUser.UserName = _UserName;
                    return CurrentUser;
                }

                return null;
            }
            catch
            {
                Console.WriteLine("\n"+"Error: Invalid UserName or Password");
                return null;
            }

        }
    }
}
