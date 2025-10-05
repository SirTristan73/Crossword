using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


namespace EventBus
{
    public class MainMenuPanel : MonoBehaviour
    {
        [SerializeField] private UIPanel _mainMenuPanel;
        [SerializeField] private UIPanel _levelSelectPanel;
        [SerializeField] private UIState _levelSelectState;
        [SerializeField] private UIState _menuState;


        private void OnEnable()
        {
            EventBus.Trigger(new OpenMainMenuEvent(_menuState, _mainMenuPanel,
                                                 _levelSelectState, _levelSelectPanel));
        }
    }

}
