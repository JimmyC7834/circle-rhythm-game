//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/GameInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @GameInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInput"",
    ""maps"": [
        {
            ""name"": ""PlayerInput"",
            ""id"": ""1f27b624-ff3b-4197-8864-4732d6571e7d"",
            ""actions"": [
                {
                    ""name"": ""NormalHit"",
                    ""type"": ""Button"",
                    ""id"": ""7df4f59f-75d5-4deb-bf8a-9d37f2779583"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SpaceHit"",
                    ""type"": ""Button"",
                    ""id"": ""903e7dc4-b5ce-46b3-b868-0fdf576946b2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""68467db2-8c77-410b-a8c7-165dd7787701"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NormalHit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a3f038ab-65e0-44ba-9c19-b6764dc439cf"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpaceHit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerInput
        m_PlayerInput = asset.FindActionMap("PlayerInput", throwIfNotFound: true);
        m_PlayerInput_NormalHit = m_PlayerInput.FindAction("NormalHit", throwIfNotFound: true);
        m_PlayerInput_SpaceHit = m_PlayerInput.FindAction("SpaceHit", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerInput
    private readonly InputActionMap m_PlayerInput;
    private IPlayerInputActions m_PlayerInputActionsCallbackInterface;
    private readonly InputAction m_PlayerInput_NormalHit;
    private readonly InputAction m_PlayerInput_SpaceHit;
    public struct PlayerInputActions
    {
        private @GameInput m_Wrapper;
        public PlayerInputActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @NormalHit => m_Wrapper.m_PlayerInput_NormalHit;
        public InputAction @SpaceHit => m_Wrapper.m_PlayerInput_SpaceHit;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputActions instance)
        {
            if (m_Wrapper.m_PlayerInputActionsCallbackInterface != null)
            {
                @NormalHit.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnNormalHit;
                @NormalHit.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnNormalHit;
                @NormalHit.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnNormalHit;
                @SpaceHit.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSpaceHit;
                @SpaceHit.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSpaceHit;
                @SpaceHit.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSpaceHit;
            }
            m_Wrapper.m_PlayerInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @NormalHit.started += instance.OnNormalHit;
                @NormalHit.performed += instance.OnNormalHit;
                @NormalHit.canceled += instance.OnNormalHit;
                @SpaceHit.started += instance.OnSpaceHit;
                @SpaceHit.performed += instance.OnSpaceHit;
                @SpaceHit.canceled += instance.OnSpaceHit;
            }
        }
    }
    public PlayerInputActions @PlayerInput => new PlayerInputActions(this);
    public interface IPlayerInputActions
    {
        void OnNormalHit(InputAction.CallbackContext context);
        void OnSpaceHit(InputAction.CallbackContext context);
    }
}