using UnityEngine;
using System.Collections;
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

	public static Tile selectedTile;

	public TileRow myRow;

	private static float swapDuration = 0.1f;

	[SerializeField]
	private List<Sprite> icons;
	[SerializeField]
	private SpriteRenderer icon;

	private Type type;
	private bool isMoving;

	public static Tile CreateTile() {
		GameObject gameObject = Instantiate(Resources.Load("Prefabs/Tile")) as GameObject;
		Tile tile = gameObject.GetComponent<Tile> ();
		tile.type = ((Type)Random.Range(1, (int)Type.MaxType));
		tile.icon.sprite = tile.icons[(int)tile.type - 1];

		return tile;
	}

	public static void Swap(Tile a, Tile b) {
		a.StartCoroutine(a.MoveToPosition(b.myRow, b.myRow.IndexForTile(b)));
		b.StartCoroutine(b.MoveToPosition(a.myRow, a.myRow.IndexForTile(a)));
	}

	public List<Tile> Neighbors() {
		List<Tile> neighbors = new List<Tile> ();
		neighbors.AddRange (this.HorizontalNeighbors());
		neighbors.AddRange (this.VerticalNeighbors());
		return neighbors;
	}

	public Type MyType() {
		return this.type;
	}

	public List<Tile> HorizontalNeighbors() {
		List<Tile> neighbors = new List<Tile> ();
		int index = this.myRow.IndexForTile (this);
		if (index > 0 && this.myRow.TileAtIndex(index - 1) != null) {
			neighbors.Add(this.myRow.TileAtIndex(index - 1));
		}
		if (index < TileRow.capacity - 1 && this.myRow.TileAtIndex(index + 1) != null) {
			neighbors.Add(this.myRow.TileAtIndex(index + 1));
		}

		return neighbors;
	}

	public List<Tile> VerticalNeighbors() {
		List<Tile> neighbors = new List<Tile> ();
		int index = this.myRow.IndexForTile (this);
		TileRow aboveRow = BoardManager.Instance.TileRowAboveTileRow (this.myRow);
		Tile aboveTile = aboveRow.TileAtIndex (index);
		if (aboveTile != null) {
			neighbors.Add (aboveTile);
		}
		TileRow belowRow = BoardManager.Instance.TileRowBelowTileRow (this.myRow);
		Tile belowTile = belowRow.TileAtIndex (index);
		if (belowTile != null) {
			neighbors.Add (belowTile);
		}

		return neighbors;
	}

	private IEnumerator MoveToPosition(TileRow targetRow, int tileIndex) {
		float elapsedTime = 0;
		Vector3 startingPosition = this.transform.position;
		while (elapsedTime < swapDuration) {
			Vector3 target = new Vector3 (TileRow.TileXPositionForIndex(tileIndex), targetRow.transform.position.y, targetRow.transform.position.z);
			this.transform.position = Vector3.Lerp (startingPosition, target, (elapsedTime / swapDuration));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		targetRow.AddTileAtPosition (tileIndex, this);
	}

	void OnMouseOver() {
		if (Tile.selectedTile != this) {
			Tile.selectedTile = this;
		}
	}

	void OnMouseExit() {
		if (Tile.selectedTile == this) {
			Tile.selectedTile = null;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
