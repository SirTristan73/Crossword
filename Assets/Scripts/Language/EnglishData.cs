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


    private Dictionary<string, (string word, string hint)> _dialogueTexts = new Dictionary<string, (string word, string hint)>()
    {
        {"sasd", ("asda", "sada")},
        {"sagadadsd", ("asdgagaa", "sadaagaga")},
    };


    public override Dictionary<string, (string word, string hint)> DialogueTexts => _dialogueTexts;
}
