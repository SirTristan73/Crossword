using System;
using System.Linq;
using EventBus;
using Mono.Cecil.Cil;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class KeyboardManager : MonoBehaviour
{

    [SerializeField] private TMP_Text _keyboardLetters;

    [SerializeField] private char[] _keyboardAlphabet;

    [SerializeField] private RectTransform _keyboardArea;

    private Bounds _keyboardBounds;

    private float _keyboardButtonSize;

    private int[] _rowsLengths;

    private int _rowsCount;

    // private float _keyboardPaddingX;

    private float _keyboardPaddingY;


    
    void Start()
    {
        EventBus.TextContainer.Instance.SetTextLanguage(LanguageState.English);

        FillKeyboard();

        GetBoundsForGrid();

        PlayerInput._clickCallback += GetInputForKeyboard;
    }


    private void FillKeyboard()
    {

        if (EventBus.TextContainer.Instance == null)
        {
            return;
        }
        
        var libraryKeyboard = EventBus.TextContainer.Instance.KeyboardAlphabet;

        // int totalCells = libraryKeyboard.Length;

        _keyboardAlphabet = libraryKeyboard;

        _rowsLengths = EventBus.TextContainer.Instance.KeyboardRowLengths;

        _rowsCount = _rowsLengths.Length;

        string textInLevel = "";

        int start = 0;

        for(int y = 0; y < _rowsCount; y++)
        {
            int length = _rowsLengths[y];
            
            textInLevel += new string(_keyboardAlphabet, start, length);

            start += length;

            if(y < _rowsCount - 1)
            {
                textInLevel += "\n";
            }

        }

        _keyboardLetters.text = textInLevel;

    }



    private void GetBoundsForGrid()
    {

        Vector3[] corners = new Vector3[4];

        _keyboardArea.GetWorldCorners(corners);

        Vector3 min = corners[0];

        Vector3 max = corners[2];

        Vector3 center = (min + max) / 2f;

        Vector3 panelSize = max - min;

        int maxColumns = 0;

        foreach (int length in _rowsLengths)
        {
            if (length > maxColumns)
            {
                maxColumns = length;
            }
        }

        _keyboardBounds = new Bounds(center, panelSize);

        _keyboardButtonSize = Mathf.Min(_keyboardBounds.size.x / maxColumns, _keyboardBounds.size.y / _rowsCount);

        _keyboardPaddingY = (_keyboardBounds.size.y - _keyboardButtonSize * maxColumns) / 2f;

        Debug.Log(_keyboardButtonSize); // Size needed for counting cellsize in final version
    }


    // private void GetInputForKeyboard(Vector3 point)
    // {
    //     point.z = _keyboardBounds.center.z;

    //     if (_keyboardBounds.Contains(point))
    //     {
    //         Vector3 localPos = point - _keyboardBounds.min;

    //         localPos.y -= _keyboardPaddingY;
            
    //         int targetXvalue = Mathf.FloorToInt(localPos.x / _keyboardButtonSize);

    //         int maxValue = _rowsCount - 1;

    //         int row = maxValue - Mathf.FloorToInt(localPos.y / _keyboardButtonSize);

    //         row = Mathf.Clamp(row, 0, maxValue);

    //         int colInRow = _rowsLengths[row];

    //         float rowPaddingX = (_keyboardBounds.size.x - _keyboardButtonSize * colInRow) / 2f;

    //         int col = Mathf.Clamp((int)((localPos.x - rowPaddingX) / _keyboardButtonSize), 0, colInRow - 1);

    //         // int cell = row * _columnsCount + column;

    //         Debug.Log($"Clicked collumn: {col}, row: {row}");

    //         // if (_currentCellToEdit != cell && !_level.CheckIfCellBlocked(cell))
    //         // {
    //         //     _currentCellToEdit = cell;
    //         //                                                 //TODO 
    //         //     _pointer.transform.position = new Vector3(    
    //         //         _gamefieldGridBounds.min.x + paddingX + column * _cellSize + _cellSize / 2f,
    //         //         _gamefieldGridBounds.min.y + paddingY + (_level._size - 1 - row) * _cellSize + _cellSize / 2f,
    //         //         _pointer.transform.position.z
    //         //     );

    //         // Debug.Log($"Current cell: {_currentCellToEdit}");

    //     }
    // }
    private void GetInputForKeyboard(Vector3 point)
    {
        // ЭКСПЕРИМЕНТАЛЬНАЯ КОРРЕКЦИЯ:
        // Если при нажатии на B (во втором ряду) считывается H (в первом ряду),
        // это означает, что сетка визуально сдвинута относительно кода.
        // ПОПРОБУЙТЕ НАЧАТЬ С 0.05 * _keyboardButtonSize
        const float HORIZONTAL_OFFSET = 0f; // Установите 0.0, если уверены в настройках TMP
        
        point.z = _keyboardBounds.center.z;

        if (_keyboardBounds.Contains(point))
        {
            Vector3 localPos = point - _keyboardBounds.min;

            // 1. Расчет РЯДА (Row)
            int maxValue = _rowsCount - 1;

            // Расчет с учетом, что 0-й ряд находится сверху
            int row = maxValue - Mathf.FloorToInt(localPos.y / _keyboardButtonSize);
            row = Mathf.Clamp(row, 0, maxValue);

            if (row < 0 || row >= _rowsCount)
            {
                return; // Клик в вертикальном отступе
            }

            int colInRow = _rowsLengths[row];

            // 2. Расчет ОТСТУПА И КОЛОНКИ (Padding and Column)
            float rowPaddingX = (_keyboardBounds.size.x - _keyboardButtonSize * colInRow) / 2f;
            
            // ПРИМЕНЯЕМ ЭКСПЕРИМЕНТАЛЬНЫЙ СДВИГ К ПОЗИЦИИ КЛИКА
            localPos.x += HORIZONTAL_OFFSET;

            // Если клик пришелся в левый отступ или за правый край, выходим
            if (localPos.x < rowPaddingX || localPos.x >= (_keyboardBounds.size.x - rowPaddingX))
            {
                return;
            }

            // Вычисляем колонку, используя скорректированную позицию
            int col = Mathf.Clamp(
                Mathf.FloorToInt((localPos.x - rowPaddingX) / _keyboardButtonSize), 
                0, 
                colInRow - 1
            );
            
            // 3. Расчет ИНДЕКСА СИМВОЛА
            int charIndex = 0;
            for (int r = 0; r < row; r++)
            {
                charIndex += _rowsLengths[r];
            }
            charIndex += col;

            // 4. Получение и вывод символа
            if (charIndex >= 0 && charIndex < _keyboardAlphabet.Length)
            {
                char clickedChar = _keyboardAlphabet[charIndex];
                Debug.Log($"Clicked key: '{clickedChar}' at Row: {row}, Column: {col}, Index: {charIndex}");

                // EventBus.InputSystem.Instance.KeyboardInput(clickedChar);
            }
            else
            {
                Debug.LogWarning($"Calculated index {charIndex} out of bounds. Row: {row}, Column: {col}");
            }
        }
    }
}
