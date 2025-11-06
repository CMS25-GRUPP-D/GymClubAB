using Infrastructure.Interfaces;
using Presentation.Views;
using System.Windows;

namespace Presentation.Services;

public class DialogService : IDialogService
{
    public bool ShowTermsOfServiceDialog()
    {
        var tosWindow = new TermsOfServiceWindow();
        return tosWindow.ShowDialog() == true;
    }
}
