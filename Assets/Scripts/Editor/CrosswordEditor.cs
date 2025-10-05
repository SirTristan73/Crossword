using EventBus;
using UnityEditor;
using UnityEngine;



public class CrosswordEditor : EditorWindow
{
    LanguageState _selectedLanguage = LanguageState.English;
    


    [MenuItem("Tools/My Editor Window")]
    public static void OpenWindow()
    {
        GetWindow<CrosswordEditor>("CrosswordEditorTool");
    }


    private void OnGUI()
    {
        GUILayout.Label("Custom Tool Example", EditorStyles.boldLabel);

        _selectedLanguage = (LanguageState)EditorGUILayout.EnumPopup("Language", _selectedLanguage);

        if (GUILayout.Button("Click Me"))
        {
            Debug.Log("You typed: " + _selectedLanguage);
        }
    }
}
