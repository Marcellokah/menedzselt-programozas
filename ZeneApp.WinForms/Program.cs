using ZeneApp.Common;

namespace ZeneApp.WinForms;

static class Program
{
    [STAThread]
    static void Main()
    {
        // Adatbázis létrehozása, ha nem létezik
        using (var db = new ZeneContext())
        {
            db.Database.EnsureCreated();
        }

        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}