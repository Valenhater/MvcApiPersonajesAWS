﻿using MvcApiPersonajesAWS.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcApiPersonajesAWS.Services
{
    public class ServiceApiPersonajes
    {
        private MediaTypeWithQualityHeaderValue header;

        private string UrlApi;

        public ServiceApiPersonajes(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiPersonajes");

            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicies) =>
                {
                    return true;
                };
                using (HttpClient client = new HttpClient(handler))
                {
                    string request = "";
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.header);
                    HttpResponseMessage response = await client.GetAsync(this.UrlApi + request);
                    if (response.IsSuccessStatusCode)
                    {
                        List<Personaje> personajes = await response.Content.ReadAsAsync<List<Personaje>>();
                        return personajes;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

        }

        public async Task CreatePersonajeAsync(string nombre, string imagen)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicies) =>
                {
                    return true;
                };
                using (HttpClient client = new HttpClient(handler))
                {
                    string request = "post";
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.header);
                    Personaje personaje = new Personaje
                    {
                        IdPersonaje = 0,
                        Nombre = nombre,
                        Imagen = imagen
                    };
                    string json = JsonConvert.SerializeObject(personaje);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(this.UrlApi + request, content);
                }
            }
        }
        public async Task UpdatePersonajeAsync(int idPersonaje, string nombre, string imagen)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicies) =>
                {
                    return true;
                };
                using (HttpClient client = new HttpClient(handler))
                {
                    string request = "put/" + idPersonaje;
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.header);
                    Personaje personaje = new Personaje
                    {
                        IdPersonaje = idPersonaje,
                        Nombre = nombre,
                        Imagen = imagen
                    };
                    string json = JsonConvert.SerializeObject(personaje);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync(this.UrlApi + request, content);                  
                }
            }
        }
        
    }
}
