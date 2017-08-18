﻿using System;
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
                toret = new List<Olimpiada>();

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
                toret = new List<SedeJJOO>();

            return toret;
        }
    }
}