using MVCApplication.Models;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MVCApplication.Services
{
    public class GoogleCaptchaService
    {
        private readonly IConfiguration _config;
        public GoogleCaptchaService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<bool> VerifyToken(string token)
        {
            try
            {
                var secretKey = _config["GoogleReCaptcha:SecretKey"];
                var url = $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={token}";

                using (var client = new HttpClient())
                {
                    var httpResult = await client.GetAsync(url);
                    if (httpResult.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }
                    var responseString = await httpResult.Content.ReadAsStringAsync();
                    var googleResult = JsonSerializer.Deserialize<GoogleCaptchaResponseModel>(responseString);
                   return googleResult.success && googleResult.score >= 0.5;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
