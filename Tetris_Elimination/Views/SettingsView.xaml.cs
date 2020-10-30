using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Tetris_Elimination.Models;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {

        private AudioManagerModel audioManager;

        public SettingsView()
        {
            audioManager = AudioManagerModel.Instance;
            InitializeComponent();
        }

        private void EffectsBlip(object sender, DragCompletedEventArgs e)
        {
            audioManager.playSound(Sound.TIMER_END);
        }
    }
}
