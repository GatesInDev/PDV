using System;
using System.Windows.Input;

namespace PDV.Clients.ViewModels.Interfaces
{
    public interface ILoginViewModel
    {
        string? Username { get; set; }
        string? ErrorMessage { get; set; }
        bool IsBusy { get; set; }

        ICommand LoginCommand { get; }

        event Action<Tuple<bool, string>> LoginData;
    }
}