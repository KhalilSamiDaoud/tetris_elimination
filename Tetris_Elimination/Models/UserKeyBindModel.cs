using System.Windows.Input;
using System;

namespace Tetris_Elimination.Models
{
    public class UserKeyBindModel
    {
        private String keyStringRepresentation;
        private int keyIntRepresentation;

        public UserKeyBindModel(Key keyType)
        {
            DetermineValues(keyType);
        }

        private void DetermineValues(Key keyType)
        {
            keyStringRepresentation = keyType.ToString().ToUpper();
            keyIntRepresentation    = (int)keyType;
        }

        public String GetString()
        {
            return keyStringRepresentation;
        }

        public int GetInt()
        {
            return keyIntRepresentation;
        }
    }
}
