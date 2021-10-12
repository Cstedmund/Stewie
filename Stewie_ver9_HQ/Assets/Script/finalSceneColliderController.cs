using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalSceneColliderController : MonoBehaviour {

    public stageController stage;
    private GameObject player;
    

	// Use this for initialization
	void Start () {
        stage = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<stageController>();
        player = GameObject.FindGameObjectWithTag(Tags.player);
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            stage.fsa = true;
            stage.pa = false;
            stage.esa = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            stage.fsa = false;
            stage.pa = false;
            stage.esa = false;
        }

        if (other.name == "puppeeeeth")
        {
            stage.fsa = false;
            stage.pa = true;
            stage.esa = false;
        }
    }


}
