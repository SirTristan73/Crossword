using UnityEngine;

public class EditorCellState : EditorState
{
    public EditorCellState(CrosswordEditor editor) : base(editor) { }

    public override void Enter()
    {
        Debug.Log("Entering EditCell state");
    }


    public override void Exit()
    {
        Debug.Log("Exiting EditCell state");
    }
    
    public override void OnGUI()
    {
        GUILayout.Label("Edit Cell Mode");
    }

    public override void Update()
    {
        // обработка ввода, кликов и т.д.
    }

}
