using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MortgageWebProject.Services
{
    public class MortgageRateService : IMortgageRateService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _seriesId;

        public MortgageRateService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["FRED:ApiKey"] ?? throw new Exception("FRED API Key is missing in configuration.");
            _seriesId = configuration["FRED:SeriesId"] ?? throw new Exception("FRED SeriesId is missing in configuration.");
        }


        public async Task<double> GetLatestMortgageRateAsync()
        {
            var url = $"https://api.stlouisfed.org/fred/series/observations?series_id={_seriesId}&api_key={_apiKey}&file_type=json&sort_order=desc&limit=1";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("FRED API 호출 실패");
            }

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            var observations = root.GetProperty("observations");
            if (observations.GetArrayLength() == 0)
            {
                throw new Exception("관측치 데이터가 없습니다.");
            }
            var firstObs = observations[0];
            var valueString = firstObs.GetProperty("value").GetString();

            if (double.TryParse(valueString, out double rate))
            {
                return rate;
            }
            else
            {
                throw new Exception("금리 데이터 파싱 실패");
            }
        }
    }
}
