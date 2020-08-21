using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using MarketAnalyst.Core.Dtos.Common;
using Newtonsoft.Json;

namespace MarketAnalyst.Core.Services.ExternalApi
{
    public class HttpCallService : IHttpCallService
    {

        public async Task<(T Result, List<ErrorDto> Errors)> GetAlborzServiceAsync<T>(string uri, Dictionary<string, string> headers)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Timeout = 20000;

            if (headers != null)
                foreach (KeyValuePair<string, string> entry in headers)
                {
                    request.Headers.Add(entry.Key, entry.Value);
                }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = await reader.ReadToEndAsync();
                    if (typeof(T) == typeof(string))
                    {
                        return (Result: (T)Convert.ChangeType(json, typeof(T)), Errors: null);
                    }
                    else
                    {                     
                        return (Result: JsonConvert.DeserializeObject<T>(json), Errors: null);
                    }
                }
            }
            catch (WebException e)
            {
                return (Result: default(T), Errors: await handleError(uri, e));
            }
        }
        public async Task<(T Result, List<ErrorDto> Errors)> PostAlborzServiceAsync<T>(string uri, object model, Dictionary<string, string> headers = null, int timeout = 40000)
        {


            var DESC = JsonConvert.SerializeObject(model);
            byte[] dataBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Timeout = timeout;

            request.ContentLength = dataBytes.Length;
            request.ContentType = "application/json";
            request.Method = "POST";

            if (headers != null)
                foreach (KeyValuePair<string, string> entry in headers)
                {
                    request.Headers.Add(entry.Key, entry.Value);
                }

            using (Stream requestBody = request.GetRequestStream())
            {
                await requestBody.WriteAsync(dataBytes, 0, dataBytes.Length);
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = await reader.ReadToEndAsync();

                    return (Result: JsonConvert.DeserializeObject<T>(json), Errors: null);
                }

            }
            catch (WebException e)
            {
                return (Result: default(T), Errors: await handleError(uri, e));

            }

        }


        private async Task<List<ErrorDto>> handleError(string uri, WebException e)
        {
            try
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {

                        var json = await reader.ReadToEndAsync();
                        //Log.Warning(Newtonsoft.Json.JsonConvert.SerializeObject(new
                        //{
                        //    Name = "ALL EXTERNAL ERROR1",
                        //    Step = 100,
                        //    Object = JsonConvert.SerializeObject(json)
                        //}));
                        var ErrorObject = JsonConvert.DeserializeObject<List<ErrorDto>>(json);
                        return ErrorObject;

                    }
                }
            }
            catch (Exception ex)
            {
                //Log.Warning(Newtonsoft.Json.JsonConvert.SerializeObject(new
                //{
                //    Name = "ALL EXTERNAL ERROR2",
                //    Step = 100,
                //    Object = JsonConvert.SerializeObject(ex)
                //}));
                throw new Exception(uri + "مشکل در برقراری ارتباط با " + "\r\n");
            }
        }

        public Task<T> GetAsync<T>(string uri, Dictionary<string, string> headers)
        {
            throw new NotImplementedException();
        }

        public async Task<T> PostAsync<T>(string uri, object model, Dictionary<string, string> headers)
        {

            byte[] dataBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Timeout = 20000;

            request.ContentLength = dataBytes.Length;
            request.ContentType = "application/json";
            request.Method = "POST";

            foreach (KeyValuePair<string, string> entry in headers)
            {
                request.Headers.Add(entry.Key, entry.Value);

            }

            using (Stream requestBody = request.GetRequestStream())
            {
                await requestBody.WriteAsync(dataBytes, 0, dataBytes.Length);
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (Exception e)
            {
                throw; 
            }

        }
    }

}
