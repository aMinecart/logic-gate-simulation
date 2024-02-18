using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_Controller : MonoBehaviour
{
    [SerializeField] private InputReader input;

    private Vector2 cursor_pos;
    private bool clicking;

    // Start is called before the first frame update
    void Start()
    {
        input.LookEvent += handleLook;

        input.ClickEvent += handleClick;
        input.ClickReleasedEvent += handleReleasedClick;
    }

    private void handleLook(Vector2 pos)
    {
        cursor_pos += pos;
    }

    private void handleClick()
    {
        clicking = true;
    }

    private void handleReleasedClick()
    {
        clicking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (clicking)
        {
            print(cursor_pos);
        }
        else
        {
            print("not clicking");
        }
    }
}