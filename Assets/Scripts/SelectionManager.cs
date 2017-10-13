using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character {
	Random = 0,
	Min = 1,
	Cat = 1,
	Fox = 2,
	Whale = 3,
	Lizard = 4,
	Penguin = 5,
	Max = 6
};

public class SelectionManager : Singleton<SelectionManager> {

	public SpriteRenderer myCharacterSpriteRenderer;
	public SpriteRenderer theirCharacterSpriteRenderer;

	public List<GameObject> portraits; // index is same as Character

	private Character _selectedCharacter = Character.Random;

	void Awake() {
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		SetupCharacter(_selectedCharacter);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	#region Versus Area

	private void SetupCharacter(Character c) {
		int i = (int)_selectedCharacter;
		GameObject portrait = portraits[i];
		GameObject frame = portrait.transform.Find("PortraitHighlight").gameObject;
		frame.SetActive(false);

		_selectedCharacter = c;
		i = (int)_selectedCharacter;
		portrait = portraits[i];
		frame = portrait.transform.Find("PortraitHighlight").gameObject;
		frame.SetActive(true);

		// setup sprite
	}

	#endregion
		
	#region Button Input

	public void ReadyButtonPressed() {
		// gate by ready for opponent

		// if random, choose character, maybe have animation
		if (_selectedCharacter == Character.Random) {
			Character randomCharacter = (Character)Random.Range((int)Character.Min, (int)Character.Max);
			SetupCharacter(randomCharacter);
		}

		PlayerPrefs.SetInt("selectedCharacter", (int)_selectedCharacter);
		ScreenManager.Instance.MoveRight();
	}

	public void CharacterSelected(int i) {
		Character c = (Character) i;
		SetupCharacter(c);
	}

	public void BackButtonPressed() {
		ScreenManager.Instance.MoveLeft();
	}

	#endregion
}
