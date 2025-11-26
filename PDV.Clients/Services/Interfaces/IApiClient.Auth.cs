using PDV.Clients.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDV.Clients.Services.Interfaces
{
    public partial interface IApiClient
    {
        Task<LoginResponseModel?> AutenticarAsync(string username, string password);
    }
}
