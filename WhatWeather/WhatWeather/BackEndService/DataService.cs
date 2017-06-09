using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace WhatWeather.BackEndService
{
    class DataService
    {
        public static async Task<dynamic> GetDataFromService(string queryString)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(queryString);

                dynamic data = null;
                if (response != null)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    data = JsonConvert.DeserializeObject(json);
                }

                return data;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
