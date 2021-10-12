using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour {
	#region Public Members
    public Image ImgHealthBar;
    public Text TxtHealth;
    public int Min;
    public int Max;
	#endregion
	
	#region Private Members
    private int currentValue;
    private float currentPercent;
    #endregion

  public void SetValue(int health)
    {
        if(health != currentValue)
        {
            if(Max - Min == 0)
            {
                currentValue = 0;
                currentPercent = 0;
            }
            else
            {
                currentValue = health;
                currentPercent = (float)currentValue / (float)100;
            }
            TxtHealth.text = string.Format("{0} %", Mathf.RoundToInt(currentPercent * 100));
            ImgHealthBar.fillAmount = currentPercent;
        }
    }

    public float CurrentPercent
    {
        get { return currentPercent; }
    }

    public int CurrentValue
    {
        get { return currentValue;  }
    }

	// Use this for initialization
	void Start () {

	}

}
