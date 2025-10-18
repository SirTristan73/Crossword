using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;



namespace EventBus
{
    public class KeyboardGrid : MonoBehaviour
    {
        [SerializeField] private KeyboardButton _defaultKey;

        [SerializeField] private List<Transform> _keyboardRows;



        private ObjectPool<KeyboardButton> _keysPool;

        private readonly List<KeyboardButton> _activeKeys = new();

        private char[] _keyboardChars;


        private void Awake()
        {
            CreateKeysPool();
        }


        private void Start()
        {
            TextContainer.Instance.SetTextLanguage(LanguageState.English);
            SetupKeyboard();
        }


        private void CreateKeysPool()
        {
            _keysPool = new ObjectPool<KeyboardButton>(
            createFunc: () => Instantiate(_defaultKey),
            actionOnGet: btn => btn.gameObject.SetActive(true),
            actionOnRelease: key =>
            {
                key.gameObject.SetActive(false);
            },
            actionOnDestroy: btn => Destroy(btn.gameObject),
            collectionCheck: false,
            defaultCapacity: 40,
            maxSize: 50
            );
        }


        public void SetupKeyboard()
        {
            ClearKeysPool();

            var letters = TextContainer.Instance.KeyboardAlphabet;

            var rowLengths = TextContainer.Instance.KeyboardRowLengths;

            if (letters == null || rowLengths == null)
            {
                return;
            }

            int letterInd = 0;

            for (int row = 0; row < rowLengths.Length; row++)
            {
                int count = rowLengths[row];

                if (row >= _keyboardRows.Count)
                {
                    break;
                }

                var parent = _keyboardRows[row];

                for (int i = 0; i < count; i++)
                {
                    if (letterInd >= letters.Length)
                    {
                        break;
                    }

                    var key = _keysPool.Get();

                    key.transform.SetParent(parent, false);

                    key.InitKeyboardKey(letters[letterInd]);

                    _activeKeys.Add(key);

                    letterInd++;
                }
            }
        }


        private void ClearKeysPool()
        {
            foreach (var key in _activeKeys)
            {
                _keysPool.Release(key);
            }

            _activeKeys.Clear();
        }
    }
}