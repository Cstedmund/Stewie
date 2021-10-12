using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbPsyche : MonoBehaviour {


    // Use this for initialization
    void OnTriggerStay(Collider other) {
        if (other.gameObject.name == "puppeeeeth" || other.gameObject.name == "darkling_ball")
        {
            other.gameObject.GetComponent<PlayerController>().decreaseSpiritRate = 0.05f;
        }
    }

    // Update is called once per frame
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "puppeeeeth" || other.gameObject.name == "darkling_ball")
        {
            other.gameObject.GetComponent<PlayerController>().decreaseSpiritRate = 0.4f;
        }
    }
}
