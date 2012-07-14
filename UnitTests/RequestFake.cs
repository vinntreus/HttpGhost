using System;
using System.Linq;
using System.Collections.Generic;
using HttpGhost.Authentication;
using HttpGhost.Transport;

namespace UnitTests
{
    public class RequestFake : IRequest
    {
        private readonly IResponse response;

        public RequestFake(){}

        public RequestFake(IResponse response)
        {
            this.response = response;
        }

        public IResponse GetResponse()
        {
            return response ?? new FakeResponse();
        }

        public void SetAuthentication(AuthenticationInfo authentication)
        {
            HaveSetAuthentication++;
        }


        readonly IDictionary<string, int> setMethod = new Dictionary<string, int>();
        readonly IDictionary<string, int> setContentType = new Dictionary<string, int>();
        readonly IDictionary<object, int> setFormData = new Dictionary<object, int>();

        public void SetMethod(string method)
        {
            SetDictionary(setMethod, method);
        }

        public void SetContentType(string contentType)
        {
            SetDictionary(setContentType, contentType);
        }

        private static void SetDictionary<T>(IDictionary<T, int> dictionary, T key)
        {
            if (key == null)
                return;
            if (dictionary.ContainsKey(key))
                dictionary[key] += 1;
            else
            {
                dictionary.Add(key, 1);
            }
        }

        public void WriteFormDataToRequestStream(string formData)
        {
            
        }

        public string Url { get; set; }

        public long HaveSetAuthentication { get; private set; }

        public void WriteFormDataToRequestStream(object formData)
        {
            SetDictionary(setFormData, formData);
        }

        public AuthenticationInfo GetAuthentication()
        {
            throw new System.NotImplementedException();
        }

        public string GetContentType()
        {
            throw new System.NotImplementedException();
        }

        public Uri Uri { get; set; }

        public int HaveSetMethodWith(string method)
        {
            return setMethod[method];
        }

        public int HaveSetContentTypeWith(string contentType)
        {
            return setContentType[contentType];
        }

        public int HaveSetFormDataWith(object formData)
        {
            return setFormData[formData];
        }

        public void AddHeader(System.Net.HttpRequestHeader requestHeader, string value)
        {
            Headers.Add(requestHeader, value);
        }


        public System.Net.WebHeaderCollection Headers
        {
            get;
            set;
        }


        public string Body
        {
            get;
            set;
        }
    }
}