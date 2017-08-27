using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GuessController : MonoBehaviour {

	public string[] guesses;
	public GameObject guess;
	public float buttonSize;
	public GameObject content;

	private GameObject[] guessButtons;

	void Awake() {
		content.GetComponent<RectTransform> ().offsetMin = new Vector2(content.GetComponent<RectTransform> ().offsetMin.x, -guesses.Length * buttonSize);
		guessButtons = new GameObject[guesses.Length];
		float top = 0.0f;
		float bottom = content.GetComponent<RectTransform> ().rect.height - buttonSize;
		for (int i = 0; i < guesses.Length; i++) {
			
			guessButtons [i] = Object.Instantiate (guess, content.transform, false);
			Text text = guessButtons[i].transform.GetChild(0).GetComponent<Text> ();
			text.text = guesses [i];
			RectTransform rectTransform = guessButtons[i].GetComponent<RectTransform> ();
			Debug.Log ("offsetMin before: " + rectTransform.offsetMin); // .y == bottom == 270
			Debug.Log ("offsetMax before: " + rectTransform.offsetMax); // .y == top == 0
			rectTransform.offsetMin = new Vector2 (rectTransform.offsetMin.x, bottom);
			rectTransform.offsetMax = new Vector2 (rectTransform.offsetMax.x, -top);
			rectTransform = guessButtons[i].GetComponent<RectTransform> ();
			Debug.Log ("offsetMin after: " + rectTransform.offsetMin);
			Debug.Log ("offsetMax after: " + rectTransform.offsetMax);

			top += buttonSize;
			bottom -= buttonSize;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
