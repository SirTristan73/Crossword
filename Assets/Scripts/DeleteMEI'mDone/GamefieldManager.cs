using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;



namespace EventBus
{
    public class GamefieldManager : MonoBehaviour
    {
        public GameObject _plsl;

        public GameObject _pointer; // TODO make pointer separate

        private int _currentCellToEdit = -1;
        //DELETEABOVE

        [SerializeField] private TMP_Text _textField; // Transfer

        [SerializeField] private TMP_Text _hintField; // Transfer to separate

        [SerializeField] private CrosswordLevel _level; //Edit 

        [SerializeField] private char[] _gridGamefield;

        [SerializeField] private RectTransform _playArea;

        private Bounds _gamefieldGridBounds;

        private float _cellSize;

        private float _gamefieldPaddingX;
        private float _gamefieldPaddingY;


        private void Start()
        {
            GetBoundsForGrid();

            FillStringForLevel();

            PlayerInput._clickCallback += GetInput;
        }


        private void FillStringForLevel()
        {
            int totalCells = _level._size * _level._size;

            int size = _level._size;

            _gridGamefield = new char[totalCells];

            for (int i = 0; i < totalCells; i++)
            {
                _gridGamefield[i] = ' ';
            }

            foreach (var word in _level.Words)
            {
                int start = word._startIndex;
                int end = word.EndIndex(size);

                int step = word._isHorizontal ? 1 : size;

                for (int i = 0, cell = start;
                        cell <= end && i < word._wordHere.Length;
                        i++, cell += step)
                {
                    _gridGamefield[cell] = word._wordHere[i];
                }

            }

            string textInLevel = "";

            for (int y = 0; y < size; y++)
            {
                textInLevel += new string(_gridGamefield, y * size, size);

                if (y < size - 1)
                {
                    textInLevel += "\n";
                }
            }

            _textField.text = textInLevel;

        }


        private void GetBoundsForGrid()
        {
            Vector3[] corners = new Vector3[4];

            _playArea.GetWorldCorners(corners);

            Vector3 min = corners[0];

            Vector3 max = corners[2];

            Vector3 center = (min + max) / 2f;

            Vector3 panelSize = max - min;

            _gamefieldGridBounds = new Bounds(center, panelSize);

            _cellSize = Mathf.Min(_gamefieldGridBounds.size.x, _gamefieldGridBounds.size.y) / _level._size;

            _gamefieldPaddingX = (_gamefieldGridBounds.size.x - _cellSize * _level._size) / 2f;
            _gamefieldPaddingY = (_gamefieldGridBounds.size.y - _cellSize * _level._size) / 2f;

            Debug.Log(_cellSize); // Size needed for counting cellsize in final version

            for (int y = 0; y < _level._size; y++) //Placeholder for visual
            {
                for (int x = 0; x < _level._size; x++)
                {
                    GameObject pop;
                    Vector3 pos = new Vector3(_gamefieldGridBounds.min.x + _cellSize / 2 + x * _cellSize, _gamefieldGridBounds.min.y + _cellSize / 2 + y * _cellSize, 0f);
                    pop = Instantiate(_plsl, pos, Quaternion.identity, _playArea.transform);
                    pop.transform.localScale = new Vector3(_cellSize, _cellSize, 0f);
                }
            }
            
            _pointer.transform.localScale = new Vector3(_cellSize, _cellSize, 0f);

        }


        private void GetInput(Vector3 point)
        {
            point.z = _gamefieldGridBounds.center.z;

            if (_gamefieldGridBounds.Contains(point))
            {
                Vector3 localPos = point - _gamefieldGridBounds.min;

                localPos.x -= _gamefieldPaddingX;
                localPos.y -= _gamefieldPaddingY;

                int maxValue = _level._size - 1;

                int targetXvalue = Mathf.FloorToInt(localPos.x / _cellSize);

                int targetYvalue = maxValue - Mathf.FloorToInt(localPos.y / _cellSize);

                int column = Mathf.Clamp(targetXvalue, 0, maxValue);

                int row = Mathf.Clamp(targetYvalue, 0, maxValue);

                int cell = row * _level._size + column;

                Debug.Log($"Clicked collumn: {column}, row: {row}");

                if (_currentCellToEdit != cell && !_level.CheckIfCellBlocked(cell))
                {
                    _currentCellToEdit = cell;
                                                                //TODO 
                    _pointer.transform.position = new Vector3(    
                        _gamefieldGridBounds.min.x + _gamefieldPaddingX + column * _cellSize + _cellSize / 2f,
                        _gamefieldGridBounds.min.y + _gamefieldPaddingY + (_level._size - 1 - row) * _cellSize + _cellSize / 2f,
                        _pointer.transform.position.z
                    );
                    
                    Debug.Log($"Current cell: {_currentCellToEdit}");
                }
            }
        }

    }
}

