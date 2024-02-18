using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_Logic : MonoBehaviour
{
    public string type = "AND";
    
    public GameObject source_1;
    public GameObject source_2;

    [SerializeField] private bool input_1 = false;
    [SerializeField] private bool input_2 = false;

    // warning: this variable defaults to false. as a result, for the first cycle of the update function,
    // an input variable that depends on another gate for input will be always be false
    public bool output { get; set; }

    private enum gate_types
    {
        NOT, AND, OR,
        XOR,
        NAND, NOR, XNOR
    }

    private bool calculate(string operation, bool in_1, bool in_2)
    {
        switch (operation)
        {
            case "NOT":
                return !in_1;
            case "AND":
                return in_1 && in_2;
            case "OR":
                return in_1 || in_2;
            case "XOR":
                return in_1 != in_2;
            case "NAND":
                return !calculate("AND", in_1, in_2);
            case "NOR":
                return !calculate("OR", in_1, in_2);
            case "XNOR":
                return !calculate("XOR", in_1, in_2);
            default:
                print("invalid type given");
                break;
        }

        return false;
    }

    private void calc_output()
    {
        output = calculate(type, input_1, input_2);
    }

    // Update is called once per frame
    void Update()
    {
        if (source_1 != null && source_1.TryGetComponent(out Gate_Logic gate_one_logic))
        {
            // success
            input_1 = gate_one_logic.output;
        }

        if (source_2 != null && source_2.TryGetComponent(out Gate_Logic gate_two_logic))
        {
            // success
            input_2 = gate_two_logic.output;
        }

        calc_output();
    }
}