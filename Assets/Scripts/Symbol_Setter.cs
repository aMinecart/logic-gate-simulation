using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol_Setter : MonoBehaviour
{
    [SerializeField] private Sprite[] symbols = new Sprite[7];
    
    private GateLogic gate_logic;
    private SpriteRenderer sprite_renderer;

    // Start is called before the first frame update
    void Start()
    {
        gate_logic = GetComponentInParent<GateLogic>();
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        sprite_renderer.sprite = symbols[gate_logic.gate_type_as_num];
    }
}
