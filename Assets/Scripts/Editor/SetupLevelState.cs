using UnityEngine;
using EventBus;
using UnityEditor;

public class SetupLevelState : EditorState
{
    public SetupLevelState(CrosswordEditor editor) : base(editor) { }

    public override void OnGUI()
    {
        var level = _editor._currentLevel;

        if (level == null)
        {
            _editor.ChangeEditorState(new NoLevelState(_editor));
            return;
        }

        _editor._selectedLanguage = (LanguageState)EditorGUILayout.EnumPopup("Language", _editor._selectedLanguage);

        _editor._levelSize = (LevelSize)EditorGUILayout.EnumPopup("Size", _editor._levelSize);

        if (level._isCellBlocked != null && level._isCellBlocked.Count == (int)_editor._levelSize * (int)_editor._levelSize)
        {
            if (GUILayout.Button("Resasign parameters"))
            {
                SetLevelParameters(level);
            }
        }
        else
        {
            if (GUILayout.Button("Set parameters"))
            {
                SetLevelParameters(level);
            }
        }

        if (GUILayout.Button("Back to editing"))
        {
            _editor.ChangeEditorState(new GridEditingState(_editor));
        }

        if (GUILayout.Button("Chose other level"))
        {
            _editor._currentLevel = null;
            
            _editor.ChangeEditorState(new NoLevelState(_editor));
        }        
    }


    private void SetLevelParameters(CrosswordLevel level)
    {
        int size = (int)_editor._levelSize;

        level.InitLevelSize(size);

        level._levelsLanguage = _editor._selectedLanguage;

        EditorUtility.SetDirty(level);
        AssetDatabase.SaveAssets();

        _editor.ChangeEditorState(new GridEditingState(_editor));
    }
}
