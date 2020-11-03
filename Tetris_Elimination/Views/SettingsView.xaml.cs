using static Tetris_Elimination.Models.ConstantsModel;
using System.Windows.Controls.Primitives;
using Tetris_Elimination.Models;
using System.Windows.Controls;

namespace Tetris_Elimination.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// Code behind breaking MVVM as CM does not surrport Thumb.DragComplete events
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
            audioManager.PlaySound(Sound.TIMER_END);
        }
    }
}
