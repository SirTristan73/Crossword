using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


namespace EventBus
{
    public class ConfirmButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [SerializeField] private string _thisButtonKey;

        [SerializeField] private TMP_Text _buttonText;


        private void OnEnable()
        {
            _button.onClick.AddListener(ConfirmAction);

            if (TextContainer.Instance.CurrentMenuTexts != null)
            {
                ButtonText(TextContainer.Instance.CurrentMenuTexts);
            }
            else
            {
                Debug.LogWarning($"Key {_thisButtonKey} not assigned");
            }

            TextContainer.OnChangeMenuLanguage += ButtonText;
        }


        private void OnDisable()
        {
            _button.onClick.RemoveListener(ConfirmAction);
            TextContainer.OnChangeMenuLanguage -= ButtonText;
        }


        private void ConfirmAction()
        {
            UIManager.Instance.PopOverlay();
        }


        private void ButtonText(Dictionary<string, string> menuText)
        {
            _buttonText.text = menuText[_thisButtonKey];
        }
    }
}