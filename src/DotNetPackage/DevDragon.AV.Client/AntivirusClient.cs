using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DevDragon.AV.Client
{
    /// <summary>
    /// DevDragon Antivirus API Client
    /// </summary>
    public class AntivirusClient
    {
        /// <summary>
        /// Initialises Antivirus Client
        /// </summary>
        /// <param name="accessKey">Subscription Access Key. Can be obtained from your DevDragon account page</param>
        /// <param name="production">Use production environment by default</param>
        public AntivirusClient(string accessKey, bool production = true)
        {
            _httpClient = new HttpClient();

            if (production)
            {
                throw new NotImplementedException("Production environment hasn't been released yet");
            }
            else
            {
                _httpClient.BaseAddress = new Uri("https://dragonfs-uat.gftestplatform.net");
                _httpClient.DefaultRequestHeaders.Add("X-AUTH", accessKey);
            }
        }

        private HttpClient _httpClient = null;

        public async Task<ScanResult> ScanFile(string filePath)
        {
            HttpResponseMessage result = null;
            var content = new MultipartFormDataContent();

            using (var file = File.OpenRead(filePath))
            {
                var fileContent = new StreamContent(file);
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = Path.GetFileName(filePath)
                };

                content.Add(fileContent);

                result = await _httpClient.PostAsync("/scan", content);
            }

            if (!result.IsSuccessStatusCode)
            {
                if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var response = await result.Content.ReadAsStringAsync();
                    throw new FileScanException(string.Format("Authorisation error: {0}", response));
                }
                else
                {
                    result.EnsureSuccessStatusCode();
                    // the line above will throw relevant exception
                    return null;
                }
            }
            else
            {
                var scanResult = JsonConvert.DeserializeObject<ScanResult>(await result.Content.ReadAsStringAsync());
                return scanResult;
            }
        }
    }
}
