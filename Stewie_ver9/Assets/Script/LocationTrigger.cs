using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationTrigger : MonoBehaviour
{
    public Text LocationText;
    private string locate;

    private void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
    }
    void OnTriggerStay(Collider other)
    {if (other.gameObject.name == "puppeeeeth" || other.gameObject.name == "darkling_ball")
        {
            LocationText.text = "Location: " + this.gameObject.name;
        }
    }
    void OnTriggerExit(Collider other)
    {  
    }
}
