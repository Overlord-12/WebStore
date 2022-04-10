using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Interface.Interfaces
{
    public interface ITestClientService
    {
        public string GetString(string value);
        public string PostString(string value);
    }
}
