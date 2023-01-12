using System;
using System.Threading.Tasks;
using Netix.Models;
using Interfaces;
using Refit;
using System.Net.Http;

namespace Netix.Services
{
    public class NetixService
    {
        public static NetixService Instance = new NetixService();

        public Task<NetixModel> GetModel(string id)
        {
            var api = RestService.For<INetixApi>("https://mobile.netix.ru");
            return api.GetModel(id);
        }
    }
}

