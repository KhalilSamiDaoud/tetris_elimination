using Caliburn.Micro;
using System.Windows;
using Tetris_Elimination.ViewModels;

namespace Tetris_Elimination
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }
    }
}
