using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GateController : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private GameObject menu;
    private GateLogic gate_logic;

    private List<GameObject> other_gates;

    [SerializeField] private int preset_type;
    [SerializeField] private GameObject preset_input_gate_1;
    [SerializeField] private GameObject preset_input_gate_2;

    [HideInInspector] public Toggle toggle_1;
    [HideInInspector] public Toggle toggle_2;

    private TMP_Dropdown type_dropdown;
    private TMP_Dropdown in1_dropdown;
    private TMP_Dropdown in2_dropdown;

    [HideInInspector] public bool menu_open_to_attached_gate = false;
    private bool menu_open;

    private int dropdown_tracker_1;
    private int dropdown_tracker_2;

    private Vector2 cursor_pos;
    private bool clicking;

    // Start is called before the first frame update
    void Start()
    {
        input.LookEvent += handleLook;
        input.ClickEvent += handleClick;
        input.ClickReleasedEvent += handleReleasedClick;

        other_gates = get_other_gates();

        gate_logic = GetComponent<GateLogic>();
        toggle_1 = menu.transform.GetChild(2).gameObject.GetComponent<Toggle>();
        toggle_2 = menu.transform.GetChild(3).gameObject.GetComponent<Toggle>();
        type_dropdown = menu.transform.GetChild(7).gameObject.GetComponent<TMP_Dropdown>();
        in1_dropdown = menu.transform.GetChild(8).gameObject.GetComponent<TMP_Dropdown>();
        in2_dropdown = menu.transform.GetChild(9).gameObject.GetComponent<TMP_Dropdown>();

        gate_logic.gate_type_as_num = preset_type;

        dropdown_tracker_1 =  Math.Max(GateCollisionHandler.gates.Length - 1, 0);
        dropdown_tracker_2 = Math.Max(GateCollisionHandler.gates.Length - 1, 0);

        if (other_gates.Contains(preset_input_gate_1))
        {
            int index = other_gates.FindIndex(x => x == preset_input_gate_1);
            gate_logic.input_gate_1 = other_gates[index];
            dropdown_tracker_1 = index;
        }

        if (other_gates.Contains(preset_input_gate_2))
        {
            int index = other_gates.FindIndex(x => x == preset_input_gate_2);
            gate_logic.input_gate_2 = other_gates[index];
            dropdown_tracker_2 = index;
        }
    }

    private void handleLook(Vector2 pos)
    {
        cursor_pos = Camera.main.ScreenToWorldPoint(pos);
    }

    private void handleClick()
    {
        clicking = true;
    }

    private void handleReleasedClick()
    {
        clicking = false;
    }

    private List<GameObject> get_other_gates()
    {
        List<GameObject> gates = new List<GameObject>(GateCollisionHandler.gates);
        gates.Remove(this.gameObject);
        return gates;
    }

    // Update is called once per frame
    void Update()
    {
        if (!menu_open)
        {
            menu_open_to_attached_gate = false;
        }

        if (clicking &&
            !menu_open &&
            (Math.Abs(cursor_pos.x - transform.position.x) < transform.localScale.x / 2) &&
            (Math.Abs(cursor_pos.y - transform.position.y ) < transform.localScale.y / 2))
        {
            menu_open_to_attached_gate = true;

            // create list of logic gates and their names, excluding the gate
            // this script is attached to
            other_gates = get_other_gates();

            List<string> gate_names = new List<string>();

            foreach (GameObject gate in other_gates)
            {
                gate_names.Add(gate.name);
            }
            gate_names.Add("N/A");

            // handle type selection
            type_dropdown.onValueChanged.RemoveAllListeners();
            type_dropdown.SetValueWithoutNotify(gate_logic.gate_type_as_num);

            type_dropdown.onValueChanged.AddListener(delegate {
                gate_logic.gate_type_as_num = type_dropdown.value;
            });

            // handle input 1 selection
            in1_dropdown.onValueChanged.RemoveAllListeners();

            in1_dropdown.ClearOptions();
            in1_dropdown.AddOptions(gate_names);

            in1_dropdown.SetValueWithoutNotify(dropdown_tracker_1);
            in1_dropdown.RefreshShownValue();
            
            in1_dropdown.onValueChanged.AddListener(delegate {
                dropdown_tracker_1 = in1_dropdown.value;
                
                if (in1_dropdown.value < in1_dropdown.options.Count - 1)
                {
                    gate_logic.input_gate_1 = other_gates[in1_dropdown.value];
                }
                else
                {
                    gate_logic.input_gate_1 = null;
                }
            });

            // handle input 2 selection
            in2_dropdown.onValueChanged.RemoveAllListeners();

            in2_dropdown.ClearOptions();
            in2_dropdown.AddOptions(gate_names);

            in2_dropdown.SetValueWithoutNotify(dropdown_tracker_2);
            in2_dropdown.RefreshShownValue();

            in2_dropdown.onValueChanged.AddListener(delegate {
                dropdown_tracker_2 = in2_dropdown.value;

                if (in2_dropdown.value < in2_dropdown.options.Count - 1)
                {
                    gate_logic.input_gate_2 = other_gates[in2_dropdown.value];
                }
                else
                {
                    gate_logic.input_gate_2 = null;
                }
            });

            // make menu visible
            menu.SetActive(true);
        }
        
        menu_open = menu.activeInHierarchy;
    }
}