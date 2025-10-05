using UnityEngine;


public class MainMenuState : CoreUIState
{

    public override UIState StateType => UIState.MainMenu;

    public MainMenuState() { }


    public override void Enter()
    {
        base._panel.Show();
    }

    public override void Exit()
    {
        base._panel.Hide();
    }
}
