using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiControllerManager : MonoBehaviour
{

    public AiController[] aiControllers;

    // Start is called before the first frame update
    void Start()
    {
        aiControllers = GetComponents<AiController>();
    }

    // Update is called once per frame
    public void step()
    {

        foreach (AiController c in aiControllers) {
            if (c.capturedEntities.Count > 0) {
                c.step();
            }
        }

    }

    public AiController[] getControllers() {
        return GetComponents<AiController>();
    }
}
