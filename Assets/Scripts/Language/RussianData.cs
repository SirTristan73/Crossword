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


    private Dictionary<string, (string word, string hint)> _dialogueTexts = new Dictionary<string, (string word, string hint)>()
    {

    };


    public override Dictionary<string, (string word, string hint)> DialogueTexts => _dialogueTexts;


    private char[] _alphabet = new char[]
    {
        'а', 'б', 'в', 'г'
    };


    public override char[] KeyboardAlphabet => _alphabet;



    private int[] _keyboardRows = new int[]
    {
        10, 9, 7
    };

    public override int[] KeyboardRowLengths => _keyboardRows;
}


