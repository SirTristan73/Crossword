using UnityEngine;

public class ViewState : EditorState
{
    public ViewState(CrosswordEditor editor) : base(editor) { }

    public override void Enter() => Debug.Log("Entering View state");
    public override void Exit() => Debug.Log("Exiting View state");

    public override void OnGUI()
    {
        GUILayout.Label("View Mode");
    }

    public override void Update() { }
}
