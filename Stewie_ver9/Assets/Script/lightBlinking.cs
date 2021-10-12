using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightBlinking : MonoBehaviour {

	#region Public Members
    public float fadeSpeed = 0.8f; //How fast the light fades between intensities.
    public float highIntensity = 1.5f;//The maximum intensity of the light whilst the alarm is on.
    public float lowIntensity = 0.7f;//The minimum intensity of the light whilst the alarm is on.
    public float changeMargin = 1.5f; // The margin within which the target intensity is changed.
    #endregion
	
	#region Private Members
	private float targetIntensity; //The intensity that the light is aiming for currently.
    #endregion
	
	void Awake()
    {
        // When the level starts we want the light to be "off".
        GetComponent<Light>().intensity = 0f;
        // When the alarm starts for the first time, the light should aim to have the maximum intensity.
        targetIntensity = highIntensity;
    }
    void Update()
    {
        // If the light is on...
        // ... Lerp the light's intensity towards the current target.
        GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity,
        targetIntensity, fadeSpeed * Time.deltaTime);
        // Check whether the target intensity needs changing and change it if so.
        CheckTargetIntensity();
    }
    void CheckTargetIntensity()
    {
        // If the difference between the target and current intensities is less than the change margin...
        if (Mathf.Abs(targetIntensity - GetComponent<Light>().intensity) < changeMargin)
        {
            // ... if the target intensity is high...
            if (targetIntensity == highIntensity)
                // ... then set the target to low.
                targetIntensity = lowIntensity;
            else
                // Otherwise set the targer to high.
                targetIntensity = highIntensity;
        }
    }
}
