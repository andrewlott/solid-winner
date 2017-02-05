using UnityEngine;
using MonsterLove.StateMachine;

public class GameManager : Singleton<GameManager> {
	protected GameManager () {}

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

	void None_Enter() {
		// should not be here
	}
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