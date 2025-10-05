 using UnityEngine;

public class LanguageChangeState : CoreUIState
{
    public override UIState StateType => UIState.LanguageChoice;

    public LanguageChangeState() { }


    public override void Enter()
    {
        base._panel.Show();
    }

    public override void Exit()
    {
        base._panel.Hide();
    }
}
