using UnityEngine;
using System.Collections.Generic;

public class Tile : MonoBehaviour {
	public static float tileWidth = 2.76f;

	public static int rowWidth = 6;

	public enum Type {
		None,
		Earth,
		Fire,
		Wind,
		Water,
		Heart,
		MaxType
	};
		
	public static List<Tile> allTiles = new List<Tile> ();

	public List<Sprite> icons;
	public List<Sprite> backgrounds;
	public Type type;
	public SpriteRenderer background;
	public SpriteRenderer icon;

	public static Tile CreateTile(Vector3 position) {
		Tile tile = null;
		GameObject gameObject = Instantiate(Resources.Load("Prefabs/Tile")) as GameObject;
		tile = gameObject.GetComponent<Tile> ();
		Vector3 tilePosition = position;
		tile.transform.position = tilePosition;
		tile.type = ((Type)Random.Range(1, (int)Type.MaxType));
		tile.icon.sprite = tile.icons[(int)tile.type];


		if (tile != null) {
//			tile.isInteractable = true;
		}

		allTiles.Add (tile);
		return tile;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static Tile firstTile;
	public static Tile secondTile;
	void OnMouseDown() {
		if (firstTile == null) {
			firstTile = this;
		} else if (secondTile == null && this.isHorizontal (firstTile)) {
			secondTile = this;

			Vector3 firstPosition = firstTile.gameObject.transform.position;
			Vector3 secondPosition = secondTile.gameObject.transform.position;
			firstTile.gameObject.transform.position = secondPosition;
			secondTile.gameObject.transform.position = firstPosition;

			int firstIndex = allTiles.IndexOf (firstTile);
			int secondIndex = allTiles.IndexOf (secondTile);
			allTiles [firstIndex] = secondTile;
			allTiles [secondIndex] = firstTile;

//			firstTile = null;
			secondTile = null;

			// swap occurred
			BoardManager.Instance.SwapPerformed();
		} else if (this == firstTile) {
			firstTile = null;
		} else {
			firstTile = this;
		}

		for (int i = 0; i < allTiles.Count; i++) {
			allTiles [i].background.sprite = backgrounds [allTiles [i] == firstTile ? 1 : 0];
		}
	}

	public bool isHorizontal(Tile otherTile) {
		int myIndex = allTiles.IndexOf (this);
		int x = myIndex % rowWidth;
		int y = myIndex / rowWidth;
		Debug.LogFormat ("First: {0}, {1}", x, y);
		int otherIndex = allTiles.IndexOf (otherTile);
		int otherX = otherIndex % rowWidth;
		int otherY = otherIndex / rowWidth;
		Debug.LogFormat ("Second: {0}, {1}", otherX, otherY);

		Tile leftNeighbor = null;
		Tile rightNeighbor = null;
		if (x - 1 >= 0) {
			leftNeighbor = allTiles [myIndex - 1];
		}
		if (x + 1 < rowWidth) {
			rightNeighbor = allTiles [myIndex + 1];
		}

		return leftNeighbor == otherTile || rightNeighbor == otherTile;
	}

	public bool isAdjacent(Tile otherTile) {
		int myIndex = allTiles.IndexOf (this);
		int x = myIndex % rowWidth;
		int y = myIndex / rowWidth;
		Debug.LogFormat ("First: {0}, {1}", x, y);
		int otherIndex = allTiles.IndexOf (otherTile);
		int otherX = otherIndex % rowWidth;
		int otherY = otherIndex / rowWidth;
		Debug.LogFormat ("Second: {0}, {1}", otherX, otherY);

		Tile topNeighbor = null;
		Tile bottomNeighbor = null;
		Tile leftNeighbor = null;
		Tile rightNeighbor = null;
		if (x - 1 >= 0) {
			leftNeighbor = allTiles [myIndex - 1];
		}
		if (x + 1 < rowWidth) {
			rightNeighbor = allTiles [myIndex + 1];
		}
		if ((y + 1) * rowWidth < allTiles.Count) {
			topNeighbor = allTiles [myIndex + rowWidth];
		}
		if ((y - 1) * rowWidth >= 0) {
			bottomNeighbor = allTiles [myIndex - rowWidth];
		}
		return leftNeighbor == otherTile || rightNeighbor == otherTile || topNeighbor == otherTile || bottomNeighbor == otherTile;
	}
}
