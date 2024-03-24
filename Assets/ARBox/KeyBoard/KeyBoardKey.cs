using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardKey : MonoBehaviour
{
    bool isHover = false;
    MyKeyBoard keyBoard;
    KeyCode keyCode = KeyCode.None;

    private void Start()
    {
        gameObject.AddComponent<MeshCollider>();
    }

    private void OnEnable()
    {
        keyBoard = transform.parent.gameObject.GetComponent<MyKeyBoard>();
        if (keyBoard == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            keyCode = KeynameToKeyCode(transform.name.Split("_")[1]);
        }
    }

    public void OnClick()
    {
        keyBoard.ProcessKeyDown(keyCode);
    }

    public void OnHover()
    {
        isHover = false;
    }

    public void OnHoverReleased()
    {
        isHover = false;
    }


    public KeyCode KeynameToKeyCode(string int_name)
    {
        KeyCode code = KeyCode.None;
        int KeyValue;
        if (int.TryParse(int_name, out KeyValue))
        {
            code = ConvertJSToUnityKeyCode(KeyValue);
        }

        return code;
    }

    public static KeyCode ConvertJSToUnityKeyCode(int jsKeyCode)
    {
        switch (jsKeyCode)
        {
            case 8:
                return KeyCode.Backspace;
            case 9:
                return KeyCode.Tab;
            case 13:
                return KeyCode.Return;
            case 16:
                return KeyCode.LeftShift;
            case 17:
                return KeyCode.LeftControl;
            case 18:
                return KeyCode.LeftAlt;
            case 19:
                return KeyCode.Pause;
            case 20:
                return KeyCode.CapsLock;
            case 27:
                return KeyCode.Escape;
            case 32:
                return KeyCode.Space;
            case 33:
                return KeyCode.PageUp;
            case 34:
                return KeyCode.PageDown;
            case 35:
                return KeyCode.End;
            case 36:
                return KeyCode.Home;
            case 37:
                return KeyCode.LeftArrow;
            case 38:
                return KeyCode.UpArrow;
            case 39:
                return KeyCode.RightArrow;
            case 40:
                return KeyCode.DownArrow;
            case 45:
                return KeyCode.Insert;
            case 46:
                return KeyCode.Delete;
            case 48:
                return KeyCode.Alpha0;
            case 49:
                return KeyCode.Alpha1;
            case 50:
                return KeyCode.Alpha2;
            case 51:
                return KeyCode.Alpha3;
            case 52:
                return KeyCode.Alpha4;
            case 53:
                return KeyCode.Alpha5;
            case 54:
                return KeyCode.Alpha6;
            case 55:
                return KeyCode.Alpha7;
            case 56:
                return KeyCode.Alpha8;
            case 57:
                return KeyCode.Alpha9;
            case 65:
                return KeyCode.A;
            case 66:
                return KeyCode.B;
            case 67:
                return KeyCode.C;
            case 68:
                return KeyCode.D;
            case 69:
                return KeyCode.E;
            case 70:
                return KeyCode.F;
            case 71:
                return KeyCode.G;
            case 72:
                return KeyCode.H;
            case 73:
                return KeyCode.I;
            case 74:
                return KeyCode.J;
            case 75:
                return KeyCode.K;
            case 76:
                return KeyCode.L;
            case 77:
                return KeyCode.M;
            case 78:
                return KeyCode.N;
            case 79:
                return KeyCode.O;
            case 80:
                return KeyCode.P;
            case 81:
                return KeyCode.Q;
            case 82:
                return KeyCode.R;
            case 83:
                return KeyCode.S;
            case 84:
                return KeyCode.T;
            case 85:
                return KeyCode.U;
            case 86:
                return KeyCode.V;
            case 87:
                return KeyCode.W;
            case 88:
                return KeyCode.X;
            case 89:
                return KeyCode.Y;
            case 90:
                return KeyCode.Z;
            case 91:
                return KeyCode.LeftWindows;
            case 92:
                return KeyCode.RightWindows;
            case 93:
                return KeyCode.None;
            case 96:
                return KeyCode.Keypad0;
            case 97:
                return KeyCode.Keypad1;
            case 98:
                return KeyCode.Keypad2;
            case 99:
                return KeyCode.Keypad3;
            case 100:
                return KeyCode.Keypad4;
            case 101:
                return KeyCode.Keypad5;
            case 102:
                return KeyCode.Keypad6;
            case 103:
                return KeyCode.Keypad7;
            case 104:
                return KeyCode.Keypad8;
            case 105:
                return KeyCode.Keypad9;
            case 106:
                return KeyCode.KeypadMultiply;
            case 107:
                return KeyCode.KeypadPlus;
            case 108:
                return KeyCode.KeypadEnter; // Not directly mappable in Unity, using Enter for now
            case 109:
                return KeyCode.KeypadMinus;
            case 110:
                return KeyCode.KeypadPeriod;
            case 111:
                return KeyCode.KeypadDivide;
            case 112:
                return KeyCode.F1;
            case 113:
                return KeyCode.F2;
            case 114:
                return KeyCode.F3;
            case 115:
                return KeyCode.F4;
            case 116:
                return KeyCode.F5;
            case 117:
                return KeyCode.F6;
            case 118:
                return KeyCode.F7;
            case 119:
                return KeyCode.F8;
            case 120:
                return KeyCode.F9;
            case 121:
                return KeyCode.F10;
            case 122:
                return KeyCode.F11;
            case 123:
                return KeyCode.F12;
            case 144:
                return KeyCode.Numlock;
            case 145:
                return KeyCode.ScrollLock;
            case 186:
                return KeyCode.Semicolon;
            case 187:
                return KeyCode.Equals;
            case 188:
                return KeyCode.Comma;
            case 189:
                return KeyCode.Minus;
            case 190:
                return KeyCode.Period;
            case 191:
                return KeyCode.Slash;
            case 192:
                return KeyCode.BackQuote;
            case 219:
                return KeyCode.LeftBracket;
            case 220:
                return KeyCode.Backslash;
            case 221:
                return KeyCode.RightBracket;
            case 222:
                return KeyCode.Quote;
            default:
                Debug.LogWarning("JavaScript key code not mapped to Unity KeyCode: " + jsKeyCode);
                return KeyCode.None; // Return KeyCode.None for unmapped codes
        }

    }
}

