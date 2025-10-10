using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEngine;

public class GridEditingState : EditorState
{
    public GridEditingState(CrosswordEditor editor) : base(editor) { }

    private Vector2 _scroll;
    private Vector2 _wordScroll;

    private int _selectedWordInd = -1;
    private int _selectedCellInd = -1;

    private bool _isSetToHorizontal = true;

    private CrosswordLevel _currentEditingLevel;

    private GUIStyle _emptyStyle;
    private GUIStyle _cellStyle;

    private bool _saveRequired = false;





    public override void OnGUI()
    {

        InitStyles();

        if (_editor._currentLevel != null)
        {
            _currentEditingLevel = _editor._currentLevel;
        }
        else
        {
            _editor.ChangeEditorState(new NoLevelState(_editor));
            return;
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Change Parameters"))
        {   
            if (_saveRequired)
            {
                SaveLevelsData();
            }
            
            _editor.ChangeEditorState(new SetupLevelState(_editor));
            return;
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Choose Other Level"))
        {
            if (_saveRequired)
            {
                SaveLevelsData();
            }
            
            _editor._currentLevel = null;

            _editor.ChangeEditorState(new NoLevelState(_editor));

            return;
        }

        EditorGUILayout.Space();

        _scroll = EditorGUILayout.BeginScrollView(_scroll);

        DrawGrid();

        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();

        if (_selectedCellInd >= 0)
        {
            DrawWordSelection();
        }

        SaveLevelsData();
    }


    private void DrawGrid()
    {
        int buttonsSize = 40;

        int size = _currentEditingLevel._size;

        HorizontalSwitch();

        for (int y = 0; y < size; y++)
        {
            GUILayout.BeginHorizontal();

            for (int x = 0; x < size; x++)
            {
                int index = y * size + x;

                bool isBlocked = _currentEditingLevel.CheckIfCellBlocked(index);

                char? letter = GetLetterAtCell(x, y);

                var style = isBlocked ? _emptyStyle : _cellStyle;

                string text = isBlocked ? "" : (letter.HasValue ? letter.Value.ToString() : "");

                if (GUILayout.Button(text, style, GUILayout.Width(buttonsSize), GUILayout.Height(buttonsSize)))
                {
                    HandleCellClick(index, isBlocked);
                }

            }

            GUILayout.EndHorizontal();
        }

    }
    

    private void HorizontalSwitch()
    {
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Horizontal")) _isSetToHorizontal = true;

        if (GUILayout.Button("Vertical")) _isSetToHorizontal = false;

        GUILayout.Label(_isSetToHorizontal ? "Mode: Horizontal" : "Mode: Vertical");

        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
    }


    private void HandleCellClick(int index, bool isBlocked)
    {
        if (Event.current.button == 1)
        {
            _currentEditingLevel.ChangeCellBlocking(index);

            _saveRequired = true;

            EditorApplication.delayCall += _editor.Repaint;

            return;
        }
        else
        {
            _selectedCellInd = index;
        }
        _editor.Repaint();

    }


    private void DrawWordSelection()
    {
        var dialogueData = _editor.GetDialogueDictionaryForLanguage();

        List<string> displayWords = new();

        List<string> rawWords = new();

        foreach (var pair in dialogueData)
        {
            string word = pair.Value.word;
 
            if (!string.IsNullOrEmpty(word))
            {
                displayWords.Add($"{word.ToUpper()}  Word length: {word.Length}");

                rawWords.Add(word);
            }
        }

        EditorGUILayout.Space();

        GUILayout.Label("Select Word", EditorStyles.boldLabel);

        _wordScroll = EditorGUILayout.BeginScrollView(_wordScroll, GUILayout.Height(250));

        int newSelection = GUILayout.SelectionGrid(
            _selectedWordInd,
            displayWords.ToArray(),
            1,
            EditorStyles.miniButton,
            GUILayout.ExpandWidth(true)
        );

        EditorGUILayout.EndScrollView();

        if (newSelection != _selectedWordInd && newSelection >= 0)
        {
            string selectedWord = rawWords[newSelection];

            bool isPlacingPossible = WordCheck.IsWordsConflicted(_currentEditingLevel, selectedWord, _selectedCellInd, _isSetToHorizontal);

            if (isPlacingPossible)
            {
                _currentEditingLevel.AddWord(new WordData
                {
                    _startIndex = _selectedCellInd,
                    _wordHere = selectedWord,
                    _isHorizontal = _isSetToHorizontal
                });

                _saveRequired = true;

            }
            else
            {
                Debug.LogWarning($"Word {selectedWord} can't be placed here");
            }

            _selectedWordInd = -1;
            _selectedCellInd = -1;

            // _saveRequired = true;

            // EditorApplication.delayCall += _editor.Repaint;
        }

        if (GUILayout.Button("Cancel"))
        {
            _selectedWordInd = -1;
            _selectedCellInd = -1;
        }
    }


    private char? GetLetterAtCell(int x, int y)
    {   
        int index = y * _currentEditingLevel._size + x;
        int levelSize = _currentEditingLevel._size;

        var word = WordCheck.WordAtCell(_currentEditingLevel, index);

        if (word == null)
        {
            return null;
        }

        int offset = word._isHorizontal
                    ? index - word._startIndex
                    : (index - word._startIndex) / levelSize;

        if (offset < 0 || offset >= word._wordHere.Length)
        {
            Debug.LogError($"[BUG DETECTED] IndexOutOfRange: Offset {offset} is invalid (Length: {word._wordHere.Length}) for word '{word._wordHere}' at index {index}. WordContainsCell is lying.");
            return null;
        }

        return word._wordHere[offset];
        
    }


    private void SaveLevelsData()
    {   
        if(!_saveRequired)
        {
            return;
        }
        else
        {
            Debug.Log("save");
            EditorApplication.delayCall += () =>
            {
                if (!_saveRequired)
                {
                    return;
                }
                else
                {
                    EditorUtility.SetDirty(_currentEditingLevel);

                    AssetDatabase.SaveAssets();

                    _saveRequired = false;

                    _editor.Repaint();
                }
            };
            _saveRequired = false;
        }
    }
 

    private void InitStyles()
    {
        if (_cellStyle == null)
        {
            _cellStyle = new(GUI.skin.button);
            _emptyStyle = new(GUI.skin.button);
            _emptyStyle.normal.background = Texture2D.blackTexture;
        }
        else
        {
            return;
        }
    }
}
