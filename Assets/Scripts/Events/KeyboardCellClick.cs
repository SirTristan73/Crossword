using UnityEngine;


namespace EventBus
{
    public class KeyboardCellClick : EventType
    {
        public char _inputChar { get; private set; }

        public KeyboardCellClick(char c)
        {
            _inputChar = c;
        }
        
    }
}