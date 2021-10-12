using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelChanger : MonoBehaviour {

    // Use this for initialization
    public Animator animator;
    private int levelToload;


    void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void FadeToLevel(int levelIndex)
    {
        levelToload = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void GameFinished()
    {
        animator.SetTrigger("gameFinished");
    }

    public void blurScreen(bool trigger)
    {
        if (trigger){
            animator.SetBool("blur", true);
        }
        else
        {
            animator.SetBool("blur", false);
        }
        
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToload);
    }
}
