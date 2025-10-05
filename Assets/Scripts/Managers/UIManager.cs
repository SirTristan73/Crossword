using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : PersistentSingleton<UIManager>
{
    public Dictionary<UIState, CoreUIState> _UIstates = new();

    private readonly Stack<CoreUIState> _overlayStack = new();

    public CoreUIState _currentSceneState { get; private set; }



    [Header("Persistent Panels")]

    public UIPanel _languageChangePanel;

    public UIPanel _settingsPanel;



    public void Init()
    {
        _UIstates[UIState.LanguageChoice] = new LanguageChangeState(); // Пока в ините все стейты, потом можно перенести
        _UIstates[UIState.MainMenu] = new MainMenuState();
        _UIstates[UIState.LevelSelection] = new LevelSelectionState();
        
        AssignPanelToState(UIState.LanguageChoice, _languageChangePanel);
    }


    public void AssignPanelToState(UIState type, UIPanel panel)
    {
        if (_UIstates.TryGetValue(type, out var state))
        {
            state.SetPanel(panel);
            Debug.Log($"State {state} registered to {panel} ");
        }
        else
        {
            Debug.LogWarning($"State {type} not found");
        }
    }


    public void ChangeSceneState(UIState type)
    {
        _currentSceneState?.Exit();

        if (_UIstates.TryGetValue(type, out var newState))
        {
            _currentSceneState = newState;
            _currentSceneState.Enter();
        }
        else
        {
            Debug.LogError($"UIState {type} not registered");
        }
    }


    public void PushOverlay(UIState type)
    {
        if (_UIstates.TryGetValue(type, out var overlay))
        {
            if (_currentSceneState != null)
            {
                _currentSceneState.Exit();
            }

            overlay.Enter();
            _overlayStack.Push(overlay);
        }
    }


    public void PopOverlay()
    {
        if (_overlayStack.Count > 0)
        {
            var overlay = _overlayStack.Pop();
            overlay.Exit();

            if (_currentSceneState != null)
            {
                _currentSceneState.Enter();
            }

        }

    }
}

public enum UIState
{
    MainMenu,
    LevelSelection,
    Gamestage,
    LanguageChoice,
    SettingsUI,
}
