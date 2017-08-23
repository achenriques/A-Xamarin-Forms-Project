using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

//Using of models
using JJOOxamarinForms.Model.model;
using System.Diagnostics;

namespace JJOOxamarinForms.Services.RestConection
{
    public class WebPetition
    {
        const String olimpUrl = "http://172.26.80.77:8080/olimpiadas/";
        const String sedesUrl = "http://172.26.80.77:8080/sedes/";
        const String ciudadesUrl = "http://172.26.80.77:8080/paises/";

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
            var uri = new Uri(sedesUrl);

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
            var uri = new Uri(ciudadesUrl);

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
            String url = sedesUrl + ano + "/";
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
            String url = sedesUrl + ano + "/" + id_ciudad + "/" + id_tipo + "/";
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
            String url = sedesUrl + anoViejo + "/" + anoNuevo + "/" + id_tipo + "/";
            Debug.WriteLine(url);
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
