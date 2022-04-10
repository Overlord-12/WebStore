using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Api.Client.Base
{
    public abstract class BaseClient
    {
        protected HttpClient HttpClient { get; }
        protected string _Adress { get; }
        public BaseClient(HttpClient client, string Adress)
        {
            HttpClient = client;
            _Adress = Adress;
        }
    }
}
