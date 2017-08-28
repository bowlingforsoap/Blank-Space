using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GuessController : MonoBehaviour {

	// Model in the scene.
	public GameObject model;
	public GameObject winText;

	public string[] guesses;
	public GameObject guessGameObject;
	public float buttonSize;
	public GameObject content;

	private GameObject[] guessButtons;

	void Awake() {
		content.GetComponent<RectTransform> ().offsetMin = new Vector2(content.GetComponent<RectTransform> ().offsetMin.x, -guesses.Length * buttonSize);
		guessButtons = new GameObject[guesses.Length];
		float top = 0.0f;
		float bottom = content.GetComponent<RectTransform> ().rect.height - buttonSize;
		for (int i = 0; i < guesses.Length; i++) {
			
			guessButtons [i] = Object.Instantiate (guessGameObject, content.transform, false);
			Text text = guessButtons[i].transform.GetChild(0).GetComponent<Text> ();
			text.text = guesses [i];
			RectTransform rectTransform = guessButtons[i].GetComponent<RectTransform> ();
			//Debug.Log ("offsetMin before: " + rectTransform.offsetMin); // .y == bottom == 270
			//Debug.Log ("offsetMax before: " + rectTransform.offsetMax); // .y == top == 0
			rectTransform.offsetMin = new Vector2 (rectTransform.offsetMin.x, bottom);
			rectTransform.offsetMax = new Vector2 (rectTransform.offsetMax.x, -top);
			rectTransform = guessButtons[i].GetComponent<RectTransform> ();
			//Debug.Log ("offsetMin after: " + rectTransform.offsetMin);
			//Debug.Log ("offsetMax after: " + rectTransform.offsetMax);

			top += buttonSize;
			bottom -= buttonSize;
		}
	}

	public void onClick(string buttonText) {
		for (int i = 0; i < model.transform.childCount; i++) {
			//Debug.Log ("checking model: " + model.transform.GetChild (i).name.ToLower ());
			//Debug.Log ("button text: " + buttonText.ToLower ());
			if (model.transform.GetChild (i).name.ToLower ().Equals (buttonText.ToLower ()) && model.transform.GetChild(i).gameObject.activeSelf) {
				//Debug.Log ("Win!");
				//winText.SetActive (true);

				ChooseNewModel (i);
				break;
			}
		}
	}

	private void ChooseNewModel(int prevModel) {
		model.transform.GetChild (prevModel).gameObject.SetActive (false);
		int nextModel;
		do {
			nextModel = (int)Random.value * (model.transform.childCount - 1);
		} while (nextModel == prevModel);
		model.transform.GetChild (nextModel).gameObject.SetActive (true);
	}

	void Update() {
		//Debug.Log (Input.GetAxis ("Scroll"));
	}

	public void OnScroll() {
		
	}

}
