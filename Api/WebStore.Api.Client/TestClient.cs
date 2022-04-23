using WebStore.Api.Client.Base;
using WebStore.Interface.Interfaces;

namespace WebStore.Api.Client
{
    public class TestClient : BaseClient, ITestClientService
    {
        public TestClient(HttpClient client):base(client, "api/Test")
        {

        }
        public string GetString(string value)
        {
            var response = HttpClient.GetAsync($"{_Address}/getString/{value}").Result;
            if (response.IsSuccessStatusCode)
                return value;
            return "";

        }

        public string PostString(string value)
        {
            var response = HttpClient.GetAsync(_Address).Result;
            if (response.IsSuccessStatusCode)
                return value;
            return "";
        }
    }
}