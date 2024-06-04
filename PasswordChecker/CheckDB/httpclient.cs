using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace PasswordChecker.CheckDB
{
    public class httpclient
    {
        private static HttpClient client = new HttpClient();
        private static readonly string adress = "https://api.pwnedpasswords.com/range/";

        public static async Task request(string sInput)
        {
            try
            {
                string sFullAdress = adress + sInput.Substring(0, 5);

                Uri uFullAdress = new Uri(sFullAdress);

                try
                {
                    using (HttpResponseMessage response = await client.GetAsync(uFullAdress))
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();

                        PasswordChecker.response = responseBody;
                    }
                }
                catch (HttpRequestException e)
                {
                    Debug.WriteLine($"{e.Message}");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception has been Caought!");
                Debug.WriteLine(e.ToString());
            }
        }
    }
}
