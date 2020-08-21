using MarketAnalyst.Core.Dtos.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketAnalyst.Core.Services.ExternalApi
{
    public interface IHttpCallService
    {
        Task<(T Result, List<ErrorDto> Errors)> GetAlborzServiceAsync<T>(string uri, Dictionary<string, string> headers);
        Task<(T Result, List<ErrorDto> Errors)> PostAlborzServiceAsync<T>(string uri, object model, Dictionary<string, string> headers=null, int timeout = 40000);
        Task<T> GetAsync<T>(string uri, Dictionary<string, string> headers);
        Task<T> PostAsync<T>(string uri, object model, Dictionary<string, string> headers = null);
    }
}