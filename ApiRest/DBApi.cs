using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace PyWinForm_ConsumoAPI.ApiRest
{
    public class DBApi
    {
        public dynamic Post(string url, string json, string autorizacion=null)
        {
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest("",Method.Post);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                if (autorizacion != null)
                {
                    request.AddHeader("Authorization", autorizacion);
                }
                var response = client.Post(request);

                //IRestResponse response = client.Execute(request);
                dynamic datos = JsonConvert.DeserializeObject(response.Content);
                return datos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public dynamic Get(string url)
        {
            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(url);
            myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
            myWebRequest.Credentials = CredentialCache.DefaultCredentials;
            myWebRequest.Proxy = null;
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            Stream myStream = myHttpWebResponse.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myStream);
            //Leemos los datos
            string Datos = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());
            dynamic data = JsonConvert.DeserializeObject(Datos);
            return data;

        }



        //Otros ejemplos

        public dynamic GetItem(int id)
        {
            var client = new RestClient("http://localhost:8080");
            var request = new RestRequest($"/item/{id}", Method.Get);
            var response = client.Execute(request);
            return response.Content;
        }
        public dynamic GetItems()
        {
            var client = new RestClient("http://localhost:8080");
            var request = new RestRequest("items", Method.Get);
            var response = client.Execute(request);
            return response.Content;
        }
        public dynamic GetItems(string filter)
        {
            var client = new RestClient("http://localhost:8080");
            var request = new RestRequest("items", Method.Get);
            request.AddParameter("filter", filter);
            var response = client.Execute(request);
            return response.Content;
        }
        public dynamic PostItem(string data)
        {
            var client = new RestClient("http://localhost:8080");
            var request = new RestRequest("items", Method.Post);
            request.AddParameter("data", data);
            var response = client.Execute(request);
            return response.Content;
        }
        public dynamic PutItem(int id, string data)
        {
            var client = new RestClient("http://localhost:8080");
            var request = new RestRequest("items", Method.Put);
            request.AddParameter("id", id);
            request.AddParameter("data", data);
            var response = client.Execute(request);
            return response.Content ;
        }
        public dynamic DeleteItem(int id)
        {
            var client = new RestClient("http://localhost:8080");
            var request = new RestRequest($"items/{id}", Method.Delete);
            var response = client.Execute(request);
            return response.Content ;
        }

    }
}
