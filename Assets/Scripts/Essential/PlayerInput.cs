using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static Action<Vector3> _clickCallback;



    public void OnClickCallback(InputAction.CallbackContext ctx)
    {
        if (ctx.phase != InputActionPhase.Performed)
        {
            return;
        }
        
        Vector2 screenPos = ctx.ReadValue<Vector2>();

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(
                                                    screenPos.x,
                                                    screenPos.y,
                                                    -Camera.main.transform.position.z));

          _clickCallback?.Invoke(worldPoint);
        
    }

}
