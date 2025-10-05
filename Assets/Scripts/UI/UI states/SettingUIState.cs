using UnityEngine;

public class SettingUIState : CoreUIState
{
    public override UIState StateType => UIState.SettingsUI;

    public SettingUIState() { }


    public override void Enter()
    {
        base._panel.Show();
    }
    

    public override void Exit()
    {
        base._panel.Hide();
    }
}
