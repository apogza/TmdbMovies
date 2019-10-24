using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using TmdbMovies.Models;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace TmdbMovies.Models
{
    public sealed class RestClient
    {
        private HttpClient HttpClient { get; }
        
        public RestClient()
        {
            HttpClient = new HttpClient();
        }

        public RestClient(string baseUri)
            :this()
        {
            HttpClient.BaseAddress = new Uri(baseUri);
        }

        public async Task<SearchResults<T>> GetSearchResults<T>(string query) where T: BaseModel 
        {
            return await GetResults<SearchResults<T>>(query);
        }

        public async Task<T> GetResults<T>(string query) where T: class
        {
            try
            {
                using (Stream jsonResult = await HttpClient.GetStreamAsync(query))
                {

                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                    T result = serializer.ReadObject(jsonResult) as T;

                    return result;
                }
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException("An error has occurred accessing the TMDB Api", ex);
            }

        }

        public async Task<T> GetEntity<T>(string query) where T : BaseModel
        {
            try
            {
                using (Stream jsonResult = await HttpClient.GetStreamAsync(query))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer((typeof(T)));
                    T result = serializer.ReadObject(jsonResult) as T;

                    return result;
                }
            }
            catch (HttpRequestException e)
            {
                throw new InvalidOperationException("An error has occurred accessing the TMDB Api", e);
            }
        }
    }
}
