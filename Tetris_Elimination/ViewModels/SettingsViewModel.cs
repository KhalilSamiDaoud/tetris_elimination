using Caliburn.Micro;
using System;
using Tetris_Elimination.Models;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.ViewModels
{
    class SettingsViewModel : Screen
    {
        private MainViewModel main_window;
        private AudioManagerModel audioManager;
        private String _userName;
        private Double _effectsVolume;
        private Double _musicVolume;

        public SettingsViewModel(MainViewModel _main_window)
        {
            main_window    = _main_window;
            audioManager   = AudioManagerModel.Instance;
            EffectsVolume  = Properties.Settings.Default.EffectsVol;
            MusicVolume    = Properties.Settings.Default.MusicVol;
            UserName       = Properties.Settings.Default.Name;
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);

            }
        }

        public double EffectsVolume
        {
            get
            {
                return _effectsVolume;
            }
            set
            {
                _effectsVolume = value;
                audioManager.setVolume(_effectsVolume, _musicVolume);
                NotifyOfPropertyChange(() => EffectsVolume);
            }
        }

        public double MusicVolume
        {
            get
            {
                return _musicVolume;
            }
            set
            {
                _musicVolume = value;
                audioManager.setVolume(_effectsVolume, _musicVolume);
                NotifyOfPropertyChange(() => MusicVolume);
            }
        }

        private void Save()
        {
            Properties.Settings.Default.Name         = UserName;
            Properties.Settings.Default.MusicVol     = MusicVolume;
            Properties.Settings.Default.EffectsVol   = EffectsVolume;
            Properties.Settings.Default.Save();
        }

        public void SaveAndExit()
        {
            Save();
            main_window.SetNewView(Screens.MENU);
        }
    }
}
