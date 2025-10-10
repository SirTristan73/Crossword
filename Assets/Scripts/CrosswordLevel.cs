using System.Collections.Generic;
using EventBus;
using Unity.Collections;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;


[CreateAssetMenu(fileName = "CrosswordLevel", menuName = "Crossword/Level")]
public class CrosswordLevel : ScriptableObject
{
    public int _size { get; private set; }

    public LanguageState _levelsLanguage;

    public List<bool> _isCellBlocked { get; private set; }

    public List<WordData> _words = new();
    public IReadOnlyList<WordData> Words => _words;


    public void InitLevelSize(int size)
    {
        _size = size;
        _isCellBlocked = new List<bool>(new bool[_size * _size]);
    }


    public bool CheckIfCellBlocked(int i)
    {
        if (_isCellBlocked == null || i < 0 || i > _isCellBlocked.Count)
        {
            return false;
        }

        return _isCellBlocked[i];
    }

    public void ChangeCellBlocking(int i)
    {
        _words.RemoveAll(word => WordContainsCell(word, i));

        _isCellBlocked[i] = !_isCellBlocked[i];
    }

    public void AddWord(WordData word)
    {
        _words.Add(word);
    // --- АВТОБЛОКИРОВКА ПРИМЫКАЮЩИХ ЯЧЕЕК ---
    
    int levelSize = _size;
    int startIndex = word._startIndex;
    int length = word._wordHere.Length;
    int totalCells = levelSize * levelSize;

    int startRow = startIndex / levelSize;
    int startCol = startIndex % levelSize;

    // --- Определение beforeIndex ---
    int beforeIndex = -1; // Изначально невалидный
    if (word._isHorizontal)
    {
        // Только если слово не начинается в первом столбце
        if (startCol > 0)
        {
            beforeIndex = startIndex - 1;
        }
    }
    else // Vertical
    {
        // Только если слово не начинается в первой строке
        if (startRow > 0)
        {
            beforeIndex = startIndex - levelSize;
        }
    }

    // --- Определение afterIndex ---
    int afterIndex = -1; // Изначально невалидный
    if (word._isHorizontal)
    {
        // Только если слово не заканчивается в последнем столбце
        if (startCol + length < levelSize)
        {
            afterIndex = startIndex + length;
        }
    }
    else // Vertical
    {
        // Только если слово не заканчивается в последней строке
        if (startRow + length < levelSize)
        {
            afterIndex = startIndex + length * levelSize;
        }
    }

    // --- Применение блокировки ---

    // Блокируем 'before'
    if (beforeIndex >= 0 && beforeIndex < totalCells && beforeIndex != -1)
    {
        // Не трогаем ячейку, если там уже есть слово
        if (WordCheck.WordAtCell(this, beforeIndex) == null) 
        {
            _isCellBlocked[beforeIndex] = true;
        }
    }

    // Блокируем 'after'
    if (afterIndex >= 0 && afterIndex < totalCells && afterIndex != -1)
    {
        if (WordCheck.WordAtCell(this, afterIndex) == null)
        {
            _isCellBlocked[afterIndex] = true;
        }
    }
    }


    public void GetWordDataAtCell(int index, out WordData HorizontalWord, out WordData VerticalWord)
    {
        HorizontalWord = null;
        VerticalWord = null;

        if (CheckIfCellBlocked(index))
        {
            return;
        }

        foreach (var word in _words)
        {
            if (WordContainsCell(word, index))
            {
                if (word._isHorizontal) HorizontalWord = word;
                else VerticalWord = word;
            }
        }
    }


    public bool WordContainsCell(WordData word, int cellIndex)
    {
        // if (CheckIfCellBlocked(cellIndex))
        // {
        //     return false;
        // }

        int lastIndex = word.EndIndex(_size);

        if (word._isHorizontal)
        {
            int startRow = word._startIndex / _size;
            int row = cellIndex / _size;

            return row == startRow &&
                            cellIndex >= word._startIndex &&
                            cellIndex <= lastIndex;
        }
        else
        {
            int startCol = word._startIndex % _size;
            int col = cellIndex % _size;

            return col == startCol &&
                            cellIndex >= word._startIndex &&
                            cellIndex <= lastIndex;
        }
    }
    

}

    public enum LevelSize
    {
        _5X5 = 5,
        _10X10 = 10,
        _17X17 = 17,
    }