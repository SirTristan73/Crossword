using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;


namespace EventBus
{
    public class Hintfield : MonoBehaviour
    {
        [SerializeField] private TMP_Text _horizontalHintfieldText;

        [SerializeField] private TMP_Text _verticalHintfieldText;

        private float _charDelay = 0.025f;

        private string _currentLanguageHorizontal;

        private string _currentLanguageVertical;

        public string _currentHint { get; private set; }



        private void OnEnable()
        {
            EventBus.SubscribeToEvent<ShowHint>(DisplayHint);

        }


        private void OnDisable()
        {
            EventBus.UnsubscribeFromEvent<ShowHint>(DisplayHint);    
        }



        private void DisplayHint(ShowHint eventData)
        {
            GetCurrentMenuText();

            if (eventData == null)
            {
                return;
            }

            StopAllCoroutines();

            _horizontalHintfieldText.text = "";

            _verticalHintfieldText.text = "";

            string horizontalHint = eventData._horizontalHint ?? string.Empty;

            string verticalHint = eventData._verticalHint ?? string.Empty;

            if (!string.IsNullOrEmpty(eventData._horizontalHint))
            {
                _horizontalHintfieldText.text = _currentLanguageHorizontal;
            }

            if (!string.IsNullOrEmpty(eventData._verticalHint))
            {
                _verticalHintfieldText.text = _currentLanguageVertical;
            }

            StartCoroutine(ShowHintAnimation(horizontalHint, verticalHint));
        }

        private void GetCurrentMenuText()
        {
            _currentLanguageHorizontal = TextContainer.Instance.CurrentMenuTexts["horizontalWordHint"];

            _currentLanguageVertical = TextContainer.Instance.CurrentMenuTexts["veritcalWordHint"];
        }

        private IEnumerator ShowHintAnimation(string horizontalHint, string verticalHint)
        {
    int maxLength = Mathf.Max(horizontalHint.Length, verticalHint.Length);

    for (int i = 0; i < maxLength; i++)
    {
        if (i < horizontalHint.Length)
            _horizontalHintfieldText.text += horizontalHint[i];

        if (i < verticalHint.Length)
            _verticalHintfieldText.text += verticalHint[i];

        yield return new WaitForSeconds(_charDelay);
    }
        }


        
    }
}