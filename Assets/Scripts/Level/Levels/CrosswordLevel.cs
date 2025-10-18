using System.Collections.Generic;
using EventBus;
using Unity.Collections;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;


[CreateAssetMenu(fileName = "CrosswordLevel", menuName = "Crossword/Level")]

public class CrosswordLevel : ScriptableObject
{
    [SerializeField] public int _size;

    public LanguageState _levelsLanguage;

    [SerializeField] public List<bool> _isCellBlocked;

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