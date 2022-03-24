using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    public static event System.Action<Vector2> mousePosition;

    [SerializeField]
    private InputMap inputMap;

    private void Awake()
    {
        if (inputMap == null)
        {
            inputMap = new InputMap();
        }

        //I could have used a Lambda here
        inputMap.UI.Cursor.performed += InvokeMousePositionChange;
    }

    private void OnEnable()
    {
        inputMap.Enable();
    }

    private void InvokeMousePositionChange(InputAction.CallbackContext args)
    { 
        mousePosition?.Invoke(args.ReadValue<Vector2>()); 
    }

    private void OnDisable()
    {
        inputMap.Disable();
    }
}
