using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public GameObject MessagePanel;

	// Use this for initialization
	void Start () {
	}

    private bool isMessagePanelOpened = false;

    public bool IsMessagePanelOpened
    {
        get { return isMessagePanelOpened; }
    }

    public void OpenMessagePanel(crystals item)
    {
        Text messageText = MessagePanel.transform.Find("Text").GetComponent<Text>();
        messageText.text = item.InteractText;        
        isMessagePanelOpened = true;
    }

    public void OpenMessagePanel(string text)
    {
        MessagePanel.SetActive(true);

        Text messageText = MessagePanel.transform.Find("Text").GetComponent<Text>();
        messageText.text = text;
        isMessagePanelOpened = true;
    }

    public void CloseMessagePanel()
    {
        MessagePanel.SetActive(false);
        isMessagePanelOpened = false;
    }
}
