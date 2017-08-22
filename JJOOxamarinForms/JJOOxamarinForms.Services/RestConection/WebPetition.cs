using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

//Using of models
using JJOOxamarinForms.Model.model;

namespace JJOOxamarinForms.Services.RestConection
{
    public class WebPetition
    {
        HttpClient client;

        public WebPetition()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.Timeout = TimeSpan.FromSeconds(3);
        }

        public async Task<List<Olimpiada>> GetOlimpiadas()
        {
            List<Olimpiada> toret;
            String url = "http://172.26.80.77:8080/olimpiadas/";
            var uri = new Uri(url);

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                toret = JsonConvert.DeserializeObject<List<Olimpiada>>(content);
            }
            else
                toret = null;

            return toret;
        }

        public async Task<List<SedeJJOO>> GetSedes()
        {
            List<SedeJJOO> toret;
            String url = "http://172.26.80.77:8080/sedes/";
            var uri = new Uri(url);

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                toret = JsonConvert.DeserializeObject<List<SedeJJOO>>(content);
            }
            else
                toret = null;

            return toret;
        }

        public async Task<List<List<String>>> GetCiudades()
        {
            List<List<String>> toret;
            String url = "http://172.26.80.77:8080/paises/";
            var uri = new Uri(url);

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                toret = JsonConvert.DeserializeObject<List<List<String>>>(content);
            }
            else
                toret = null;

            return toret;
        }

        public async Task<Boolean> DeleteSede(int ano)
        {
            String url = "http://172.26.80.77:8080/sedes/"+ ano + "/";
            var uri = new Uri(url);

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = uri
            };
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
                return false;
        }

        public async Task<Boolean> NewSede(int ano, int id_ciudad, int id_tipo)
        {
            String url = "http://172.26.80.77:8080/sedes/" + ano + "/" + id_ciudad + "/" + id_tipo + "/";
            var uri = new Uri(url);

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = uri
            };
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
                return false;
        }

        public async Task<Boolean> ModifySede(int anoViejo, int anoNuevo, int id_tipo)
        {
            String url = "http://172.26.80.77:8080/sedes/" + anoViejo + "/" + anoNuevo + "/" + id_tipo + "/";
            var uri = new Uri(url);

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = uri
            };
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
                return false;
        }
    }
}
