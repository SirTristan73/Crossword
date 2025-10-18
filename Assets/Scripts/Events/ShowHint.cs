using UnityEngine;


namespace EventBus
{
    public class ShowHint : EventType
    {
        public WordData _horizontalWord;

        private WordData _verticalWord;

        public string _horizontalHint { get; private set; }

        public string _verticalHint{ get; private set; }


        public ShowHint(WordData horizontalWord, WordData verticalWord)
        {
            if (_horizontalWord != horizontalWord)
            {
                _horizontalWord = horizontalWord;

                _horizontalHint = _horizontalWord._hint;
            }

            if (_verticalWord != verticalWord)
            {
                _verticalWord = verticalWord;

                _verticalHint = _verticalWord._hint;
            }
        }
    }
}