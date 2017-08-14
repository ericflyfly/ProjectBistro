using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setup02Script : MonoBehaviour {

	public Button eraseButton;
	private Color initialEraseColor;

	void Start(){
		initialEraseColor = eraseButton.colors.normalColor;
		if (GameManager.eraseMode == true) {
			eraseButton.GetComponent<Image>().color = Color.red;
		}
	}

	public void ChangeEraseMode (){
		if (GameManager.eraseMode == false) {
			GameManager.SetEraseMode (true);
			eraseButton.GetComponent<Image>().color = Color.red;
		} else if (GameManager.eraseMode == true) {
			GameManager.SetEraseMode (false);
			eraseButton.GetComponent<Image>().color = initialEraseColor;
		}
	}
}
