using System;
using UnityEngine;

public class GateCollisionHandler : MonoBehaviour
{
    public static GameObject[] gates;
    private float gate_width = 2.0f;

    [RuntimeInitializeOnLoadMethod]
    private static void Initialization()
    {
        if (FindObjectOfType<GateCollisionHandler>() != null)
        {
            return;
        }
        
        var instance = new GameObject { name = "AutoSingleton" };
        instance.AddComponent<GateCollisionHandler>();
        DontDestroyOnLoad(instance);
    }

    private void Awake()
    {
        gates = GameObject.FindGameObjectsWithTag("LogicGate");
    }

    private void LateUpdate()
    {
        gates = GameObject.FindGameObjectsWithTag("LogicGate");

        for (int i = 0; i < gates.Length; i++)
        {
            int j = i + 1;
            while (j < gates.Length)
            {
                Vector3 i_pos = gates[i].transform.position;
                Vector3 j_pos = gates[j].transform.position;

                bool x_overlap = Math.Abs(j_pos.x - i_pos.x) < gate_width;
                bool y_overlap = Math.Abs(j_pos.y - i_pos.y) < gate_width;

                if (x_overlap && y_overlap)
                {
                    if (x_overlap)
                    {
                        j_pos.x += gate_width - (j_pos.x - i_pos.x);
                    }
                    
                    if (y_overlap)
                    {
                        j_pos.y += gate_width - (j_pos.y - i_pos.y);
                    }
                }

                gates[i].transform.position = i_pos;
                gates[j].transform.position = j_pos;
                
                j++;
            }
        }
    }
}