using Caliburn.Micro;
using System;
using System.Windows.Input;
using Tetris_Elimination.Models;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.ViewModels
{
    class SettingsViewModel : Screen
    {
        private MainViewModel main_window;
        private AudioManagerModel audioManager;
        private Double _effectsVolume;
        private Double _musicVolume;
        private String _userName;
        private String _rotateKey;
        private Key _downKey;
        private Key _leftName;
        private Key _rightName;
        private Key _dropName;
        private Key _holdName;
        private Key _pauseName;

        public SettingsViewModel(MainViewModel _main_window)
        {
            main_window = _main_window;
            audioManager = AudioManagerModel.Instance;

            LoadSettings();
        }

        public void TextBoxClear(KeyBind keyBind)
        {
            switch (keyBind)
            {
                case KeyBind.ROTATE:
                    RotateKey = Key.None.ToString();
                    break;
                default:
                    break;
            }
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

        public string RotateKey
        {
            get
            {
                return _rotateKey;
            }
            set
            {
                if (value == Key.None.ToString())
                {
                    _rotateKey = "";
                }
                else if (value == Key.Up.ToString())
                {
                    _rotateKey = "UP";
                }
                else
                {
                    _rotateKey = value.ToUpper();
                }
                NotifyOfPropertyChange(() => RotateKey);
            }
        }

        //public string DownKey
        //{
        //    get
        //    {
        //        return _musicVolume;
        //    }
        //    set
        //    {
        //        _musicVolume = value;
        //        audioManager.setVolume(_effectsVolume, _musicVolume);
        //        NotifyOfPropertyChange(() => MusicVolume);
        //    }
        //}

        //public string LeftKey
        //{
        //    get
        //    {
        //        return _musicVolume;
        //    }
        //    set
        //    {
        //        _musicVolume = value;
        //        audioManager.setVolume(_effectsVolume, _musicVolume);
        //        NotifyOfPropertyChange(() => MusicVolume);
        //    }
        //}

        //public string RightKey
        //{
        //    get
        //    {
        //        return _musicVolume;
        //    }
        //    set
        //    {
        //        _musicVolume = value;
        //        audioManager.setVolume(_effectsVolume, _musicVolume);
        //        NotifyOfPropertyChange(() => MusicVolume);
        //    }
        //}

        //public string DropKey
        //{
        //    get
        //    {
        //        return _musicVolume;
        //    }
        //    set
        //    {
        //        _musicVolume = value;
        //        audioManager.setVolume(_effectsVolume, _musicVolume);
        //        NotifyOfPropertyChange(() => MusicVolume);
        //    }
        //}

        //public string HoldKey
        //{
        //    get
        //    {
        //        return _musicVolume;
        //    }
        //    set
        //    {
        //        _musicVolume = value;
        //        audioManager.setVolume(_effectsVolume, _musicVolume);
        //        NotifyOfPropertyChange(() => MusicVolume);
        //    }
        //}

        //public string PauseKey
        //{
        //    get
        //    {
        //        return _musicVolume;
        //    }
        //    set
        //    {
        //        _musicVolume = value;
        //        audioManager.setVolume(_effectsVolume, _musicVolume);
        //        NotifyOfPropertyChange(() => MusicVolume);
        //    }
        //}

        private void LoadSettings()
        {
            UserName = Properties.Settings.Default.Name;
            MusicVolume = Properties.Settings.Default.MusicVol;
            EffectsVolume = Properties.Settings.Default.EffectsVol;
            RotateKey = ((Key)Properties.Settings.Default.Rotate).ToString();
            ////DownKey          = Properties.Settings.Default.Down;
            ////LeftKey          = Properties.Settings.Default.Left;
            ////RightKey         = Properties.Settings.Default.Right;
            ////DropKey          = Properties.Settings.Default.Drop;
            ////HoldKey          = Properties.Settings.Default.Hold;
            ////PauseKey         = Properties.Settings.Default.Pause;
        }
        private void SaveSettings()
        {
            Properties.Settings.Default.Name = UserName;
            Properties.Settings.Default.MusicVol = MusicVolume;
            Properties.Settings.Default.EffectsVol = EffectsVolume;
            Properties.Settings.Default.Rotate = (int)Enum.Parse(typeof(Key), RotateKey);
            //Properties.Settings.Default.Down = (int)DownKey;
            //Properties.Settings.Default.Left = (int)LeftKey;
            //Properties.Settings.Default.Right = (int)RightKey;
            //Properties.Settings.Default.Drop = (int)DropKey;
            //Properties.Settings.Default.Hold = (int)HoldKey;
            //Properties.Settings.Default.Pause = (int)PauseKey;
            Properties.Settings.Default.Save();
        }

        public void SaveAndExit()
        {
            SaveSettings();
            main_window.SetNewView(Screens.MENU);
        }
    }
}
