using UnityEngine;
using System;
using System.Collections.Generic;


namespace EventBus
{
    public class TextContainer : PersistentSingleton<TextContainer>
    {
        public static event Action<Dictionary<string, string>> OnChangeMenuLanguage;

        public LanguageState CurrentLanguage { get; private set; }

        [SerializeField] private LanguageData[] _languageData;

        private Dictionary<string, string> _currentMenuTexts;
        public Dictionary<string, string> CurrentMenuTexts => _currentMenuTexts;


        private Dictionary<string, (string word, string hint)> _currentGameTexts;
        public Dictionary<string, (string word, string hint)> CurrentGameText => _currentGameTexts;

        private char[] _currentAlphabet;
        private int[] _currentKeyboardLengths;

        public int[] KeyboardRowLengths => _currentKeyboardLengths;
        public char[] KeyboardAlphabet => _currentAlphabet;


        /// <summary>
        /// Refactor with crossword requirements
        /// </summary>

        private static readonly Dictionary<LanguageState, Type> LanguageMap = new()
        {

                {LanguageState.English, typeof(EnglishData) },
                {LanguageState.Russian, typeof(RussianData) }

        };



        public void SetTextLanguage(LanguageState language)
        {

            CurrentLanguage = language;

            if (LanguageMap.TryGetValue(language, out var type))
            {
                _currentGameTexts = GetDialogueTexts(type);

                _currentMenuTexts = GetMenuTexts(type) ?? new Dictionary<string, string>();

                _currentAlphabet = GetKeyboardAlphabet(type);

                _currentKeyboardLengths = GetKeyboardLenghts(type);
            }
            else
            {
                Debug.LogError($"Language {language} not found");
            }

            OnChangeMenuLanguage?.Invoke(_currentMenuTexts);
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


        private char[] GetKeyboardAlphabet(Type type)
        {
            foreach (var data in _languageData)
            {
                if (data.GetType() == type)
                {
                    return data.KeyboardAlphabet;
                }
            }

            Debug.LogError("Not found");
            return null;
        }


        private int[] GetKeyboardLenghts(Type type)
        {
            foreach (var data in _languageData)
            {
                if (data.GetType() == type)
                {
                    return data.KeyboardRowLengths;
                }
            }

            Debug.LogError("Not found");
            return null;
        }


    }
        public enum LanguageState
        {
            English = 0,
            Russian = 1,
            Ukrainian = 2,
        }
}