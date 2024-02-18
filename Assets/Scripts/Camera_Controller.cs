using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] private InputReader input;

    [SerializeField] private float speed = 0.01f;
    private Vector2 move_direction;

    // Start is called before the first frame update
    void Start()
    {
        input.MoveEvent += handleMove;
    }

    private void handleMove(Vector2 dir)
    {
        move_direction = dir;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(move_direction.x, move_direction.y, 0.0f) * speed;
    }
}