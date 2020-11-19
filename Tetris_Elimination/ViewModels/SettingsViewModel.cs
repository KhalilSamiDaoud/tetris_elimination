using static Tetris_Elimination.Models.ConstantsModel;
using System.Collections.Generic;
using Tetris_Elimination.Models;
using System.Windows.Input;
using Caliburn.Micro;
using System;

namespace Tetris_Elimination.ViewModels
{
    class SettingsViewModel : Screen
    {
        private List<UserKeyBindModel> userKeys;
        private AudioManagerModel audioManager;        
        private MainViewModel mainWindow;
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

        public SettingsViewModel(MainViewModel _mainWindow)
        {
            mainWindow   = _mainWindow;
            audioManager = AudioManagerModel.Instance;

            mainWindow.SetBackground = BACKGROUND_SETTINGS;
            mainWindow.SetShade      = .5;

            CreateKeyList();
            LoadSettings();
        }

        private void CreateKeyList()
        {
            userKeys = new List<UserKeyBindModel>();

            userKeys.Add(new UserKeyBindModel((Key)Properties.Settings.Default.Rotate));
            userKeys.Add(new UserKeyBindModel((Key)Properties.Settings.Default.Down));
            userKeys.Add(new UserKeyBindModel((Key)Properties.Settings.Default.Left));
            userKeys.Add(new UserKeyBindModel((Key)Properties.Settings.Default.Right));
            userKeys.Add(new UserKeyBindModel((Key)Properties.Settings.Default.Drop));
            userKeys.Add(new UserKeyBindModel((Key)Properties.Settings.Default.Hold));
            userKeys.Add(new UserKeyBindModel((Key)Properties.Settings.Default.Pause));
        }

        public void CheckInput(ActionExecutionContext e, KeyBind keyBind)
        {
            var keyArgs = e.EventArgs as KeyEventArgs;

            TextBoxClear(keyBind);

            switch (keyBind)
            {
                case KeyBind.ROTATE:
                    userKeys[0] = new UserKeyBindModel(keyArgs.Key);
                    RotateKey   = userKeys[0].GetString();
                    break;
                case KeyBind.DOWN:
                    userKeys[1] = new UserKeyBindModel(keyArgs.Key);
                    DownKey     = userKeys[1].GetString();
                    break;
                case KeyBind.LEFT:
                    userKeys[2] = new UserKeyBindModel(keyArgs.Key);
                    LeftKey     = userKeys[2].GetString();
                    break;
                case KeyBind.RIGHT:
                    userKeys[3] = new UserKeyBindModel(keyArgs.Key);
                    RightKey    = userKeys[3].GetString();
                    break;
                case KeyBind.DROP:
                    userKeys[4] = new UserKeyBindModel(keyArgs.Key);
                    DropKey     = userKeys[4].GetString();
                    break;
                case KeyBind.HOLD:
                    userKeys[5] = new UserKeyBindModel(keyArgs.Key);
                    HoldKey     = userKeys[5].GetString();
                    break;
                case KeyBind.PAUSE:
                    userKeys[6] = new UserKeyBindModel(keyArgs.Key);
                    PauseKey    = userKeys[6].GetString();
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
                    DownKey   = Key.None.ToString();
                    break;
                case KeyBind.LEFT:
                    LeftKey   = Key.None.ToString();
                    break;
                case KeyBind.RIGHT:
                    RightKey  = Key.None.ToString();
                    break;
                case KeyBind.DROP:
                    DropKey   = Key.None.ToString();
                    break;
                case KeyBind.HOLD:
                    HoldKey   = Key.None.ToString();
                    break;
                case KeyBind.PAUSE:
                    PauseKey  = Key.None.ToString();
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
                        RotateKey = userKeys[0].GetString();
                    break;
                case KeyBind.DOWN:
                    if (String.IsNullOrEmpty(DownKey))
                        DownKey   = userKeys[1].GetString();
                    break;
                case KeyBind.LEFT:
                    if (String.IsNullOrEmpty(LeftKey))
                        LeftKey   = userKeys[2].GetString();
                    break;
                case KeyBind.RIGHT:
                    if (String.IsNullOrEmpty(RightKey))
                        RightKey  = userKeys[3].GetString();
                    break;
                case KeyBind.DROP:
                    if (String.IsNullOrEmpty(DropKey))
                        DropKey   = userKeys[4].GetString();
                    break;
                case KeyBind.HOLD:
                    if (String.IsNullOrEmpty(HoldKey))
                        HoldKey   = userKeys[5].GetString();
                    break;
                case KeyBind.PAUSE:
                    if (String.IsNullOrEmpty(PauseKey))
                        PauseKey  = userKeys[6].GetString();
                    break;
                default:
                    break;
            }
        }

        private string TranslateKeyString(string keyString)
        {
            if (keyString == Key.None.ToString())
            {
                return String.Empty;
            }
            else
            {
                return keyString;
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
            get { return _effectsVolume; }
            set
            {
                _effectsVolume = value;
                audioManager.SetVolume(_effectsVolume, _musicVolume);
                NotifyOfPropertyChange(() => EffectsVolume);
            }
        }

        public double MusicVolume
        {
            get { return _musicVolume; }
            set
            {
                _musicVolume = value;
                audioManager.SetVolume(_effectsVolume, _musicVolume);
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
            get{ return _downKey; }
            set
            {
                _downKey = TranslateKeyString(value);
                NotifyOfPropertyChange(() => DownKey);
            }
        }

        public string LeftKey
        {
            get { return _leftKey; }
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
            get { return _holdKey; }
            set
            {
                _holdKey = TranslateKeyString(value);
                NotifyOfPropertyChange(() => HoldKey);
            }
        }

        public string PauseKey
        {
            get { return _pauseKey; }
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
            RotateKey      = userKeys[0].GetString();
            DownKey        = userKeys[1].GetString();
            LeftKey        = userKeys[2].GetString();
            RightKey       = userKeys[3].GetString();
            DropKey        = userKeys[4].GetString();
            HoldKey        = userKeys[5].GetString();
            PauseKey       = userKeys[6].GetString();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.Name        = UserName;
            Properties.Settings.Default.MusicVol    = MusicVolume;
            Properties.Settings.Default.EffectsVol  = EffectsVolume;
            Properties.Settings.Default.Rotate      = userKeys[0].GetInt();
            Properties.Settings.Default.Down        = userKeys[1].GetInt();
            Properties.Settings.Default.Left        = userKeys[2].GetInt();
            Properties.Settings.Default.Right       = userKeys[3].GetInt();
            Properties.Settings.Default.Drop        = userKeys[4].GetInt();
            Properties.Settings.Default.Hold        = userKeys[5].GetInt();
            Properties.Settings.Default.Pause       = userKeys[6].GetInt();
            Properties.Settings.Default.Save();
        }

        public void SaveAndExit()
        {
            SaveSettings();
            mainWindow.SetNewView(Screens.MENU);
        }
    }
}
