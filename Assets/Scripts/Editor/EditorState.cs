using UnityEngine;

public abstract class EditorState
{
    protected CrosswordEditor _editor;

    public EditorState(CrosswordEditor editor)
    {
        _editor = editor;
    }

    public virtual void Enter() {}
    public virtual void Exit() {}
    public virtual void OnGUI() {}
    public virtual void Update() {}
}
