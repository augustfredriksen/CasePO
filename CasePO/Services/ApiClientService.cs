
namespace CasePO.Services   
{
    /// <summary>
    /// The ApiClientService class is used to make API calls to the Brønnøysundregister.
    /// </summary>
    public class ApiClientService
    {
        // HttpClient object used to make API calls.
        static HttpClient _httpClient = new HttpClient();
        // Base URL of the Brønnøysundregister API.
        private readonly string _baseUrl = "https://data.brreg.no/enhetsregisteret/api/enheter/";


        /// <summary>
        /// Makes an API call to the Brønnøysundregister with a batch of orgNos.
        /// </summary>
        /// <param name="orgNos">A list of orggNos to get data from the API</param>
        /// <returns>A json string containing the data returned from the API or null if the call failed.</returns>
        public async Task<string> MakeBatchApiCall(List<string> orgNos)
        {
            // Creating the query that goes after the base URL.
            var queryString = $"?size={orgNos.Count}&organisasjonsnummer=" + string.Join(",", orgNos);
            try
            {
                // Make the API call
                var response = await _httpClient.GetAsync($"{_baseUrl}{queryString}");
                if (response.IsSuccessStatusCode)
                {
                    // Read the json data from the response.
                    var json = await response.Content.ReadAsStringAsync();
                    return json;
                }
                else
                {
                    // If the response did not return success, iterate through all the orgNos and log the error.
                    // With a query string this will most likely never happen?
                    foreach (var orgNo in orgNos)
                    {
                        var error = $"{DateTime.Now} Api call failed with orgNo {orgNo} and status code {response.StatusCode}";
                        File.AppendAllText("error_log.txt", error + Environment.NewLine);
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                // Log unexpected errors while making the API call.
                var error = ($"{DateTime.Now} Error when making API call {e}");
                File.AppendAllText("error_log.txt", error + Environment.NewLine);
                return null;
            }
        }

        /// <summary>
        /// Makes API call to the Brønnøysundregister with a single orgNo.
        /// </summary>
        /// <param name="orgNo">A single orgNo to get data from the API</param>
        /// <returns>A json string containing the data from the API call or null if the call failed.</returns>
        public async Task<string> MakeIndividualApiCall(string orgNo)
        {
            try
            {
                // Make the API call
                var response = await _httpClient.GetAsync($"{_baseUrl}{orgNo}");
                if (response.IsSuccessStatusCode)
                {
                    // Read the json data from the response.
                    var json = await response.Content.ReadAsStringAsync();
                    return json;
                }
                else
                {
                    // If the response did not return success log the orgNo and errorcode.
                    var error = $"{DateTime.Now} Api call failed with orgNo {orgNo} and status code {response.StatusCode}";
                    File.AppendAllText("error_log.txt", error + Environment.NewLine);
                    return null;
                }
            }
            catch (Exception e)
            {
                // Log any unexpected errors while making the API call.
                var error = ($"{DateTime.Now} Error when making API call {e}");
                File.AppendAllText("error_log.txt", error + Environment.NewLine);
                return null;
            }
        }
    }
}
