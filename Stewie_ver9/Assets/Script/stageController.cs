using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageController : MonoBehaviour {

    #region Private Members
    private AudioSource panicAudio; // Reference to the AudioSource of the panic msuic.
    private AudioSource finalSceneAudio;
    private AudioSource endSceneAudio;
    private AudioSource origianlAudio;
	#endregion
	#region Public Members
    public bool fsa = false;
    public bool pa = false;
    public bool esa = false;
	public float musicFadeSpeed = 1f;
	#endregion
    void Start () {

        origianlAudio = transform.Find("NormalMusic").GetComponent<AudioSource>();
        finalSceneAudio = transform.Find("FinalSceneMusic").GetComponent<AudioSource>();
        panicAudio = transform.Find("EscapeMusic").GetComponent<AudioSource>();
        endSceneAudio = transform.Find("EndSceneMusic").GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        MusicFading();
    }

    void MusicFading()
    {
        // If the alarm is not being triggered...
        if (fsa)
        {
            origianlAudio.volume = Mathf.Lerp(origianlAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
            finalSceneAudio.volume = Mathf.Lerp(finalSceneAudio.volume, 0.8f, musicFadeSpeed *Time.deltaTime);
        }
        else if (!fsa)
        {
            finalSceneAudio.volume = Mathf.Lerp(finalSceneAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
        }

        if (pa)
        {
            origianlAudio.volume = Mathf.Lerp(origianlAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
        }
        else if (!pa)
        {
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
        }

        if (esa)
        {
            origianlAudio.volume = Mathf.Lerp(origianlAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
            endSceneAudio.volume = Mathf.Lerp(endSceneAudio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
        }
        else if (!esa)
        {
            endSceneAudio.volume = Mathf.Lerp(endSceneAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
        }

        if (!fsa && !pa && !esa) { origianlAudio.volume = Mathf.Lerp(origianlAudio.volume, 0.5f, musicFadeSpeed * Time.deltaTime); }

    }

}

