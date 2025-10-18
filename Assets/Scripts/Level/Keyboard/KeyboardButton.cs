using TMPro;
using UnityEngine;
using UnityEngine.UI;



namespace EventBus
{
    public class KeyboardButton : MonoBehaviour
    {
        [SerializeField] private Button _keyboardButton;

        [SerializeField] private TMP_Text _keyboardLetter;

        public char _thisKeyChar { get; private set; }



        public void InitKeyboardKey(char c)
        {
            _thisKeyChar = c;

            _keyboardLetter.text = _thisKeyChar.ToString();

            _keyboardButton.onClick.AddListener(OnClick);
        }


        private void OnDisable()
        {
            _keyboardButton.onClick.RemoveAllListeners();           
        }



        private void OnClick()
        {
            EventBus.Trigger(new KeyboardCellClick(_thisKeyChar));
        }


    }
}