using System.Security.AccessControl;
using Villa_MVC.Models;

namespace MagicVilla_Web.Models
{
    public class APIRequest
    {
        public APIType ApiType { get; set; } = APIType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        //public string Token { get; set; }
    }
}
