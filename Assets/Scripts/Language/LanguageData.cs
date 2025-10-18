using System.Collections.Generic;
using UnityEngine;

public abstract class LanguageData : ScriptableObject
{
    public abstract Dictionary<string, string> MenuTexts { get; }
    public abstract Dictionary<string, (string word, string hint)> DialogueTexts { get; }
    public abstract char[] KeyboardAlphabet { get; }
    public abstract int[] KeyboardRowLengths { get; }

}
