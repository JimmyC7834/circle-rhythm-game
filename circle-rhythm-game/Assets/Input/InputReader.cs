using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace Game
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
    public class InputReader : ScriptableObject, GameInput.IGamePlayActions//, GameInput.IMenusActions
    {
        // Pointer
        public event UnityAction dotHitEvent = delegate { };
        public event UnityAction dotReleaseEvent = delegate { };
        public event UnityAction circleHitEvent = delegate { };

        // Menus
        // public event UnityAction<Vector2> menuMoveSelectionEvent = delegate { };
        // public event UnityAction menuConfirmEvent = delegate { };
        // public event UnityAction menuCancelEvent = delegate { };

        // !!! Remember to edit Input Reader functions upon updating the input map !!!
        private GameInput gameInput;

        private void OnEnable() {
            if (gameInput == null) {
                gameInput = new GameInput();

                gameInput.GamePlay.SetCallbacks(this);
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
            {
                // Debug.Log($"{context}");
                if ((int)context.ReadValue<float>() == 1)
                {
                    dotHitEvent.Invoke();
                    // Debug.Log("dotHitEvent");
                    return;
                }
                
                dotReleaseEvent.Invoke();
                // Debug.Log("dotReleaseEvent");
            }
        }

        public void OnCircleHit(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                circleHitEvent.Invoke();
                // Debug.Log("OnCircleHit");
            }
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

            gameInput.GamePlay.Enable();
        }

        // public void EnableMenusInput() {
        //     gameInput.PlayerInput.Disable();
        //
        //     // gameInput.Menus.Enable();
        // }

        public void DisableAllInput() {
            gameInput.GamePlay.Disable();
            // gameInput.Menus.Disable();
        }
    }
}
