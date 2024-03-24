using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerKeyboardBinding
{

    public static bool WasConfirmKeyPressedThisFrame()
    {
        return Keyboard.current.oKey.wasPressedThisFrame;
    }

    public static bool WasConfirmKeyReleasedThisFrame()
    {
        return Keyboard.current.oKey.wasReleasedThisFrame; 
    }

    public static bool IsConfirmKeyPressed()
    {
        return Keyboard.current.oKey.isPressed;
    }




    public static bool WasAKeyPressedThisFrame()
    {
        return Keyboard.current.hKey.wasPressedThisFrame;
    }

    public static bool WasAKeyReleasedThisFrame()
    {
        return Keyboard.current.hKey.wasReleasedThisFrame;
    }

    public static bool IsAKeyPressed()
    {
        return Keyboard.current.hKey.isPressed;
    }



    public static bool WasBKeyPressedThisFrame()
    {
        return Keyboard.current.uKey.wasPressedThisFrame;
    }

    public static bool WasBKeyReleasedThisFrame()
    {
        return Keyboard.current.uKey.wasReleasedThisFrame;
    }

    public static bool IsBKeyPressed()
    {
        return Keyboard.current.uKey.isPressed;
    }



    public static bool WasXKeyPressedThisFrame()
    {
        return Keyboard.current.yKey.wasPressedThisFrame;
    }

    public static bool WasXKeyReleasedThisFrame()
    {
        return Keyboard.current.yKey.wasReleasedThisFrame;
    }

    public static bool IsXKeyPressed()
    {
        return Keyboard.current.yKey.isPressed;
    }


    public static bool WasYKeyPressedThisFrame()
    {
        return Keyboard.current.jKey.wasPressedThisFrame;
    }

    public static bool WasYKeyReleasedThisFrame()
    {
        return Keyboard.current.jKey.wasReleasedThisFrame;
    }

    public static bool IsYKeyPressed()
    {
        return Keyboard.current.jKey.isPressed;
    }



    public static bool WasUpKeyPressedThisFrame()
    {
        return Keyboard.current.aKey.wasPressedThisFrame;
    }

    public static bool WasUpKeyReleasedThisFrame()
    {
        return Keyboard.current.aKey.wasReleasedThisFrame;
    }

    public static bool IsUpKeyPressed()
    {
        return Keyboard.current.aKey.isPressed;
    }



    public static bool WasDownKeyPressedThisFrame()
    {
        return Keyboard.current.dKey.wasPressedThisFrame;
    }

    public static bool WasDownKeyReleasedThisFrame()
    {
        return Keyboard.current.dKey.wasReleasedThisFrame;
    }

    public static bool IsDownKeyPressed()
    {
        return Keyboard.current.dKey.isPressed;
    }



    public static bool WasLeftKeyPressedThisFrame()
    {
        return Keyboard.current.xKey.wasPressedThisFrame;
    }

    public static bool WasLeftKeyReleasedThisFrame()
    {
        return Keyboard.current.xKey.wasReleasedThisFrame;
    }

    public static bool IsLeftKeyPressed()
    {
        return Keyboard.current.xKey.isPressed;
    }


    public static bool WasRightKeyPressedThisFrame()
    {
        return Keyboard.current.wKey.wasPressedThisFrame;
    }

    public static bool WasRightKeyReleasedThisFrame()
    {
        return Keyboard.current.wKey.wasReleasedThisFrame;
    }

    public static bool IsRightKeyPressed()
    {
        return Keyboard.current.wKey.isPressed;
    }

}
