using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crystals : MonoBehaviour
{
	
	#region Public Members
    public string Name;
    public string InteractText = "";
    public int crystalvalue = 10;
    public AudioClip crystalTrigger;
    public AudioClip crystalDestory;
    public Text Story;
	public GameObject[] crystallist;
	#endregion
	
	#region Private Members
	ParticleSystem ps;
    PlayerController playerScript;
    private GameObject player;
    private int Timer = 0;
    private int index=0;
    private int cindex = 0;
    private int counter = 0;
    private string[] sentence = { "\"Stewie...Good Boy!\" \n\"Woof!\" This is my dad.","One day. mum and dad went out.\nI sat in front of the door for a whole day.", "That night, mum's back. Where's dad?\nI wished he can play with me before going to bed.", "\"Daddy's sick, he'll be home next week,\ndon't worry.\" Mum said \"Woof!\"", "Every day I sat in front of the door,\nwaiting to give him a big welcome. But, he hadn't come back.", "\"Hello? Yes..\" Mum cried.\nA bad call from the hospital.", "I was able to sense the atmosphere,\nI know I should look for him.", "\"Stewie, Where are you going?\"\nI ran out the door. I must look for him.", "I searched for several weeks..\nwithout eating and... Couldn't move with this body...", "" 
	};
	#endregion


    public Animator animator;

    void Start()
    {
        Story.text = "";
        ps = transform.Find("MultiplyerCollectParticles").GetComponent<ParticleSystem>();
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerScript = player.GetComponent<PlayerController>();
        ps.Stop();

        animator = GetComponent<Animator>();

    }

    public virtual void OnInteractAnimation(Animator animator)
    {
        animator.SetTrigger("tr_pickup");
    }
    void Update() {
       
       
        if (!crystallist[cindex].activeSelf) {
            Sen();
            cindex++;
            
        }
        if (Story.text != "")
        {
            Clear();


        }
        else {  Timer = 0;}
        

    }

    void Clear() {
        
        Timer += (int)(Time.deltaTime * 10) + 1;
        if (Timer > 100) { Story.text = ""; }
    }

    public virtual bool CanInteract(Collider other)
    {
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject == player) {
            
            ps.Play();
            AudioSource.PlayClipAtPoint(crystalTrigger, transform.position);

        }
    }

    private void OnTriggerExit(Collider other)
    {
       
        if (other.gameObject == player){
            
            ps.Play();
            playerScript.CollectCrystal(crystalvalue);
            AudioSource.PlayClipAtPoint(crystalDestory, transform.position);
            animator.SetTrigger("Destory");
        }
    }
    
   void Sen() {
       
        if (index < sentence.Length)
        {

            Story.text = sentence[index];
            Story.text = sentence[index];
            index++;
        }

    
    }
    public void DestoryCrystal()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    

}
