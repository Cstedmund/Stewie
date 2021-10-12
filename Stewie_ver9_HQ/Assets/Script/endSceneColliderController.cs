using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endSceneColliderController : MonoBehaviour {

    public stageController stage;


    // Use this for initialization
    void Start()
    {
        stage = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<stageController>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "puppeeeeth")
        {
            Debug.Log("Enter the door");
            stage.fsa = false;
            stage.pa = false;
            stage.esa = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        stage.fsa = false;
        stage.pa = false;
        stage.esa = false;
    }
}
