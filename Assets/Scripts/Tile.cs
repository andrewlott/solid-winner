using UnityEngine;
using System.Collections.Generic;

public class Tile : MonoBehaviour {
	public enum Type {
		None,
		Red,
		Yellow,
		Blue,
		Green,
		Teal,
		MaxType
	};

	[SerializeField]
	private List<Sprite> icons;
	[SerializeField]
	private SpriteRenderer icon;

	private Type type;


	public static Tile CreateTile() {
		GameObject gameObject = Instantiate(Resources.Load("Prefabs/Tile")) as GameObject;
		Tile tile = gameObject.GetComponent<Tile> ();
		tile.type = ((Type)Random.Range(1, (int)Type.MaxType));
		tile.icon.sprite = tile.icons[(int)tile.type - 1];

		return tile;
	}

	void OnMouseDown() {
		Debug.Log (this.type);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
