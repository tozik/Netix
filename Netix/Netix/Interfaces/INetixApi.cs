using System;
using Refit;
using System.Threading.Tasks;
using Netix.Models;

namespace Interfaces
{
    public interface INetixApi
    {
        [Get("/MobileWebResource.asmx/GetTextAndPicture?id={uniqId}")]
        Task<NetixModel> GetModel(string uniqId);
    }
}

