//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.8.2
//     from Assets/Inputs/Inputs.inputactions
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
using UnityEngine;

namespace pji2918.Input
{
    public partial class @Inputs: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Inputs()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""1195f908-4421-4f56-a6e3-35de1388c9ec"",
            ""actions"": [
                {
                    ""name"": ""PlayerMove"",
                    ""type"": ""Value"",
                    ""id"": ""5baf83a5-3de7-48ba-9943-4c44ecb195b0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""308991a1-e5ca-4ae5-a36c-afc5234072b9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""64b86277-c4df-4939-85df-05150fbd6167"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""3966528d-246d-4006-bd9a-f1782729f962"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""303d1309-a299-4693-866a-9c342acb5e7b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Mouse"",
                    ""type"": ""Value"",
                    ""id"": ""746e7095-e9f4-45f1-b754-1316471f2d5a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TouchPos"",
                    ""type"": ""Value"",
                    ""id"": ""5303707b-7348-4f05-9e36-12b960ff69ea"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""isTouched"",
                    ""type"": ""Button"",
                    ""id"": ""479a1875-9917-4265-bbb6-d3d97a397d87"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""c457cf88-4386-47b2-a3df-a45eebb37e4c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""585d332e-c23a-4bed-a20b-14ba5848ede1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8132be6d-9798-48ce-82cb-419736bff089"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c5b451ee-4791-41b3-a019-d1546ad8d462"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ca178e2e-c4c7-4998-96e2-b9ef62b5cda8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4f95b88f-2926-4a4e-929d-fa6204c5485b"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC;Mobile"",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6bb4eaba-55ac-4664-a45e-b6f4515bd6ee"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e98f760b-b826-4dbc-a800-d6a945ead46a"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""91122a84-2930-4448-b2ad-7a9091f8ec23"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd7de9e0-7474-41bd-8bfb-d7292fd9daa4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Mouse"",
                    ""id"": ""f50e7a32-8e49-4608-b608-457de3869c00"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e95fecff-5dfd-45c6-910b-0c5c608bed84"",
                    ""path"": ""<Mouse>/delta/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a9c33e42-f48b-4ece-b344-b59989765309"",
                    ""path"": ""<Mouse>/delta/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ac7f25af-4c82-41dd-9b8a-5766156b5f52"",
                    ""path"": ""<Mouse>/delta/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8d6429bc-c3c9-4497-a4e0-c0d0fcb86b45"",
                    ""path"": ""<Mouse>/delta/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a395c07c-dc49-496d-90ab-7bd17c365ec7"",
                    ""path"": ""<Touchscreen>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""913e3d50-fdd8-4672-805b-e2f9b51a9b6f"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC;Mobile"",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b7deb2f1-4ce5-403f-8b39-450a330212b7"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""TouchPos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7bc6ea1f-1a70-4b94-9bcd-86e4e837a4ae"",
                    ""path"": ""<Touchscreen>/touch*/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""isTouched"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""System"",
            ""id"": ""80662060-e276-4b85-ab17-bbd7d4999c9a"",
            ""actions"": [
                {
                    ""name"": ""Screenshot"",
                    ""type"": ""Button"",
                    ""id"": ""201d9a8b-a67a-4782-9306-52999d1b7b31"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Debug"",
                    ""type"": ""Button"",
                    ""id"": ""4541b304-1c55-435b-923c-a6ce3b184805"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePos"",
                    ""type"": ""Value"",
                    ""id"": ""63c212c4-c4e8-45a8-878c-96298224d9d3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c63bdffa-f19f-46f1-90a5-209a618f8596"",
                    ""path"": ""<Keyboard>/f4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC;Mobile"",
                    ""action"": ""Screenshot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0f4e13e-0343-4094-abde-7b3d2c672d93"",
                    ""path"": ""<Keyboard>/f3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC;Mobile"",
                    ""action"": ""Debug"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""955ac807-eb96-4338-9962-6e4c41e67bf4"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""MousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Mobile"",
            ""bindingGroup"": ""Mobile"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_PlayerMove = m_Player.FindAction("PlayerMove", throwIfNotFound: true);
            m_Player_Inventory = m_Player.FindAction("Inventory", throwIfNotFound: true);
            m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
            m_Player_Reload = m_Player.FindAction("Reload", throwIfNotFound: true);
            m_Player_Fire = m_Player.FindAction("Fire", throwIfNotFound: true);
            m_Player_Mouse = m_Player.FindAction("Mouse", throwIfNotFound: true);
            m_Player_TouchPos = m_Player.FindAction("TouchPos", throwIfNotFound: true);
            m_Player_isTouched = m_Player.FindAction("isTouched", throwIfNotFound: true);
            // System
            m_System = asset.FindActionMap("System", throwIfNotFound: true);
            m_System_Screenshot = m_System.FindAction("Screenshot", throwIfNotFound: true);
            m_System_Debug = m_System.FindAction("Debug", throwIfNotFound: true);
            m_System_MousePos = m_System.FindAction("MousePos", throwIfNotFound: true);
        }

        ~@Inputs()
        {
            Debug.Assert(!m_Player.enabled, "This will cause a leak and performance issues, Inputs.Player.Disable() has not been called.");
            Debug.Assert(!m_System.enabled, "This will cause a leak and performance issues, Inputs.System.Disable() has not been called.");
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

        // Player
        private readonly InputActionMap m_Player;
        private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
        private readonly InputAction m_Player_PlayerMove;
        private readonly InputAction m_Player_Inventory;
        private readonly InputAction m_Player_Interact;
        private readonly InputAction m_Player_Reload;
        private readonly InputAction m_Player_Fire;
        private readonly InputAction m_Player_Mouse;
        private readonly InputAction m_Player_TouchPos;
        private readonly InputAction m_Player_isTouched;
        public struct PlayerActions
        {
            private @Inputs m_Wrapper;
            public PlayerActions(@Inputs wrapper) { m_Wrapper = wrapper; }
            public InputAction @PlayerMove => m_Wrapper.m_Player_PlayerMove;
            public InputAction @Inventory => m_Wrapper.m_Player_Inventory;
            public InputAction @Interact => m_Wrapper.m_Player_Interact;
            public InputAction @Reload => m_Wrapper.m_Player_Reload;
            public InputAction @Fire => m_Wrapper.m_Player_Fire;
            public InputAction @Mouse => m_Wrapper.m_Player_Mouse;
            public InputAction @TouchPos => m_Wrapper.m_Player_TouchPos;
            public InputAction @isTouched => m_Wrapper.m_Player_isTouched;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void AddCallbacks(IPlayerActions instance)
            {
                if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
                @PlayerMove.started += instance.OnPlayerMove;
                @PlayerMove.performed += instance.OnPlayerMove;
                @PlayerMove.canceled += instance.OnPlayerMove;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
                @TouchPos.started += instance.OnTouchPos;
                @TouchPos.performed += instance.OnTouchPos;
                @TouchPos.canceled += instance.OnTouchPos;
                @isTouched.started += instance.OnIsTouched;
                @isTouched.performed += instance.OnIsTouched;
                @isTouched.canceled += instance.OnIsTouched;
            }

            private void UnregisterCallbacks(IPlayerActions instance)
            {
                @PlayerMove.started -= instance.OnPlayerMove;
                @PlayerMove.performed -= instance.OnPlayerMove;
                @PlayerMove.canceled -= instance.OnPlayerMove;
                @Inventory.started -= instance.OnInventory;
                @Inventory.performed -= instance.OnInventory;
                @Inventory.canceled -= instance.OnInventory;
                @Interact.started -= instance.OnInteract;
                @Interact.performed -= instance.OnInteract;
                @Interact.canceled -= instance.OnInteract;
                @Reload.started -= instance.OnReload;
                @Reload.performed -= instance.OnReload;
                @Reload.canceled -= instance.OnReload;
                @Fire.started -= instance.OnFire;
                @Fire.performed -= instance.OnFire;
                @Fire.canceled -= instance.OnFire;
                @Mouse.started -= instance.OnMouse;
                @Mouse.performed -= instance.OnMouse;
                @Mouse.canceled -= instance.OnMouse;
                @TouchPos.started -= instance.OnTouchPos;
                @TouchPos.performed -= instance.OnTouchPos;
                @TouchPos.canceled -= instance.OnTouchPos;
                @isTouched.started -= instance.OnIsTouched;
                @isTouched.performed -= instance.OnIsTouched;
                @isTouched.canceled -= instance.OnIsTouched;
            }

            public void RemoveCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IPlayerActions instance)
            {
                foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public PlayerActions @Player => new PlayerActions(this);

        // System
        private readonly InputActionMap m_System;
        private List<ISystemActions> m_SystemActionsCallbackInterfaces = new List<ISystemActions>();
        private readonly InputAction m_System_Screenshot;
        private readonly InputAction m_System_Debug;
        private readonly InputAction m_System_MousePos;
        public struct SystemActions
        {
            private @Inputs m_Wrapper;
            public SystemActions(@Inputs wrapper) { m_Wrapper = wrapper; }
            public InputAction @Screenshot => m_Wrapper.m_System_Screenshot;
            public InputAction @Debug => m_Wrapper.m_System_Debug;
            public InputAction @MousePos => m_Wrapper.m_System_MousePos;
            public InputActionMap Get() { return m_Wrapper.m_System; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(SystemActions set) { return set.Get(); }
            public void AddCallbacks(ISystemActions instance)
            {
                if (instance == null || m_Wrapper.m_SystemActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_SystemActionsCallbackInterfaces.Add(instance);
                @Screenshot.started += instance.OnScreenshot;
                @Screenshot.performed += instance.OnScreenshot;
                @Screenshot.canceled += instance.OnScreenshot;
                @Debug.started += instance.OnDebug;
                @Debug.performed += instance.OnDebug;
                @Debug.canceled += instance.OnDebug;
                @MousePos.started += instance.OnMousePos;
                @MousePos.performed += instance.OnMousePos;
                @MousePos.canceled += instance.OnMousePos;
            }

            private void UnregisterCallbacks(ISystemActions instance)
            {
                @Screenshot.started -= instance.OnScreenshot;
                @Screenshot.performed -= instance.OnScreenshot;
                @Screenshot.canceled -= instance.OnScreenshot;
                @Debug.started -= instance.OnDebug;
                @Debug.performed -= instance.OnDebug;
                @Debug.canceled -= instance.OnDebug;
                @MousePos.started -= instance.OnMousePos;
                @MousePos.performed -= instance.OnMousePos;
                @MousePos.canceled -= instance.OnMousePos;
            }

            public void RemoveCallbacks(ISystemActions instance)
            {
                if (m_Wrapper.m_SystemActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(ISystemActions instance)
            {
                foreach (var item in m_Wrapper.m_SystemActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_SystemActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public SystemActions @System => new SystemActions(this);
        private int m_PCSchemeIndex = -1;
        public InputControlScheme PCScheme
        {
            get
            {
                if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
                return asset.controlSchemes[m_PCSchemeIndex];
            }
        }
        private int m_MobileSchemeIndex = -1;
        public InputControlScheme MobileScheme
        {
            get
            {
                if (m_MobileSchemeIndex == -1) m_MobileSchemeIndex = asset.FindControlSchemeIndex("Mobile");
                return asset.controlSchemes[m_MobileSchemeIndex];
            }
        }
        public interface IPlayerActions
        {
            void OnPlayerMove(InputAction.CallbackContext context);
            void OnInventory(InputAction.CallbackContext context);
            void OnInteract(InputAction.CallbackContext context);
            void OnReload(InputAction.CallbackContext context);
            void OnFire(InputAction.CallbackContext context);
            void OnMouse(InputAction.CallbackContext context);
            void OnTouchPos(InputAction.CallbackContext context);
            void OnIsTouched(InputAction.CallbackContext context);
        }
        public interface ISystemActions
        {
            void OnScreenshot(InputAction.CallbackContext context);
            void OnDebug(InputAction.CallbackContext context);
            void OnMousePos(InputAction.CallbackContext context);
        }
    }
}
