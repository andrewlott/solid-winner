using UnityEngine;
using System.Collections.Generic;
/*
public class Tile : MonoBehaviour {
	public enum TileState {
		None,
		Idle,
		Falling
	}

	public enum Type {
		None,
		Earth,
		Fire,
		Wind,
		Water,
		Heart,
		MaxType
	};

	public static float tileWidth = 2.76f;
	public static float yDelta = 0.25f;
	public static float xDelta = 0.25f;

	public static int rowWidth = 5;
	public static float RenderScale = 0.75f;
		
	public List<Sprite> icons;
	public List<Sprite> backgrounds;
	public Type type;
	public SpriteRenderer background;
	public SpriteRenderer icon;

	private Tile leftNeighbor, rightNeighbor, topNeighbor, bottomNeighbor;

	public static Tile CreateTile(Vector3 position, bool active = true) {
		GameObject gameObject = Instantiate(Resources.Load("Prefabs/Tile")) as GameObject;
		Tile tile = gameObject.GetComponent<Tile> ();
		Vector3 tilePosition = position;
		tile.transform.position = tilePosition;
		tile.type = ((Type)Random.Range(1, (int)Type.MaxType));
		tile.icon.sprite = tile.icons[(int)tile.type];
		tile.gameObject.transform.localScale *= RenderScale;

		tile.ToggleActive (active);

		return tile;
	}

	void OnMouseDown() {
		Debug.Log (this.Neighbors ());
	}

//	void OnTriggerEnter2D(Collider2D collider) {
//		GameObject o = collider.gameObject;
//		if (o.transform.position.x < this.gameObject.transform.position.x) {
//			this.leftNeighbor = o.GetComponent<Tile> ();
//		} else if (o.transform.position.x > this.gameObject.transform.position.x) {
//			this.rightNeighbor = o.GetComponent<Tile> ();
//		} else if (o.transform.position.y < this.gameObject.transform.position.y) {
//			this.bottomNeighbor = o.GetComponent<Tile> ();
//		} else if (o.transform.position.y > this.gameObject.transform.position.y) {
//			this.topNeighbor = o.GetComponent<Tile> ();
//		}
//	}
		
	void OnTriggerExit2D(Collider2D collider) {
		GameObject o = collider.gameObject;
		if (o.transform.position.x < this.gameObject.transform.position.x) {
			this.leftNeighbor = null;
		} else if (o.transform.position.x > this.gameObject.transform.position.x) {
			this.rightNeighbor = null;
		} else if (o.transform.position.y < this.gameObject.transform.position.y) {
			this.bottomNeighbor = null;
		} else if (o.transform.position.y > this.gameObject.transform.position.y) {
			this.topNeighbor = null;
		}
	}

	void OnTriggerStay2D(Collider2D collider) {
		GameObject o = collider.gameObject;
		if (o.transform.position.x < this.gameObject.transform.position.x) {
			this.leftNeighbor = o.GetComponent<Tile> ();
		} else if (o.transform.position.x > this.gameObject.transform.position.x) {
			this.rightNeighbor = o.GetComponent<Tile> ();
		} else if (o.transform.position.y < this.gameObject.transform.position.y) {
			this.bottomNeighbor = o.GetComponent<Tile> ();
		} else if (o.transform.position.y > this.gameObject.transform.position.y) {
			this.topNeighbor = o.GetComponent<Tile> ();
		}
	}

	public void ToggleActive(bool active) {
		//this.gameObject.SetActive (active);
	}

	public List<Tile> MatchedNeighbors() {
		List<Tile> matched = new List<Tile> ();
		foreach (Tile tile in this.Neighbors()) {
			if (tile != null && tile.type == this.type) {
				matched.Add (tile);
			}
		}

		return matched;
	}

	public List<Tile> Neighbors() {
		return new List<Tile> () { this.leftNeighbor, this.rightNeighbor, this.topNeighbor, this.bottomNeighbor };
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
*/