using UnityEngine;

public class InputManager
{
    public Vector2 GetMovementInput()
    {
        var movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        return movementInput;
    }
    
    public bool GetInteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
    
    public bool GetWatchInput()
    {
        return Input.GetKey(KeyCode.Space);
    }
}
