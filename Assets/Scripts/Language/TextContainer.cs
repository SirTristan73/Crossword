using UnityEngine;
using System;
using System.Collections.Generic;


namespace EventBus
{
    public class TextContainer : PersistentSingleton<TextContainer>
    {
        public static event Action<Dictionary<string, string>> OnChangeMenuLanguage;
        public static event Action<Dictionary<string, (string characterName, string text)>> OnGameLanguageChange;


        public LanguageState CurrentLanguage { get; private set; }

        [SerializeField] private LanguageData[] _languageData;



        private Dictionary<string, string> _currentMenuTexts;
        public Dictionary<string, string> CurrentMenuTexts => _currentMenuTexts;


        private Dictionary<string, (string characterName, string text)> _currentGameTexts;
        public Dictionary<string, (string characterName, string text)> CurrentDialogueTexts => _currentGameTexts;



        private static readonly Dictionary<LanguageState, Type> LanguageMap = new()
        {

                {LanguageState.English, typeof(EnglishData) },
                {LanguageState.Russian, typeof(RussianData) }

        };



        public void SetTextLanguage(LanguageState language)
        {
            // if (CurrentLanguage == language) return;
            CurrentLanguage = language;

            if (LanguageMap.TryGetValue(language, out var type))
            {
                _currentGameTexts = GetDialogueTexts(type);
                _currentMenuTexts = GetMenuTexts(type) ?? new Dictionary<string, string>();
            }
            else
            {
                Debug.LogError($"Language {language} not found");
            }


            OnChangeMenuLanguage?.Invoke(_currentMenuTexts);
            OnGameLanguageChange?.Invoke(_currentGameTexts);

        }


        private Dictionary<string, string> GetMenuTexts(Type type)
        {
            foreach (var data in _languageData)
            {
                if (data.GetType() == type)
                {
                    return data.MenuTexts;
                }
            }
            Debug.LogError("NotFOund");
            return null;
        }


        private Dictionary<string, (string characterName, string text)> GetDialogueTexts(Type type)
        {
            foreach (var data in _languageData)
            {
                if (data.GetType() == type)
                {
                    return data.DialogueTexts;
                }
            }
            Debug.LogError("NotFOund");
            return new Dictionary<string, (string, string)>();
        }


    }
        public enum LanguageState
        {
            English = 0,
            Russian = 1,
            Ukrainian = 2,
        }
}