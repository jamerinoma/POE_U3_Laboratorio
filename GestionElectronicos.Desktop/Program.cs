using GestionElectronicos.Desktop.Formularios;

namespace GestionElectronicos.Desktop;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmLogin());
    }
}
