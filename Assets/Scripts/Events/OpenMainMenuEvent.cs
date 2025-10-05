using UnityEngine;


namespace EventBus
{
    public class OpenMainMenuEvent : EventType
    {
        public OpenMainMenuEvent(UIState menuState, UIPanel menuPanel, UIState selectState, UIPanel selectPanel)
        {
            UIManager.Instance.AssignPanelToState(menuState, menuPanel);

            UIManager.Instance.AssignPanelToState(selectState, selectPanel);

            UIManager.Instance.ChangeSceneState(menuState);
        }
    }
}