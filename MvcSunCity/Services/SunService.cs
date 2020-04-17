using System;
using System.Net.Http;
using System.Text;

namespace MvcSunCity.Services
{
    public static class SunService
    {
        private static readonly HttpClient client = new HttpClient();
        
        public static async System.Threading.Tasks.Task<string> GetDataFromServer(string lattitude, string longitude, string date)
        {
            StringBuilder sb = new StringBuilder("https://api.sunrise-sunset.org/json?")
            .Append("lat=" + lattitude)
            .Append("&lng=" + longitude)
            .Append("&date=" + date);

            UriBuilder builder = new UriBuilder(sb.ToString());
            client.DefaultRequestHeaders.Accept.Clear();
            var result = client.GetStringAsync(builder.Uri);
            var msg = await result;
            return msg ;
        }
    }
}