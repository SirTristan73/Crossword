using UnityEngine;

public abstract class CoreUIState
{
    public abstract UIState StateType { get; }

    protected UIPanel _panel;

    public void SetPanel(UIPanel p) => _panel = p;



    public virtual void Enter()
    {
        if (_panel != null)
        {
            _panel?.Show();
        }
    }
    

    public virtual void Exit()
    {
        if (_panel != null)
        {
            _panel?.Hide();
        }
    }
}
