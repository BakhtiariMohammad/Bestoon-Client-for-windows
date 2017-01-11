using System;
using System.IO;
using System.Net;
using System.Text;


namespace Bestoon_WinClient.NET
{
    class HttpRequest
    {
        public static string SubmitPostRequest(string BaseUrl, string StrPostData)
        {
            try
            {
                WebRequest _WebRequest = WebRequest.Create(BaseUrl);

                _WebRequest.ContentType = "application/x-www-form-urlencoded";
                _WebRequest.Method = "POST";

                string postData = StrPostData;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                _WebRequest.ContentLength = byteArray.Length;

                Stream dataStream = _WebRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse response = _WebRequest.GetResponse();
                /* Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription); */

                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
