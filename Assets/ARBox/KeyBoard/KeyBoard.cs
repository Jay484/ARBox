using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyKeyBoard : MonoBehaviour
{
    private bool isShiftPressed = false;
    private bool capsLockOn = false;
    public TMP_InputField inputField;

    private void OnEnable()
    {
        AttachKeyBoardKeyScriptToChildObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessKeyDown(KeyCode keyCode)
    {
        if(keyCode == KeyCode.LeftShift || keyCode == KeyCode.RightShift)
        {
            isShiftPressed = !isShiftPressed;
        }
        else if(keyCode == KeyCode.CapsLock)
        {
            capsLockOn = !capsLockOn;
        }
        else{
            Event _event = Event.KeyboardEvent(keyCode.ToString());
            _event.character = KeyCodeToChar(keyCode, isShiftPressed, capsLockOn);
            _event.keyCode = keyCode;
            inputField.ProcessEvent(_event);
            inputField.ForceLabelUpdate();
            inputField.ActivateInputField();
        }
        
        //inputField.ProcessEvent();
    }

    

    private void AttachKeyBoardKeyScriptToChildObjects()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            var child = transform.GetChild(i);
            child.gameObject.AddComponent<KeyBoardKey>();
        }
    }


    public static char KeyCodeToChar(KeyCode keyCode,bool isShiftPressed, bool isCapsLockOn)
    {
        switch (keyCode)
        {
            // Alphanumeric keys
            case KeyCode.A: 
                if (isShiftPressed^isCapsLockOn)
                    return 'A';
                else
                    return 'a';
            case KeyCode.B:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'B';
                else
                    return 'b';
            case KeyCode.C:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'C';
                else
                    return 'c';
            case KeyCode.D:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'D';
                else
                    return 'd';
            case KeyCode.E:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'E';
                else
                    return 'e';
            case KeyCode.F:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'F';
                else
                    return 'f';
            case KeyCode.G:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'G';
                else
                    return 'g';
            case KeyCode.H:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'H';
                else
                    return 'h';
            case KeyCode.I:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'I';
                else
                    return 'i';
            case KeyCode.J:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'J';
                else
                    return 'j';
            case KeyCode.K:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'K';
                else
                    return 'k';
            case KeyCode.L:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'L';
                else
                    return 'l';
            case KeyCode.M:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'M';
                else
                    return 'm';
            case KeyCode.N:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'N';
                else
                    return 'n';
            case KeyCode.O:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'O';
                else
                    return 'o';
            case KeyCode.P:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'P';
                else
                    return 'p';
            case KeyCode.Q:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'Q';
                else
                    return 'q';
            case KeyCode.R:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'R';
                else
                    return 'r';
            case KeyCode.S:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'S';
                else
                    return 's';
            case KeyCode.T:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'T';
                else
                    return 't';
            case KeyCode.U:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'U';
                else
                    return 'u';
            case KeyCode.V:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'V';
                else
                    return 'v';
            case KeyCode.W:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'W';
                else
                    return 'w';
            case KeyCode.X:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'X';
                else
                    return 'x';
            case KeyCode.Y:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'Y';
                else
                    return 'y';
            case KeyCode.Z:
                if (isShiftPressed ^ isCapsLockOn)
                    return 'Z';
                else
                    return 'z';
            case KeyCode.Alpha0:
                if (isShiftPressed)
                    return ')';
                else
                   return '0';
            case KeyCode.Alpha1:
                if (isShiftPressed)
                    return '!';
                else
                    return '1';
            case KeyCode.Alpha2:
                if (isShiftPressed)
                    return '@';
                else
                    return '2';
            case KeyCode.Alpha3:
                if (isShiftPressed)
                    return '#';
                else
                    return '3';
            case KeyCode.Alpha4:
                if (isShiftPressed)
                    return '$';
                else
                    return '4';
            case KeyCode.Alpha5:
                if (isShiftPressed)
                    return '%';
                else
                    return '5';
            case KeyCode.Alpha6:
                if (isShiftPressed)
                    return '^';
                else
                    return '6';
            case KeyCode.Alpha7:
                if (isShiftPressed)
                    return '&';
                else
                    return '7';
            case KeyCode.Alpha8:
                if (isShiftPressed)
                    return '*';
                else
                    return '8';
            case KeyCode.Alpha9:
                if (isShiftPressed)
                    return '(';
                else
                    return '9';

            // Special characters
            case KeyCode.Space: return ' ';
            case KeyCode.Tab: return '\t';
            case KeyCode.Return: return '\n';
            case KeyCode.Backspace: return '\b'; // Backspace character
            case KeyCode.Comma:
                if (isShiftPressed)
                    return '<';
                else
                    return ',';
            case KeyCode.Period:
                if (isShiftPressed)
                    return '>';
                else
                    return '.';
            case KeyCode.Slash:
                if (isShiftPressed)
                    return '?';
                else
                    return '/';
            case KeyCode.BackQuote:
                if (isShiftPressed)
                    return '~';
                else
                    return '`';
            case KeyCode.LeftBracket:
                if (isShiftPressed)
                    return '{';
                else
                    return '[';
            case KeyCode.RightBracket:
                if (isShiftPressed)
                    return '}';
                else
                    return ']';
            case KeyCode.Minus:
                if (isShiftPressed)
                    return '_';
                else
                    return '-';
            case KeyCode.Equals:
                if (isShiftPressed)
                    return '+';
                else
                    return '=';
            case KeyCode.Semicolon:
                if (isShiftPressed)
                    return ':';
                else
                    return ';';
            case KeyCode.Quote:
                if (isShiftPressed)
                    return '"';
                else
                    return '\'';
            case KeyCode.Backslash:
                if (isShiftPressed)
                    return '|';
                else
                    return '\\';
            // Add more cases for other special characters as needed

            default: return '\0'; // Return null character for non-mapped keys
        }
    }

}
