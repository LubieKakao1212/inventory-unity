// GENERATED AUTOMATICALLY FROM 'Assets/Def/InputMap.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMap : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMap"",
    ""maps"": [
        {
            ""name"": ""UI"",
            ""id"": ""c8d95016-8f9a-4688-98d1-0d0d4ac7d639"",
            ""actions"": [
                {
                    ""name"": ""Cursor"",
                    ""type"": ""Value"",
                    ""id"": ""1b742737-458a-4670-95db-ef58c80df5f6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""742f824f-71ec-4599-916e-ebea30e51cda"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Clicks"",
            ""id"": ""28066a7e-c195-4e67-a765-cf245a3776f8"",
            ""actions"": [
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""4225f2f0-799f-4518-92f7-b104414fa0ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1d798e00-9dd1-454f-a7cc-62ead645e8f3"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Cursor = m_UI.FindAction("Cursor", throwIfNotFound: true);
        // Clicks
        m_Clicks = asset.FindActionMap("Clicks", throwIfNotFound: true);
        m_Clicks_Right = m_Clicks.FindAction("Right", throwIfNotFound: true);
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

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Cursor;
    public struct UIActions
    {
        private @InputMap m_Wrapper;
        public UIActions(@InputMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @Cursor => m_Wrapper.m_UI_Cursor;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Cursor.started -= m_Wrapper.m_UIActionsCallbackInterface.OnCursor;
                @Cursor.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnCursor;
                @Cursor.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnCursor;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Cursor.started += instance.OnCursor;
                @Cursor.performed += instance.OnCursor;
                @Cursor.canceled += instance.OnCursor;
            }
        }
    }
    public UIActions @UI => new UIActions(this);

    // Clicks
    private readonly InputActionMap m_Clicks;
    private IClicksActions m_ClicksActionsCallbackInterface;
    private readonly InputAction m_Clicks_Right;
    public struct ClicksActions
    {
        private @InputMap m_Wrapper;
        public ClicksActions(@InputMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @Right => m_Wrapper.m_Clicks_Right;
        public InputActionMap Get() { return m_Wrapper.m_Clicks; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ClicksActions set) { return set.Get(); }
        public void SetCallbacks(IClicksActions instance)
        {
            if (m_Wrapper.m_ClicksActionsCallbackInterface != null)
            {
                @Right.started -= m_Wrapper.m_ClicksActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_ClicksActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_ClicksActionsCallbackInterface.OnRight;
            }
            m_Wrapper.m_ClicksActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
            }
        }
    }
    public ClicksActions @Clicks => new ClicksActions(this);
    public interface IUIActions
    {
        void OnCursor(InputAction.CallbackContext context);
    }
    public interface IClicksActions
    {
        void OnRight(InputAction.CallbackContext context);
    }
}
