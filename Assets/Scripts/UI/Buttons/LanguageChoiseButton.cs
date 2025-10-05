using EventBus;
using UnityEngine;
using UnityEngine.UI;

namespace EventBus
{
    public class LanguageChoiseButton : MonoBehaviour
    {
        public Button _languageButton;
        public LanguageState _language;



        private void OnEnable()
        {
            _languageButton.onClick.AddListener(OnLanguageChosen);
        }


        private void OnDisable()
        {
            _languageButton.onClick.RemoveListener(OnLanguageChosen);
        }


        private void OnLanguageChosen()
        {
            EventBus.Trigger(new LanguageChosenEvent(_language));
        }
    }
}