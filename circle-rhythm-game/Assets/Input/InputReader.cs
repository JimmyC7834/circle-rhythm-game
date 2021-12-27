using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace Game
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
    public class InputReader : ScriptableObject, GameInput.IPlayerInputActions//, GameInput.IMenusActions
    {
        // Pointer
        public event UnityAction<Vector2> moveEvent = delegate { };
        public event UnityAction confirmEvent = delegate { };
        public event UnityAction cancelEvent = delegate { };

        // Menus
        // public event UnityAction<Vector2> menuMoveSelectionEvent = delegate { };
        // public event UnityAction menuConfirmEvent = delegate { };
        // public event UnityAction menuCancelEvent = delegate { };

        // !!! Remember to edit Input Reader functions upon updating the input map !!!
        private GameInput gameInput;

        private void OnEnable() {
            if (gameInput == null) {
                gameInput = new GameInput();

                gameInput.PlayerInput.SetCallbacks(this);
                // gameInput.Menus.SetCallbacks(this);
            }

            EnablePlayerInput();
        }

        private void OnDisable() {

        }

        // -----PLAYERINPUT-----
        public void OnNormalHit(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                confirmEvent.Invoke();
        }

        public void OnSpaceHit(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                confirmEvent.Invoke();
        }

        // -----MENUS-----
        // public void OnMenuMoveSelection(InputAction.CallbackContext context)
        // {
        //     menuMoveSelectionEvent.Invoke(context.ReadValue<Vector2>());
        // }

        // public void OnMenuConfirm(InputAction.CallbackContext context)
        // {
        //     if (context.phase == InputActionPhase.Performed)
        //         menuConfirmEvent.Invoke();
        // }

        // public void OnMenuCancel(InputAction.CallbackContext context)
        // {
        //     if (context.phase == InputActionPhase.Performed)
        //         menuCancelEvent.Invoke();
        // }

        // Input Reader
        public void EnablePlayerInput() {
            // gameInput.Menus.Disable();

            gameInput.PlayerInput.Enable();
        }

        // public void EnableMenusInput() {
        //     gameInput.PlayerInput.Disable();
        //
        //     // gameInput.Menus.Enable();
        // }

        public void DisableAllInput() {
            gameInput.PlayerInput.Disable();
            // gameInput.Menus.Disable();
        }
    }
}
