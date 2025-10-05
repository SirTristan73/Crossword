using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnglishData")]
public class EnglishData : LanguageData
{
    private Dictionary<string, string> _menuTexts = new Dictionary<string, string>()
    {
        {"title_mainMenu", "Crosswords 2.0"},
        {"confirm_button", "Confirm"},
        {"back_button", "Back"},
        {"start_button", "Start"},
        {"select_language", "Language"}
    };


    public override Dictionary<string, string> MenuTexts => _menuTexts;


    private Dictionary<string, (string characterName, string text)> _dialogueTexts = new Dictionary<string, (string characterName, string text)>()
    {

    };


    public override Dictionary<string, (string characterName, string text)> DialogueTexts => _dialogueTexts;
}
