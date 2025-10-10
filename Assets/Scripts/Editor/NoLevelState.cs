using System;
using System.Collections.Generic;
using EventBus;
using UnityEditor;
using UnityEngine;


public class NoLevelState : EditorState
{
    public NoLevelState(CrosswordEditor editor) : base(editor) { }

    public override void OnGUI()
    {
        GUILayout.Label("No Level Loaded", EditorStyles.boldLabel);
        GUILayout.Label("Assign Existing Level", EditorStyles.boldLabel);

        _editor._currentLevel = (CrosswordLevel)EditorGUILayout.ObjectField(
                                                        "LevelAsset", _editor._currentLevel,
                                                        typeof(CrosswordLevel),
                                                        false);

        EditorGUILayout.Space();

        if (GUILayout.Button("Create New Level"))
        {
            _editor.CreateNewLevelAsset();
        }

        EditorGUILayout.Space();


        if (_editor._currentLevel != null &&
            _editor._currentState is not GridEditingState &&
            _editor._currentState is not SetupLevelState)
        {
            LoadLevelsConditions();
            _editor.ChangeEditorState(new SetupLevelState(_editor));
        }

    }


    private void LoadLevelsConditions()
    {
        _editor._selectedLanguage = _editor._currentLevel._levelsLanguage;
        _editor._levelSize = (LevelSize)_editor._currentLevel._size;

        if (_editor._currentLevel.Words != null &&
             _editor._currentLevel.Words.Count > 0)
        {
            _editor.ChangeEditorState(new GridEditingState(_editor));
        }
        else
        {
            _editor.ChangeEditorState(new SetupLevelState(_editor));
        }
    }   
    

}
