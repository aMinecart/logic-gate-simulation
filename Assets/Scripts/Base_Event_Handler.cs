// FOR REFERENCE ONLY
// DO NOT USE ATTACH THIS FILE TO A GAMEOBJECT
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEventController : MonoBehaviour
{
    [SerializeField] private InputReader input;

    private Vector2 move_direction;
    private Vector2 cursor_pos;
    private bool clicking;

    // commented out to prevent unnecessary computations - uncomment before use
    /*
    void Start()
    {
        // input.MoveEvent += handleMove;
        // input.LookEvent += handleLook;

        input.ClickEvent += handleClick;
        input.ClickReleasedEvent += handleReleasedClick;
    }

    private void handleMove(Vector2 dir)
    {
        move_direction = dir;
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
    */
}