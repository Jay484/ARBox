using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestProcessEvent : MonoBehaviour
{
    public KeyBoardKey key;
    private KeyBoardKey prevKey;


    private void Update()
    {
        if(prevKey != key)
        {
            prevKey = key;
            prevKey.OnClick();
        }
    }


}
