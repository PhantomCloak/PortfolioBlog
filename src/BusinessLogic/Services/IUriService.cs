using System;

namespace BusinessLogic.Services
{
    public interface IUriService
    {
        Uri GetContentUri(string key);
    }
}