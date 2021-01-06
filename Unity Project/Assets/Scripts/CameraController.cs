using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private int TargetNum = 0;

    private GameObject[] Targets;

    void Start()
    {
        Targets = GameObject.FindGameObjectsWithTag("Jet");
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (TargetNum + 1 < Targets.Length)
            {
                TargetNum += 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (TargetNum > 0)
            {
                TargetNum -= 1;
            }
        }

        GameObject target = Targets[TargetNum];

        Vector3 moveCamTo = target.transform.position - target.transform.forward * 10.0f + Vector3.up * 5.0f;
        float bias = 0.4f;
        Camera.main.transform.position = Camera.main.transform.position * bias + moveCamTo * (1.0f - bias);
        Camera.main.transform.LookAt(target.transform.position + target.transform.forward * 20.0f);
    }
}
