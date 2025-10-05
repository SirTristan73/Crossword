using UnityEngine;



namespace EventBus
{
    public class ButtonOnCLickEvent : EventType
    {
        public ButtonOnCLickEvent(string data)
        {
            switch (data)
            {
                case "back_button":
                    UIManager.Instance.PopOverlay();
                    break;

                case "confirm_button":
                    UIManager.Instance.PopOverlay();
                    break;
                    
                case "select_language":
                    UIManager.Instance.PushOverlay(UIState.LanguageChoice);
                    break;

            }
        }
    }
}