using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using backend_api.Models;
using backend_api.Interfaces;
using backend_api.Dtos.Stock;
using backend_api.Mappers;
using Newtonsoft.Json;

namespace backend_api.Service
{
    public class FMPService : IFMPService
    {
        private readonly IConfiguration _config;
        // HttpClient is a class predefined by .Net use to send HttpRequest and get HttpResponse;
        // 需要注入依赖 在 appsetting.json
        private readonly HttpClient _httpClient;

        // 自动配置 IConfiguration和HttpClient作为工具
        // FMPService 只需要 决定how to use these tools
        public FMPService(IConfiguration config, HttpClient httpClient){
            _config = config;
            _httpClient = httpClient;
        }
        public async Task<Stock> FindStockBySymbolAsync(string symbol){
            try{
                 var response = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_config["FMPKey"]}");
                if (response.IsSuccessStatusCode)
                {
                    // 读取里面的内容
                    var content = await response.Content.ReadAsStringAsync();
                    // 反序列化 返回 FMPStock的Lists
                    // 用poco将 Json数据 转化为 C# 代码
                    var stocks = JsonConvert.DeserializeObject<FMPStock[]>(content); 
                    var stock = stocks[0];
                    if (stock != null){
                        return stock.ToStockFromFMPStock();
                    }
                    return null;
                }
                return null;
            }
            catch (Exception e){
                Console.WriteLine(e);
                return null;
            }
        }
    }
}