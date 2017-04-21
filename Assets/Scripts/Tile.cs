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
	public bool isCleared;
	public bool isMoving;


	private static float swapDuration = 0.1f;
	private static float settleDuration = 0.5f;

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

	public static void Swap(Tile a, Tile b) {
		a.StartCoroutine(a.MoveToPosition(b.myRow, b.myRow.IndexForTile(b)));
		b.StartCoroutine(b.MoveToPosition(a.myRow, a.myRow.IndexForTile(a)));
	}

	private IEnumerator MoveToPosition(TileRow targetRow, int tileIndex) {
		this.isMoving = true;
		float elapsedTime = 0;
		Vector3 startingPosition = this.transform.position;
		while (elapsedTime < swapDuration) {
			Vector3 target = new Vector3 (TileRow.TileXPositionForIndex(tileIndex), targetRow.transform.position.y, targetRow.transform.position.z);
			this.transform.position = Vector3.Lerp (startingPosition, target, (elapsedTime / swapDuration));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		targetRow.AddTileAtPosition (tileIndex, this);

		yield return new WaitForSeconds (settleDuration);
		this.isMoving = false;
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
		Tile leftNeighbor = this.LeftNeighbor ();
		Tile rightNeighbor = this.RightNeighbor ();
		if (leftNeighbor != null) {
			neighbors.Add (leftNeighbor);
		}
		if (rightNeighbor != null) {
			neighbors.Add (rightNeighbor);
		}

		return neighbors;
	}

	public List<Tile> VerticalNeighbors() {
		List<Tile> neighbors = new List<Tile> ();
		Tile topNeighbor = this.TopNeighbor ();
		Tile bottomNeighbor = this.BottomNeighbor ();
		if (topNeighbor != null) {
			neighbors.Add (topNeighbor);
		}
		if (bottomNeighbor != null) {
			neighbors.Add (bottomNeighbor);
		}

		return neighbors;
	}

	public Tile TopNeighbor() {
		int index = this.myRow.IndexForTile (this);
		TileRow aboveRow = BoardManager.Instance.TileRowAboveTileRow (this.myRow);
		Tile aboveTile = aboveRow.TileAtIndex (index);
		return aboveTile;
	}

	public Tile BottomNeighbor() {
		int index = this.myRow.IndexForTile (this);
		TileRow belowRow = BoardManager.Instance.TileRowBelowTileRow (this.myRow);
		Tile belowTile = belowRow.TileAtIndex (index);
		return belowTile;
	}

	public Tile LeftNeighbor() {
		int index = this.myRow.IndexForTile (this);
		if (index > 0 && this.myRow.TileAtIndex(index - 1) != null) {
			return this.myRow.TileAtIndex(index - 1);
		}

		return null;
	}

	public Tile RightNeighbor() {
		int index = this.myRow.IndexForTile (this);
		if (index < TileRow.capacity - 1 && this.myRow.TileAtIndex(index + 1) != null) {
			return this.myRow.TileAtIndex (index + 1);
		}

		return null;
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

	public void Clear() {
		this.icon.enabled = false;
		this.isCleared = true;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.isCleared) {
			return;
		}
		if (this.BottomNeighbor () != null && this.myRow != null && this.BottomNeighbor().myRow != null && this.myRow != TileRow.BottomRow() && this.BottomNeighbor().isCleared) {
			Tile.Swap (this, this.BottomNeighbor ());
		}
	}
}
