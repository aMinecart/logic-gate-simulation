using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateLogic : MonoBehaviour
{
    private GateController gate_controller;

    private GateTypes gate_type { get; set; } = GateTypes.OR;
    public int gate_type_as_num { get; set; } = 0;

    [HideInInspector] public GameObject input_gate_1;
    [HideInInspector] public GameObject input_gate_2;

    public bool input_1 = false;
    public bool input_2 = false;

    // please check if this warning is true
    // warning: this variable defaults to false. as a result, for the first cycle of the update function,
    // an input variable that depends on another gate for input will be always be false
    public bool output;
    
    public enum GateTypes
    {
        OR, AND, NOT,
        XOR,
        NOR, NAND, XNOR,
        DOES_SHE_KNOW_HOW_TO_MAKE_A_GRILLED_CHEESE
    }

    void Start()
    {
        gate_controller = GetComponent<GateController>();
    }

    private GateTypes calc_type(int identifier)
    {
        switch (identifier)
        {
            case 0:
                return GateTypes.OR;
            case 1:
                return GateTypes.AND;
            case 2:
                return GateTypes.NOT;
            case 3:
                return GateTypes.XOR;
            case 4:
                return GateTypes.NOR;
            case 5:
                return GateTypes.NAND;
            case 6:
                return GateTypes.XNOR;
            default:
                print("unexpected indentifier, defaulting to OR");
                return GateTypes.OR;
        }
    }

    private bool handle_output(GameObject gate, out bool result)
    {
        result = false;

        if (gate == null)
        {
            return false;
        }

        if (gate.TryGetComponent<GateLogic>(out GateLogic other_gate_logic))
        {
            result = other_gate_logic.output;
            return true;
        }

        print("error, no gate logic component found");
        return false;
    }

    private bool calculate(GateTypes operation, bool in_1, bool in_2)
    {
        switch (operation)
        {
            case GateTypes.NOT:
                return !in_1;
            case GateTypes.AND:
                return in_1 && in_2;
            case GateTypes.OR:
                return in_1 || in_2;
            case GateTypes.XOR:
                return in_1 != in_2;
            case GateTypes.NAND:
                return !calculate(GateTypes.AND, in_1, in_2);
            case GateTypes.NOR:
                return !calculate(GateTypes.OR, in_1, in_2);
            case GateTypes.XNOR:
                return !calculate(GateTypes.XOR, in_1, in_2);
            case GateTypes.DOES_SHE_KNOW_HOW_TO_MAKE_A_GRILLED_CHEESE:
                return false;
            default:
                return in_1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gate_type = calc_type(gate_type_as_num);

        if (handle_output(input_gate_1, out bool result_1))
        {
            input_1 = result_1;
        }

        if (handle_output(input_gate_2, out bool result_2))
        {
            input_2 = result_2;
        }
        
        if (gate_controller.menu_open_to_attached_gate)
        {
            gate_controller.toggle_1.isOn = input_1;
            gate_controller.toggle_2.isOn = input_2;
        }

        output = calculate(gate_type, input_1, input_2);

        if (this.gameObject.name == "End Gate")
        {
            GetComponentInChildren<TextMeshPro>().SetText($"Output: {output}");
        }

        // print(gate_type);
    }
}