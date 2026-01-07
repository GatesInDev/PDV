using System.Collections;
using System.Windows.Input;
using System.Windows.Media;

namespace PDV.Clients.ViewModels.Interfaces
{
    public interface IConfigViewModel
    {
        string StoreName { get; set; }
        string Cnpj { get; set; }
        string Address { get; set; }
        string PrinterName { get; set; }
        ImageSource LogoDisplay { get; set; }

        ICommand SelectLogoCommand { get; }
        ICommand SaveConfigCommand { get; }

        event Action? RequestClose;
    }
}
