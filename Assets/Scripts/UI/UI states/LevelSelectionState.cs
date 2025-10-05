using UnityEngine;

public class LevelSelectionState : CoreUIState
{
    public override UIState StateType => UIState.LevelSelection;

    public LevelSelectionState() { }


    public override void Enter()
    {
        base._panel.Show();
    }

    public override void Exit()
    {
        base._panel.Hide();
    }
}
