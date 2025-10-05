using UnityEngine;


namespace EventBus
{
    public class GameLaunchEvent : EventType
    {
        public GameLaunchEvent()
        {
            SaveFile data = SaveManager.Instance.LoadGame();

            UIManager.Instance.Init();

            if (!SaveManager.Instance.HasSaveGame())
            {
                Debug.Log("New launch");

                data.GameSettings._savedLanguage = LanguageState.English;

                SaveManager.Instance.SaveGame(data);

                UIManager.Instance.PushOverlay(UIState.LanguageChoice);

            }
            else
            {
                LoadingManager.Instance.LoadScene("MainMenu");
                
                TextContainer.Instance.SetTextLanguage(data.GameSettings._savedLanguage);

            }


        }
    }
}