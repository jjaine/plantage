using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour {

	public bool help = false;
	public GameObject HelpPanel;
	public Button button;
	public Sprite helpButton;
	public Sprite exitButton;
	
	// Update is called once per frame
	void Update () {
		if(help){
			HelpPanel.SetActive(true);
			button.image.sprite = exitButton;
		} else {
			HelpPanel.SetActive(false);
			button.image.sprite = helpButton;
		}
	}

	public void ActivateHelp(){
		if(!help)
			help=true;
		else
			help=false;
	}
}
