using UnityEngine;

namespace EventBus
{
    public class LanguageChosenEvent : EventType
    {
        public LanguageChosenEvent(LanguageState state)
        {
            TextContainer.Instance.SetTextLanguage(state);
            
            SaveFile data = SaveManager.Instance.LoadGame();

            data.GameSettings._savedLanguage = state;

            SaveManager.Instance.SaveGame(data);
        }
    }
}