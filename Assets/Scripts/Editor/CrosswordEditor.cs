using System;
using System.Collections.Generic;
using EventBus;
using UnityEditor;
using UnityEngine;




public class CrosswordEditor : EditorWindow
{

    public EditorState _currentState;
    EditorState _nextState;

    ////////////////////////////////

    public LanguageState _selectedLanguage = LanguageState.English;

    public LevelSize _levelSize = LevelSize._5X5;

    public CrosswordLevel _currentLevel;


    private static readonly Dictionary<LanguageState, Type> LanguageMap = new()
    {
        {LanguageState.English, typeof(EnglishData) },
        {LanguageState.Russian, typeof(RussianData) }
    };


    [MenuItem("Tools/Crossword Creator")]
    public static void OpenWindow()
    {
        GetWindow<CrosswordEditor>("CrosswordEditorTool");
    }


    private void OnEnable()
    {
        ChangeEditorState(new NoLevelState(this));
    }


    public void ChangeEditorState(EditorState newState)
    {
        _nextState = newState;
    }


    private void Update()
    {
        EditorStateDelay();
        
        _currentState?.Update();
    }


    private void OnGUI()
    {
        _currentState?.OnGUI();
    }


    private void EditorStateDelay()
    {
        if (_nextState != null)
        {
            _currentState?.Exit();
            Debug.Log(_currentState);
            Debug.Log(_nextState);
            _currentState = _nextState;
            _nextState = null;
            _currentState?.Enter();
        }
    }


    public void CreateNewLevelAsset()
    {
        string path = EditorUtility.SaveFilePanelInProject(
            "Save Crossword Level",
            "New CrosswordLevel",
            "asset",
            "Save the level as a ScriptableObject asset");

        if (string.IsNullOrEmpty(path)) return;

        _currentLevel = CreateInstance<CrosswordLevel>();
        AssetDatabase.CreateAsset(_currentLevel, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = _currentLevel;

        ChangeEditorState(new SetupLevelState(this));
    }



    public Dictionary<string, (string word, string hint)> GetDialogueDictionaryForLanguage()
    {
        if (!LanguageMap.TryGetValue(_selectedLanguage, out Type targetType))
        {
            Debug.LogError($"[CrosswordEditor] Language State {_selectedLanguage} is not mapped.");
            return new Dictionary<string, (string, string)>();
        }

        string[] guids = AssetDatabase.FindAssets($"t:{targetType.Name}");

        if (guids.Length == 0)
        {
            Debug.LogError($"[CrosswordEditor] No asset of type {targetType.Name} found. Create one!");
            return new Dictionary<string, (string, string)>();
        }

        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        var languageDataAsset = AssetDatabase.LoadAssetAtPath(path, targetType) as LanguageData;

        if (languageDataAsset == null)
        {
            Debug.LogError($"[CrosswordEditor] Failed to load LanguageData asset at {path}.");
            return new Dictionary<string, (string, string)>();
        }

        return languageDataAsset.DialogueTexts;
    }




}
