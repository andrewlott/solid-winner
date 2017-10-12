﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenManager : Singleton<TitleScreenManager> {

	// have rank stats etc. here w/ getter and setter from client mem
	public GameObject titleScreen;
	// add lerp here

	void Awake() {
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		this.OnTestButtonClick ();
	}

	public void OnTestButtonClick() {
		Debug.Log ("TestButtonClicked!");
		// transition to next screen or show menu
		SelectionManager.Instance.FadeInSelection();
		this.titleScreen.SetActive (false);
	}
}
