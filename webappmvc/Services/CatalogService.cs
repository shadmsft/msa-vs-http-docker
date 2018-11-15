using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using webappmvc.Client;
using models;

namespace webappmvc.Services
{
    public interface ICatalogService
    {
        Task<List<product>> GetProducts();
        Task<product> GetProduct(string Id);
        Task CreateProduct(Iproduct prod);
    }
    public class CatalogService : ICatalogService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly string _catalogUrl;
        private IHttpClient _httpClient;

        public CatalogService(IHttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _catalogUrl = _configuration.GetSection("CatalogUrl").Value.ToString();

        }
        //public ICatalogService.GetProduct(Iproduct prod){}

        async Task<List<product>> ICatalogService.GetProducts()
        {
            var products = new List<product>();
            try
            {
                var responseString = await _httpClient.GetStringAsync(_catalogUrl);
                List<Dictionary<String, String>> responseElements = new List<Dictionary<string, string>>();
                JsonSerializerSettings settings = new JsonSerializerSettings();
                responseElements = JsonConvert.DeserializeObject<List<Dictionary<String, String>>>(responseString, settings);
                products = JsonConvert.DeserializeObject<List<product>>(responseString);
            }
            catch (System.Exception ex)
            {
                throw new Exception("Catalog URL: " + _catalogUrl + ".  Original Exception Message: " + ex.Message + ". StackTrace: " + ex.StackTrace);
            }
            return products;
        }

        async Task<product> ICatalogService.GetProduct(string Id)
        {
            var prod = new product();
            try
            {
                var responseString = await _httpClient.GetStringAsync(_catalogUrl + "/" + Id);
                prod = JsonConvert.DeserializeObject<product>(responseString);
            }
            catch (System.Exception ex)
            {
                throw new Exception("Catalog URL: " + _catalogUrl + ".  Original Exception Message: " + ex.Message + ". StackTrace: " + ex.StackTrace);
            }

            return prod;
        }
        public async Task CreateProduct(Iproduct prod)
        {
            string reqId = new Guid().ToString();
            var responseString = await _httpClient.PostAsync(_catalogUrl, prod, reqId);
        }
    }
}
