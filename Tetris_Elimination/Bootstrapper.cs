using Tetris_Elimination.ViewModels;
using Caliburn.Micro;
using System.Windows;

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
