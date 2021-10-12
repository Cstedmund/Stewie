using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameFinished : MonoBehaviour {

    public levelChanger gamefinished;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider others)
    {
        if (others.name == "puppeeeeth")
        {
            gamefinished.GameFinished();
        }
    }

}
