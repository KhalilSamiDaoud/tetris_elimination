using System.Windows.Input;
using System;

namespace Tetris_Elimination.Models
{
    /// <summary>The UserKeyBindModel is used by SettingsViewModel to store key type conversions.</summary>
    public class UserKeyBindModel
    {
        private String keyStringRepresentation;
        private int keyIntRepresentation;

        /// <summary>Initializes a new instance of the <see cref="UserKeyBindModel" /> class.</summary>
        /// <param name="keyType">The key to store and modify.</param>
        public UserKeyBindModel(Key keyType)
        {
            DetermineValues(keyType);
        }

        /// <summary>Determines the values if the keybind model by getting the ToString() and Integer cast.</summary>
        /// <param name="keyType">The Key to convert.</param>
        private void DetermineValues(Key keyType)
        {
            keyStringRepresentation = keyType.ToString().ToUpper();
            keyIntRepresentation    = (int)keyType;
        }

        /// <summary>Gets the string representation of the stores Key.</summary>
        /// <returns>Gets the string representation</returns>
        public String GetString()
        {
            return keyStringRepresentation;
        }

        /// <summary>Gets the Integer representation of the stores Key.</summary>
        /// <returns>Gets the Integer representation.</returns>
        public int GetInt()
        {
            return keyIntRepresentation;
        }
    }
}
