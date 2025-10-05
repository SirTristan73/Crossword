using UnityEngine;
using System.Collections.Generic;



[CreateAssetMenu(fileName = "RussianData")]
public class RussianData : LanguageData
{
    private Dictionary<string, string> _menuTexts = new Dictionary<string, string>()
    {
        {"title_mainMenu", "Crosswords 2.0"},
        {"confirm_button", "Подтвердить"},
        {"back_button", "Назад"},
        {"start_button", "Start"},
        {"select_language", "Язык"}
    };


    public override Dictionary<string, string> MenuTexts => _menuTexts;


    private Dictionary<string, (string characterName, string text)> _dialogueTexts = new Dictionary<string, (string characterName, string text)>()
    {

    };


    public override Dictionary<string, (string characterName, string text)> DialogueTexts => _dialogueTexts;
}


