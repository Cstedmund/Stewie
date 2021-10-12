using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tut : MonoBehaviour {
	
	#region Public Members
    public Text Story;
	public GameObject player;
	#endregion
	#region Private Members
    private int Timer;
    private int index=0;
    private bool tut;
    private string[] story= { "My name is Stewie, \nI was actually looking for something...", "Mission: Collect the rememtal to get back your memory." };
    #endregion
	// Use this for initialization
    void Start () {
        player.GetComponent<PlayerController>().enabled = false;
        tut = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (tut)
        {
            Story.text = story[index];
            Timer += (int)(Time.deltaTime ) + 1;
            if (Timer > 50 && index <2) {  index++; Timer = 0; }
            if (index ==2) { player.GetComponent<PlayerController>().enabled = true;tut = false; }
        }
        

    }
  
}
