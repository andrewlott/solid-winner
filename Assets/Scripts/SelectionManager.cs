using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : Singleton<SelectionManager> {
	public GameObject characterSelectScreen;

	void Awake() {
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetupMode() {

	}

	public void Butt() {
		Debug.Log ("GOT EEM");
	}

	public void FadeInSelection() {
		this.characterSelectScreen.SetActive (true);
		// add animation from lerp
	}
}
