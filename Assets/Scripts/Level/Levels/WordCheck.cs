using System.Drawing;
using UnityEngine;

public static class WordCheck 
{
    public static bool IsWordsConflicted(CrosswordLevel level, string word, int startIndex, bool isHorizontal)
    {
        int length = word.Length;
        int levelSize = level._size;

        int row = startIndex / levelSize;
        int col = startIndex % levelSize;

        int beforeIndex = isHorizontal ? startIndex - 1 : startIndex - levelSize;
        int afterIndex = isHorizontal ? startIndex + length : startIndex + length * levelSize;

        if (isHorizontal && col + length > levelSize)
        {
            return false;
        }

        if (!isHorizontal && row + length > levelSize)
        {
            return false;
        }

        if (beforeIndex >= 0 && WordAtCell(level, beforeIndex) != null)
        {
            return false;
        }

        if (afterIndex < levelSize * levelSize && WordAtCell(level, afterIndex) != null)
        {
            return false;
        }

        for (int i = 0; i < length; i++)
        {
            int index = isHorizontal ? startIndex + i : startIndex + i * levelSize;

            if (level.CheckIfCellBlocked(index))
            {
                return false;
            }

            foreach (var existing in level.Words)
            {
                if (level.WordContainsCell(existing, index))
                {
                    int offset = existing._isHorizontal
                                ? index - existing._startIndex
                                : (index - existing._startIndex) / levelSize;

                    if (offset < 0 || offset >= existing._wordHere.Length)
                    {
                        continue;
                    }

                    if (existing._wordHere[offset] != word[i])
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }


    public static WordData WordAtCell(CrosswordLevel level, int index)
    {
        foreach (var word in level.Words)
        {
            if (level.WordContainsCell(word, index))
            {
                return word;
            }
        }

        return null;
    }



    public static void ChangeCellBlocking(CrosswordLevel level, int i)
    {
        level._words.RemoveAll(word => level.WordContainsCell(word, i));

        level._isCellBlocked[i] = !level._isCellBlocked[i];
    }
    

    public static void AddWord(CrosswordLevel level, WordData word)
    {
        level._words.Add(word);
    
        int levelSize = level._size;
        int startIndex = word._startIndex;
        int length = word._wordHere.Length;
        int totalCells = levelSize * levelSize;

        int startRow = startIndex / levelSize;
        int startCol = startIndex % levelSize;

        int beforeIndex = -1; 
        if (word._isHorizontal)
        {
            if (startCol > 0)
            {
                beforeIndex = startIndex - 1;
            }
        }
        else 
        {
            if (startRow > 0)
            {
                beforeIndex = startIndex - levelSize;
            }
        }

        int afterIndex = -1; 
        if (word._isHorizontal)
        {
            if (startCol + length < levelSize)
            {
                afterIndex = startIndex + length;
            }
        }
        else 
        {
            if (startRow + length < levelSize)
            {
                afterIndex = startIndex + length * levelSize;
            }
        }

        if (beforeIndex >= 0 && beforeIndex < totalCells && beforeIndex != -1)
        {
            if (WordAtCell(level, beforeIndex) == null) 
            {
                level._isCellBlocked[beforeIndex] = true;
            }
        }

        if (afterIndex >= 0 && afterIndex < totalCells && afterIndex != -1)
        {
            if (WordAtCell(level, afterIndex) == null)
            {
                level._isCellBlocked[afterIndex] = true;
            }
        }
    }

}
