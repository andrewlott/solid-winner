using UnityEngine;
using MonsterLove.StateMachine;

public class GameManager : Singleton<GameManager> {

	public SpriteRenderer backgroundHUD;
	public SpriteRenderer backgroundBoard;

	private static string BackgroundPath = "FeralPixel/Game Screen/Backgrounds/Game_Background_{0}";
	private static string BackgroundHUDName = "Game_Background_{0}_HUD";
	private static string BackgroundBoardName = "Game_Background_{0}_Board";


	public enum GameState {
		None,
		Start,
		Idle,
		Swapping,
		Matching,
		Falling,
		End
	};

	private StateMachine<GameState> stateMachine;

	void Awake() {
		stateMachine = StateMachine<GameState>.Initialize (this);
	}

	void OnEnable() {
		Setup();
	}

	void None_Enter() {
		// should not be here
	}


	#region Setup 
	public void Setup() {
		int characterId = PlayerPrefs.GetInt("selectedCharacter");
		if (characterId <= 0) {
			return;
		}

		string backgroundPath = string.Format(BackgroundPath, characterId);
		Sprite[] backgroundSprites = Resources.LoadAll<Sprite>(backgroundPath);
		backgroundHUD.sprite = FindSpriteInSpriteSheetByName(backgroundSprites, string.Format(BackgroundHUDName, characterId));
		backgroundBoard.sprite = FindSpriteInSpriteSheetByName(backgroundSprites, string.Format(BackgroundBoardName, characterId));
	}

	private Sprite FindSpriteInSpriteSheetByName(Sprite[] sheet, string name) {
		foreach(Sprite s in sheet) {
			if (s.name == name) {
				return s;
			}
		}

		return null;
	}

	#endregion


	#region Button Input

	#endregion
}

// all tiles on update move up at a certain rate
// (time - starttime) % rate == 0, move up. (if not falling optional)

// one hero per color of tile, choose set of colors to be like 5 including theirs always, collecting that color charges power
// maybe collecting opposing color clears blocks or something

// characters:
// green: green lizard wizard w/ monocle or glasses
// pink: pink pig witch
// black: black sheep sorcerer
// blue: shark/fish mage
// red: orangutan enchantress
// yellow: yellow canary caster
// orange: tiger necromancer
// grey: white wolf warlock

// weakness strength against other color