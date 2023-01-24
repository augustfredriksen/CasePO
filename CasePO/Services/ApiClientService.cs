
namespace CasePO.Services   
{
    public class ApiClientService
    {
        static HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl = "https://data.brreg.no/enhetsregisteret/api/enheter/";

        public async Task<string> MakeBatchApiCall(List<string> orgNos)
        {
            var queryString = $"?size={orgNos.Count}&organisasjonsnummer=" + string.Join(",", orgNos);
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}{queryString}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return json;
                }
                else
                {
                    Console.WriteLine($"Api call failed with status code {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e} Error when making API call");
                return null;
            }
        }

        public async Task<string> MakeIndividualApiCall(string orgNo)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}{orgNo}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine($"API call was successful and returned json: {json}");
                    return json;
                }
                else
                {
                    Console.WriteLine($"Api call failed with orgNo {orgNo} and status code {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e} Error when making API call");
                return null;
            }
        }
    }
}
