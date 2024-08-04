using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerInput playerInput;

    public static Vector2 Movement;
    public static bool RangedWasPressed;
    public static bool LightWasPressed;
    public static bool HeavyWasPressed;

    private InputAction moveAction;
    private InputAction rangedAction;
    private InputAction lightAction;
    private InputAction heavyAction;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        rangedAction = playerInput.actions["Ranged"];
        lightAction = playerInput.actions["Light"];
        heavyAction = playerInput.actions["Heavy"];
    }

    // Update is called once per frame
    void Update()
    {
        Movement = moveAction.ReadValue<Vector2>();

        RangedWasPressed = rangedAction.WasPressedThisFrame();
        HeavyWasPressed = heavyAction.WasPressedThisFrame();
        LightWasPressed = lightAction.WasPressedThisFrame();
    }
}
