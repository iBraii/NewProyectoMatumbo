// GENERATED AUTOMATICALLY FROM 'Assets/REWORK/Scripts/Player/Controls/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Land"",
            ""id"": ""3a339577-3996-43b8-bab0-8aaab8e7dcd6"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""52c1a49d-6bda-422f-bbf1-9f2617f8ee63"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""00af5010-4c4a-4f27-a172-665ba4bb025c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""7e45cd03-4c9b-4d95-8c77-743d1beb5c73"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""b2bfb811-b760-47cf-9dd9-aa6ba9a5bad1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""725d9e98-970d-4433-89f2-e375c641dedc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Light"",
                    ""type"": ""Button"",
                    ""id"": ""20f0d76f-861a-4f4a-b106-4ae652c787bc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ResetLevel"",
                    ""type"": ""Button"",
                    ""id"": ""38f5f839-5c0a-4996-b2b8-a79af4a33db0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""15c5e7a9-2cb7-44c0-b460-7cf59cc13019"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NoClip"",
                    ""type"": ""Button"",
                    ""id"": ""d2957799-8904-4337-985e-3d7b474eec42"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""MultiTap""
                },
                {
                    ""name"": ""Inmortality"",
                    ""type"": ""Button"",
                    ""id"": ""459220c3-a21e-4f6a-840c-140dcfb8f135"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""MultiTap""
                },
                {
                    ""name"": ""InfiniteDC"",
                    ""type"": ""Button"",
                    ""id"": ""a537bdaa-fc0c-4594-ab34-0540bd9dca36"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""MultiTap""
                },
                {
                    ""name"": ""Speed"",
                    ""type"": ""Button"",
                    ""id"": ""f3c69f1f-51d2-4dc9-91aa-e77d3d11f961"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""MultiTap""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6f33089f-81d1-4f72-b164-f6662b319243"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Movement"",
                    ""id"": ""47d04186-c12e-4b26-80d3-22be1b4eb115"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d42bb296-7945-4bc1-b892-4034d222786a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7d11055e-863a-4b60-87ce-8fbc56db57ba"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c049262b-3fd8-43fb-8206-00b8ed3e2fd9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""75002a24-95d3-4aea-8e1f-80a95b2ceda8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""299ce7c0-10b2-4008-a73c-249da1da2c82"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""693add4a-551e-4936-9312-688f4db54a2f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""891b0bf0-4fa6-45a1-844b-f0ebe04720e1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f64e2a4b-96cb-4472-b236-04ccfa5e2d3b"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Light"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""100c3c65-9101-454e-8bc1-755bad19fbd5"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetLevel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec0737d9-7c7b-4d9f-b794-1214a0c987af"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""861eb1ff-edbd-49a7-8647-da5662dd103b"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NoClip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ff16fd1-eb14-4aa7-af1d-e291f30a1949"",
                    ""path"": ""<Keyboard>/f2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inmortality"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0556679c-2a88-4297-b415-1f8e3e171f04"",
                    ""path"": ""<Keyboard>/f3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InfiniteDC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94ce9b0d-0424-45ce-91f0-c0a8bb51ebb1"",
                    ""path"": ""<Keyboard>/f4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Speed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Land
        m_Land = asset.FindActionMap("Land", throwIfNotFound: true);
        m_Land_Jump = m_Land.FindAction("Jump", throwIfNotFound: true);
        m_Land_Move = m_Land.FindAction("Move", throwIfNotFound: true);
        m_Land_Look = m_Land.FindAction("Look", throwIfNotFound: true);
        m_Land_Interact = m_Land.FindAction("Interact", throwIfNotFound: true);
        m_Land_Attack = m_Land.FindAction("Attack", throwIfNotFound: true);
        m_Land_Light = m_Land.FindAction("Light", throwIfNotFound: true);
        m_Land_ResetLevel = m_Land.FindAction("ResetLevel", throwIfNotFound: true);
        m_Land_Escape = m_Land.FindAction("Escape", throwIfNotFound: true);
        m_Land_NoClip = m_Land.FindAction("NoClip", throwIfNotFound: true);
        m_Land_Inmortality = m_Land.FindAction("Inmortality", throwIfNotFound: true);
        m_Land_InfiniteDC = m_Land.FindAction("InfiniteDC", throwIfNotFound: true);
        m_Land_Speed = m_Land.FindAction("Speed", throwIfNotFound: true);
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

    // Land
    private readonly InputActionMap m_Land;
    private ILandActions m_LandActionsCallbackInterface;
    private readonly InputAction m_Land_Jump;
    private readonly InputAction m_Land_Move;
    private readonly InputAction m_Land_Look;
    private readonly InputAction m_Land_Interact;
    private readonly InputAction m_Land_Attack;
    private readonly InputAction m_Land_Light;
    private readonly InputAction m_Land_ResetLevel;
    private readonly InputAction m_Land_Escape;
    private readonly InputAction m_Land_NoClip;
    private readonly InputAction m_Land_Inmortality;
    private readonly InputAction m_Land_InfiniteDC;
    private readonly InputAction m_Land_Speed;
    public struct LandActions
    {
        private @PlayerControls m_Wrapper;
        public LandActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Land_Jump;
        public InputAction @Move => m_Wrapper.m_Land_Move;
        public InputAction @Look => m_Wrapper.m_Land_Look;
        public InputAction @Interact => m_Wrapper.m_Land_Interact;
        public InputAction @Attack => m_Wrapper.m_Land_Attack;
        public InputAction @Light => m_Wrapper.m_Land_Light;
        public InputAction @ResetLevel => m_Wrapper.m_Land_ResetLevel;
        public InputAction @Escape => m_Wrapper.m_Land_Escape;
        public InputAction @NoClip => m_Wrapper.m_Land_NoClip;
        public InputAction @Inmortality => m_Wrapper.m_Land_Inmortality;
        public InputAction @InfiniteDC => m_Wrapper.m_Land_InfiniteDC;
        public InputAction @Speed => m_Wrapper.m_Land_Speed;
        public InputActionMap Get() { return m_Wrapper.m_Land; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LandActions set) { return set.Get(); }
        public void SetCallbacks(ILandActions instance)
        {
            if (m_Wrapper.m_LandActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_LandActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnJump;
                @Move.started -= m_Wrapper.m_LandActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_LandActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnLook;
                @Interact.started -= m_Wrapper.m_LandActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnInteract;
                @Attack.started -= m_Wrapper.m_LandActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnAttack;
                @Light.started -= m_Wrapper.m_LandActionsCallbackInterface.OnLight;
                @Light.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnLight;
                @Light.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnLight;
                @ResetLevel.started -= m_Wrapper.m_LandActionsCallbackInterface.OnResetLevel;
                @ResetLevel.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnResetLevel;
                @ResetLevel.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnResetLevel;
                @Escape.started -= m_Wrapper.m_LandActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnEscape;
                @NoClip.started -= m_Wrapper.m_LandActionsCallbackInterface.OnNoClip;
                @NoClip.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnNoClip;
                @NoClip.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnNoClip;
                @Inmortality.started -= m_Wrapper.m_LandActionsCallbackInterface.OnInmortality;
                @Inmortality.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnInmortality;
                @Inmortality.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnInmortality;
                @InfiniteDC.started -= m_Wrapper.m_LandActionsCallbackInterface.OnInfiniteDC;
                @InfiniteDC.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnInfiniteDC;
                @InfiniteDC.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnInfiniteDC;
                @Speed.started -= m_Wrapper.m_LandActionsCallbackInterface.OnSpeed;
                @Speed.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnSpeed;
                @Speed.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnSpeed;
            }
            m_Wrapper.m_LandActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Light.started += instance.OnLight;
                @Light.performed += instance.OnLight;
                @Light.canceled += instance.OnLight;
                @ResetLevel.started += instance.OnResetLevel;
                @ResetLevel.performed += instance.OnResetLevel;
                @ResetLevel.canceled += instance.OnResetLevel;
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
                @NoClip.started += instance.OnNoClip;
                @NoClip.performed += instance.OnNoClip;
                @NoClip.canceled += instance.OnNoClip;
                @Inmortality.started += instance.OnInmortality;
                @Inmortality.performed += instance.OnInmortality;
                @Inmortality.canceled += instance.OnInmortality;
                @InfiniteDC.started += instance.OnInfiniteDC;
                @InfiniteDC.performed += instance.OnInfiniteDC;
                @InfiniteDC.canceled += instance.OnInfiniteDC;
                @Speed.started += instance.OnSpeed;
                @Speed.performed += instance.OnSpeed;
                @Speed.canceled += instance.OnSpeed;
            }
        }
    }
    public LandActions @Land => new LandActions(this);
    public interface ILandActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnLight(InputAction.CallbackContext context);
        void OnResetLevel(InputAction.CallbackContext context);
        void OnEscape(InputAction.CallbackContext context);
        void OnNoClip(InputAction.CallbackContext context);
        void OnInmortality(InputAction.CallbackContext context);
        void OnInfiniteDC(InputAction.CallbackContext context);
        void OnSpeed(InputAction.CallbackContext context);
    }
}
