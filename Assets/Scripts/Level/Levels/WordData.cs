using UnityEngine;
using System;


[Serializable]
public class WordData 
{
    public int _startIndex;
    public bool _isHorizontal;
    public string _wordHere;
    public string _hint;


    public int EndIndex(int gridSize)
    {
        return _isHorizontal ? _startIndex + _wordHere.Length - 1
                                : _startIndex + (_wordHere.Length - 1) * gridSize;

    }

}
