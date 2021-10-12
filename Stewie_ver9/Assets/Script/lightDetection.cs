using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightDetection : MonoBehaviour
{

    #region Basic Audio and collider Variable
    private GameObject player;
    public GameObject lamb;
    public AudioClip magic;
    public float timer = 0.0f;
    public int seconds;
    private AudioSource soundeffect;
    #endregion

    #region Private Members
    private bool isboostSpirit = false;
    #endregion

    #region Public Members
    public float boostRepeatRate = 0.1f;
    public int boostAmount = 1;
    public bool Repeating = true;
    #endregion

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        soundeffect = GetComponent<AudioSource>();
        soundeffect.enabled = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player||other.name == "puppeeeeth")
        {
            soundeffect.enabled = true;
            soundeffect.PlayOneShot(magic);
            isboostSpirit = true;
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (Repeating)
            {
                // Repeating damage
                StartCoroutine(Takeboost(player, boostRepeatRate));
            }
            else
            {
                // Just one time damage
                player.Boostsprite(boostAmount);
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        // If the colliding gameobject is the player...
        if (other.gameObject == player || other.name == "puppeeeeth")
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                isboostSpirit = false;
            }
        }

    }

    IEnumerator Takeboost(PlayerController player, float repeatRate)
    {
        while (isboostSpirit)
        {
            player.Boostsprite(boostAmount);
            Takeboost(player, repeatRate);
            if (player.IsDead)
                isboostSpirit = false;
            yield return new WaitForSeconds(repeatRate);
        }
    }
}
