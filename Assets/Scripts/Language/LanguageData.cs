using System.Collections.Generic;
using UnityEngine;

public abstract class LanguageData : ScriptableObject
{
    public abstract Dictionary<string, string> MenuTexts { get; }
    public abstract Dictionary<string, (string characterName, string text)> DialogueTexts { get; }

}
