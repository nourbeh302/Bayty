using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Server.Services.SMS
{
    public class SMSService : ISMSService
    {
        private readonly SMSSettings _smsSettings;
        private readonly HttpClient _http;

        public SMSService(IOptions<SMSSettings> options, HttpClient httpClient)
        {

            _smsSettings = options.Value;
            _http = httpClient;
        }

        public async Task<SMSResult> SendSMSAsync(string to, string token)
        {
            var requestUri = GenerateUri(to, token);

            try
            {
                var result = await _http.GetStringAsync(requestUri);
                return JsonConvert.DeserializeObject<SMSResult>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private string GenerateUri(string to, string token) => _smsSettings.BaseUrl + "api_key=" + _smsSettings.ApiKey + "&" + "api_secret=" + _smsSettings.ApiSecret + "&to=2" + to + $"&text={token}";
    }
}
