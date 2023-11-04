using UnityEngine;

namespace Game.Services
{
    public class InputManager
    {
        public bool PlayerInputBlocked { get; set; } = false;
        
        public Vector2 GetMovementInput()
        {
            if (PlayerInputBlocked) return Vector2.zero;
            
            var movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            return movementInput;
        }
    
        public bool GetInteractInput()
        {
            if (PlayerInputBlocked) return false;
            
            return Input.GetKeyDown(KeyCode.E);
        }
    
        public bool GetDevilZone()
        {
            if (PlayerInputBlocked) return false;

            return Input.GetKey(KeyCode.Space);
        }
        
        public bool GetDialogueInput() => Input.anyKeyDown;
        
        public bool GetBackInput()
        {
            return Input.GetKeyDown(KeyCode.Escape);
        }
    }
}
