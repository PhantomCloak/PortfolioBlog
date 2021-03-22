using System;
using Shared.Contracts.V1.Routes;

namespace BusinessLogic.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        
        public Uri GetContentUri(string key)
        {
            return new Uri(_baseUri + ApiRoutes.Entry.Get.Replace("{contentKey}", key));
        }

    }
}