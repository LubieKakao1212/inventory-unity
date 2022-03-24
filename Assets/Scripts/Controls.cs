using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    public static event System.Action<Vector2> mousePosition;

    public static InputAction CursorPos => cursorPos;

    private static InputAction cursorPos;

    [SerializeField]
    private InputMap inputMap;

    private void Awake()
    {
        if (inputMap == null)
        {
            inputMap = new InputMap();
        }

        cursorPos = inputMap.UI.Cursor;

        //I could have used a Lambda here
        inputMap.UI.Cursor.performed += (args) => mousePosition?.Invoke(args.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        inputMap.Enable();
    }

    private void OnDisable()
    {
        inputMap.Disable();
    }
}
