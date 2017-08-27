using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ButtonTextPostController : MonoBehaviour {
	//public GameObject guessScrollView;

	public void onClick() {
		Debug.Log ("ButtonTextPostController.onClick");
		//guessScrollView.GetComponent<GuessController>().onClick(gameObject.GetComponent<Text> ().text);
		GameObject.Find("Guess Scroll View").GetComponent<GuessController>().onClick(gameObject.GetComponent<Text>().text);
	}
}
