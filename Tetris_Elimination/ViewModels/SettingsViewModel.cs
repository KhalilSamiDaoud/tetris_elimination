using static Tetris_Elimination.Models.ConstantsModel;
using Tetris_Elimination.Models;
using System.Windows.Input;
using Caliburn.Micro;
using System;

namespace Tetris_Elimination.ViewModels
{

    //add all controls to dictionary to make things cleaner

    class SettingsViewModel : Screen
    {
        private AudioManagerModel audioManager;
        private MainViewModel main_window;
        private Double _effectsVolume;
        private Double _musicVolume;
        private String _userName;
        private String _rotateKey;
        private String _downKey;
        private String _leftKey;
        private String _rightKey;
        private String _dropKey;
        private String _holdKey;
        private String _pauseKey;

        public SettingsViewModel(MainViewModel _main_window)
        {
            main_window  = _main_window;
            audioManager = AudioManagerModel.Instance;

            LoadSettings();
        }

        public void CheckInput(ActionExecutionContext e, KeyBind keyBind)
        {
            var keyArgs = e.EventArgs as KeyEventArgs;

            TextBoxClear(keyBind);

            switch (keyBind)
            {
                case KeyBind.ROTATE:
                    RotateKey = keyArgs.Key.ToString();
                    break;
                case KeyBind.DOWN:
                    DownKey   = keyArgs.Key.ToString();
                    break;
                case KeyBind.LEFT:
                    LeftKey   = keyArgs.Key.ToString();
                    break;
                case KeyBind.RIGHT:
                    RightKey  = keyArgs.Key.ToString();
                    break;
                case KeyBind.DROP:
                    DropKey   = keyArgs.Key.ToString();
                    break;
                case KeyBind.HOLD:
                    HoldKey   = keyArgs.Key.ToString();
                    break;
                case KeyBind.PAUSE:
                    PauseKey  = keyArgs.Key.ToString();
                    break;
                default:
                    break;
            }
        }

        public void TextBoxClear(KeyBind keyBind)
        {
            switch (keyBind)
            {
                case KeyBind.ROTATE:
                    RotateKey = Key.None.ToString();
                    break;
                case KeyBind.DOWN:
                    DownKey = Key.None.ToString();
                    break;
                case KeyBind.LEFT:
                    LeftKey = Key.None.ToString();
                    break;
                case KeyBind.RIGHT:
                    RightKey = Key.None.ToString();
                    break;
                case KeyBind.DROP:
                    DropKey = Key.None.ToString();
                    break;
                case KeyBind.HOLD:
                    HoldKey = Key.None.ToString();
                    break;
                case KeyBind.PAUSE:
                    PauseKey = Key.None.ToString();
                    break;
                default:
                    break;
            }
        }

        public void CheckNotEmpty(KeyBind keyBind)
        {
            switch (keyBind)
            {
                case KeyBind.ROTATE:
                    if (String.IsNullOrEmpty(RotateKey))
                        RotateKey = ((Key)Properties.Settings.Default.Rotate).ToString();
                    break;
                case KeyBind.DOWN:
                    DownKey = Key.None.ToString();
                    break;
                case KeyBind.LEFT:
                    LeftKey = Key.None.ToString();
                    break;
                case KeyBind.RIGHT:
                    RightKey = Key.None.ToString();
                    break;
                case KeyBind.DROP:
                    DropKey = Key.None.ToString();
                    break;
                case KeyBind.HOLD:
                    HoldKey = Key.None.ToString();
                    break;
                case KeyBind.PAUSE:
                    PauseKey = Key.None.ToString();
                    break;
                default:
                    break;
            }
        }

        private string TranslateKeyString(string keyString)
        {
            if (keyString == Key.None.ToString())
            {
                return "";
            }
            else
            {
                return keyString.ToUpper();
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
            { return _effectsVolume; }
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
            { return _musicVolume; }
            set
            {
                _musicVolume = value;
                audioManager.setVolume(_effectsVolume, _musicVolume);
                NotifyOfPropertyChange(() => MusicVolume);
            }
        }

        public string RotateKey
        {
            get { return _rotateKey; }
            set
            {
                _rotateKey = TranslateKeyString(value);
                NotifyOfPropertyChange(() => RotateKey);
            }
        }

        public string DownKey
        {
            get
            {
                return _downKey;
            }
            set
            {
                _downKey = TranslateKeyString(value);
                NotifyOfPropertyChange(() => DownKey);
            }
        }

        public string LeftKey
        {
            get{ return _leftKey; }
            set
            {
                _leftKey = TranslateKeyString(value);
                NotifyOfPropertyChange(() => LeftKey);
            }
        }

        public string RightKey
        {
            get { return _rightKey; }
            set
            {
                _rightKey = TranslateKeyString(value);
                NotifyOfPropertyChange(() => RightKey);
            }
        }

        public string DropKey
        {
            get { return _dropKey; }
            set
            {
                _dropKey = TranslateKeyString(value);
                NotifyOfPropertyChange(() => DropKey);
            }
        }

        public string HoldKey
        {
            get
            { return _holdKey; }
            set
            {
                _holdKey = TranslateKeyString(value);
                NotifyOfPropertyChange(() => HoldKey);
            }
        }

        public string PauseKey
        {
            get
            { return _pauseKey; }
            set
            {
                _pauseKey = TranslateKeyString(value);
                NotifyOfPropertyChange(() => PauseKey);
            }
        }

        private void LoadSettings()
        {
            UserName       = Properties.Settings.Default.Name;
            MusicVolume    = Properties.Settings.Default.MusicVol;
            EffectsVolume  = Properties.Settings.Default.EffectsVol;
            RotateKey      = ((Key)Properties.Settings.Default.Rotate).ToString();
            DownKey        = ((Key)Properties.Settings.Default.Down).ToString();
            LeftKey        = ((Key)Properties.Settings.Default.Left).ToString();
            RightKey       = ((Key)Properties.Settings.Default.Right).ToString();
            DropKey        = ((Key)Properties.Settings.Default.Drop).ToString();
            HoldKey        = ((Key)Properties.Settings.Default.Hold).ToString();
            PauseKey       = ((Key)Properties.Settings.Default.Pause).ToString();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.Name        = UserName;
            Properties.Settings.Default.MusicVol    = MusicVolume;
            Properties.Settings.Default.EffectsVol  = EffectsVolume;
            Properties.Settings.Default.Rotate      = (int)Enum.Parse(typeof(Key), (RotateKey[0] + RotateKey.Substring(1).ToLower()));
            Properties.Settings.Default.Down        = (int)Enum.Parse(typeof(Key), (DownKey[0] + DownKey.Substring(1).ToLower()));
            Properties.Settings.Default.Left        = (int)Enum.Parse(typeof(Key), (LeftKey[0] + LeftKey.Substring(1).ToLower()));
            Properties.Settings.Default.Right       = (int)Enum.Parse(typeof(Key), (RightKey[0] + RightKey.Substring(1).ToLower()));
            Properties.Settings.Default.Drop        = (int)Enum.Parse(typeof(Key), (DropKey[0] + DropKey.Substring(1).ToLower()));
            Properties.Settings.Default.Hold        = (int)Enum.Parse(typeof(Key), (HoldKey[0] + HoldKey.Substring(1).ToLower()));
            Properties.Settings.Default.Pause       = (int)Enum.Parse(typeof(Key), (PauseKey[0] + PauseKey.Substring(1).ToLower()));
            Properties.Settings.Default.Save();
        }

        public void SaveAndExit()
        {
            SaveSettings();
            main_window.SetNewView(Screens.MENU);
        }
    }
}
