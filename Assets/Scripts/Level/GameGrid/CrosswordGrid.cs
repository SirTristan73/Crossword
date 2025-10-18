using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;



namespace EventBus
{
    public class CrosswordGrid : MonoBehaviour
    {
        [SerializeField] private CrosswordCell _buttonPref;


        [SerializeField] private RectTransform _gridParent;

        [SerializeField] private GridLayoutGroup _gridLayout;

        private int _currentLevelSize;

        private ObjectPool<CrosswordCell> _buttonsPool;

        private readonly List<CrosswordCell> _activeButtons = new();

        private char[] _requestedChars;

        private char[] _currentInputChars;

        private int _currentCellToEdit = -1;


        [SerializeField] private CrosswordLevel _currentLevel;



        private void Awake()
        {
            CreatePoolForGrid();
        }


        private void CreatePoolForGrid()
        {
            _buttonsPool = new ObjectPool<CrosswordCell>(
                        createFunc: () => Instantiate(_buttonPref, _gridParent),
                        actionOnGet: btn => btn.gameObject.SetActive(true),
                        actionOnRelease: btn =>
                        {
                            btn.gameObject.SetActive(false);
                            btn.SetLetter(' ');
                        },
                        actionOnDestroy: btn => Destroy(btn.gameObject),
                        collectionCheck: false,
                        defaultCapacity: 289,
                        maxSize: 289
            );
        }



        private void OnEnable()
        {
            EventBus.SubscribeToEvent<CrosswordCellClick>(GetClickedCell);

            EventBus.SubscribeToEvent<KeyboardCellClick>(KeyboardInput);

            GridSetup();
        }


        private void OnDisable()
        {
            EventBus.UnsubscribeFromEvent<CrosswordCellClick>(GetClickedCell);

            EventBus.UnsubscribeFromEvent<KeyboardCellClick>(KeyboardInput);
        }



        public void GridSetup()
        {
            _currentLevelSize = _currentLevel._size;

            int gridSize = _currentLevelSize * _currentLevelSize;

            CLearGridCache();

            _requestedChars = new char[gridSize];

            _currentInputChars = new char[gridSize];

            foreach (var word in _currentLevel.Words)
            {
                int start = word._startIndex;
                int end = word.EndIndex(_currentLevelSize);

                int step = word._isHorizontal ? 1 : _currentLevelSize;

                for (int i = 0, cell = start;
                        cell <= end && i < word._wordHere.Length;
                        i++, cell += step)
                {
                    _requestedChars[cell] = word._wordHere[i];
                }

            }

            ///
            /// GridLayoutSetup
            ///

            _gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;

            _gridLayout.constraintCount = _currentLevelSize;

            float cellWidth = _gridParent.rect.size.x / _currentLevelSize;

            float cellHeight = _gridParent.rect.size.y / _currentLevelSize;

            float cellSize = Mathf.Min(cellWidth, cellHeight);

            _gridLayout.cellSize = new Vector2(cellSize, cellSize);

            ///
            /// Buttons initialization
            /// 

            for (int i = 0; i < gridSize; i++)
            {
                var btn = _buttonsPool.Get();

                btn.transform.SetParent(_gridLayout.transform, false);

                bool isEmpty = _requestedChars[i] == '\0';

                if (isEmpty)
                {
                    btn.SetState(CrosswordCellState.Disabled);
                }
                else
                {
                    btn.SetState(CrosswordCellState.Default);
                    btn.Init(i);
                }

                _activeButtons.Add(btn);
            }
        }


        private void GetClickedCell(CrosswordCellClick eventData)
        {
            int index = eventData._clickedCellIndex;

            if (_currentCellToEdit == index)
            {
                return;
            }

            _currentCellToEdit = index;

            _currentLevel.GetWordDataAtCell(_currentCellToEdit, out WordData horizontalWord, out WordData verticalWord);

            EventBus.Trigger(new ShowHint(horizontalWord, verticalWord));

        }


        private void KeyboardInput(KeyboardCellClick input)
        {
            if (_currentCellToEdit < 0)
            {
                return;
            }

            char c = input._inputChar;

            _currentInputChars[_currentCellToEdit] = c;

            _activeButtons[_currentCellToEdit].SetLetter(c);

            if (c == _requestedChars[_currentCellToEdit])
            {
                CheckForWord();
            }
        }
        

        private void CheckForWord()
        {
            _currentLevel.GetWordDataAtCell(_currentCellToEdit, out WordData horizontalWord, out WordData verticalWord);

            int step;

            int start;

            int end;          

            if (horizontalWord != null)
            {
                start = horizontalWord._startIndex;

                end = horizontalWord.EndIndex(_currentLevelSize);

                step = 1;

                bool isComplete = true;

                for (int i = 0, cell = start;
                        cell <= end && i < horizontalWord._wordHere.Length;
                        i++, cell += step)
                {
                    if (_requestedChars[cell] != _currentInputChars[cell])
                    {
                        isComplete = false;
                        break;
                    }
                }

                if (isComplete)
                {
                    for (int i = 0, cell = start; cell <= end && i < verticalWord._wordHere.Length; i++, cell += step)
                    {
                        _activeButtons[cell].SetState(CrosswordCellState.Correct);
                    }      
                }
            }
            
            if (verticalWord != null)
            {
                start = verticalWord._startIndex;

                end = verticalWord.EndIndex(_currentLevelSize);

                step = _currentLevelSize;

                bool isComplete = true;

                for (int i = 0, cell = start;
                        cell <= end && i < verticalWord._wordHere.Length;
                        i++, cell += step)
                {
                    if (_requestedChars[cell] != _currentInputChars[cell])
                    {
                        isComplete = false;
                        break;
                    }
                }
                
                if (isComplete)
                {
                    for (int i = 0, cell = start; cell <= end && i < verticalWord._wordHere.Length; i++, cell += step)
                    {
                        _activeButtons[cell].SetState(CrosswordCellState.Correct);
                    }      
                }
            }
        }


        private void CLearGridCache()
        {
            foreach (var btn in _activeButtons)
            {
                _buttonsPool.Release(btn);
            }

            _activeButtons.Clear();
        }
    }


    public enum CrosswordCellState
    {
        Default,
        Correct,
        Disabled
    }


}