using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


namespace EventBus
{
    public class ButtonOnClick : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [SerializeField] private string _thisButtonKey;

        [SerializeField] private TMP_Text _buttonText;



        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
            if (TextContainer.Instance.CurrentMenuTexts != null)
            {
                ButtonText(TextContainer.Instance.CurrentMenuTexts);
                TextContainer.OnChangeMenuLanguage += ButtonText;
            }
            else
            {
                Debug.LogWarning($"Key {_thisButtonKey} not assigned");
            }
        }


        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
            TextContainer.OnChangeMenuLanguage -= ButtonText;
        }


        private void OnButtonClicked()
        {
            EventBus.Trigger(new ButtonOnCLickEvent(_thisButtonKey));
        }
        

        private void ButtonText(Dictionary<string, string> menuText)
        {
            _buttonText.text = menuText[_thisButtonKey];
        }
    }
}