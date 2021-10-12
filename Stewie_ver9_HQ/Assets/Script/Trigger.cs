using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour {
	#region Public Members
    public CameraFollows camf;
    public GameObject cam1;
    public GameObject ball;
	public Text Story;
    public bool say = false;
	#endregion
	
	#region Private Members
    AudioListener ears;
    private int index=0;
    private int Timer;
    private string[] saying= {"This is the animal underworld... \nI should go across the human one","I remember that there was a portal near the valley.","Nothing can stop me from looking for him, let's go.", "Mission: Go to the portal next to the valley\nBeware of the devil" };
    #endregion

    private void Start()
    {
        ears = GetComponent<AudioListener>();
        ears.enabled = false;
    }
    private void Update()
    {
        if (!say && ball==null) {
            Story.text = saying[index];
            Timer += (int)(Time.deltaTime) + 1;
            if (Timer > 100 && index < 4) { index++; Timer = 0; }
            if (index == 4) {  say = true; }

        }
    }

    void OnTriggerEnter(Collider other)
    {
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "darkling_ball" && this.gameObject.name == "puppeeeeth")
        {
            this.gameObject.GetComponent<PlayerController>().enabled = true;
            this.gameObject.GetComponent<Animation>().Play("Waking");
            this.gameObject.GetComponent<ParticleSystem>().enableEmission = false;
            Destroy(other.gameObject);
            camf = cam1.GetComponent<CameraFollows>();
            camf.target = camf.target_2;
            ears.enabled = true;
            Story.text= "";
        }
        if (this.gameObject.name == "Dad" && other.name == "puppeeeeth")
        {
            Story.text = "This is my dad.. Who I was searching for.";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "darkling_ball")
        {
            Story.text = "";
        }
    }

}
