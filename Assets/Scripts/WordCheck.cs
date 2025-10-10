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

}
