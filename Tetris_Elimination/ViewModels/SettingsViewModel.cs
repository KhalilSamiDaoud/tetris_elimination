using static Tetris_Elimination.Models.ConstantsModel;
using System.Collections.Generic;
using Tetris_Elimination.Models;
using System.Windows.Input;
using Caliburn.Micro;
using System;

namespace Tetris_Elimination.ViewModels
{
    /// <summary>The SettingsViewModel is used to load and save new user settings inclidiong name, volume, and key-binds.</summary>
    /// <seealso cref="Caliburn.Micro.Screen" />
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

        /// <summary>Initializes a new instance of the <see cref="SettingsViewModel" /> class.</summary>
        /// <param name="_mainWindow">The main window.</param>
        public SettingsViewModel(MainViewModel _mainWindow)
        {
            mainWindow   = _mainWindow;
            audioManager = AudioManagerModel.Instance;

            mainWindow.SetBackground = BACKGROUND_SETTINGS;
            mainWindow.SetShade      = .5;

            CreateKeyList();
            LoadSettings();
        }

        /// <summary>Creates the key list.</summary>
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

        /// <summary>Checks the input.</summary>
        /// <param name="e">The e.</param>
        /// <param name="keyBind">The key bind.</param>
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

        /// <summary>Texts the box clear.</summary>
        /// <param name="keyBind">The key bind.</param>
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

        /// <summary>Checks the not empty.</summary>
        /// <param name="keyBind">The key bind.</param>
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

        /// <summary>Translates the key string.</summary>
        /// <param name="keyString">The key string.</param>
        /// <returns>The Key string.</returns>
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

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }

        /// <summary>Gets or sets the effects volume.</summary>
        /// <value>The effects volume.</value>
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

        /// <summary>Gets or sets the music volume.</summary>
        /// <value>The music volume.</value>
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

        /// <summary>Gets or sets the rotate key.</summary>
        /// <value>The rotate key.</value>
        public string RotateKey
        {
            get { return _rotateKey; }
            set
            {
                _rotateKey = TranslateKeyString(value);
                NotifyOfPropertyChange(() => RotateKey);
            }
        }

        /// <summary>Gets or sets down key.</summary>
        /// <value>Down key.</value>
        public string DownKey
        {
            get{ return _downKey; }
            set
            {
                _downKey = TranslateKeyString(value);
                NotifyOfPropertyChange(() => DownKey);
            }
        }

        /// <summary>Gets or sets the left key.</summary>
        /// <value>The left key.</value>
        public string LeftKey
        {
            get { return _leftKey; }
            set
            {
                _leftKey = TranslateKeyString(value);
                NotifyOfPropertyChange(() => LeftKey);
            }
        }

        /// <summary>Gets or sets the right key.</summary>
        /// <value>The right key.</value>
        public string RightKey
        {
            get { return _rightKey; }
            set
            {
                _rightKey = TranslateKeyString(value);
                NotifyOfPropertyChange(() => RightKey);
            }
        }

        /// <summary>Gets or sets the drop key.</summary>
        /// <value>The drop key.</value>
        public string DropKey
        {
            get { return _dropKey; }
            set
            {
                _dropKey = TranslateKeyString(value);
                NotifyOfPropertyChange(() => DropKey);
            }
        }

        /// <summary>Gets or sets the hold key.</summary>
        /// <value>The hold key.</value>
        public string HoldKey
        {
            get { return _holdKey; }
            set
            {
                _holdKey = TranslateKeyString(value);
                NotifyOfPropertyChange(() => HoldKey);
            }
        }

        /// <summary>Gets or sets the pause key.</summary>
        /// <value>The pause key.</value>
        public string PauseKey
        {
            get { return _pauseKey; }
            set
            {
                _pauseKey = TranslateKeyString(value);
                NotifyOfPropertyChange(() => PauseKey);
            }
        }

        /// <summary>Loads the settings.</summary>
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

        /// <summary>Saves the settings.</summary>
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

        /// <summary>Saves and exits.</summary>
        public void SaveAndExit()
        {
            SaveSettings();
            mainWindow.SetNewView(Screens.MENU);
        }
    }
}
