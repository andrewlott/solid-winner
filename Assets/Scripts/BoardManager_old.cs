using UnityEngine;
using System.Collections.Generic;
/*
public class BoardManager : Singleton<BoardManager> {
	protected BoardManager () {}

	[SerializeField]
	private RectTransform boardTransform;
	[SerializeField]
	private GameObject slotPrefab;
	[SerializeField]
	private GameObject tilePrefab;


	private List<Slot>slots;
	private List<Tile>tiles;

//	public static int BoardWidth = 5;
//	public static Vector2 BoardCenter = new Vector2 (0.0f, -3.34f);

//	public List<Tile> tiles;

	// Dragging n shit
//	public float dragSpeed = 1;
//	private Vector3 dragOrigin;
//	private Vector3 lastDragPoint;
//
//	public float tileMoveRate = 0.05f;
//	public float tileMoveFrequency = 0.5f;
//	public float lastMoveTime;
//	public float lastRowCreateTime = 0;
//
	void Awake() {
		DontDestroyOnLoad (this.gameObject);
		Initialize ();
	}

	private void Initialize() {
		this.Reset ();
		this.CreateInitialBoard ();
	}

	private void Reset() {
		this.slots.Clear ();
		this.tiles.Clear ();
	}

	private void CreateInitialBoard() {

	}

//	private void Initialize() {
//		CreateInitialBoard ();
//		this.lastMoveTime = Time.time;
//	}
//
//	private float XPositionForColumn(int columnIndex) {
//		return BoardCenter.x + (Tile.tileWidth + Tile.xDelta) * Tile.RenderScale * (columnIndex - (BoardWidth / 2));
//	}
//
//	private float YPositionForRow(int rowIndex) {
//		return BoardCenter.y + (Tile.tileWidth + Tile.yDelta) * Tile.RenderScale * (rowIndex - (BoardWidth / 2));
//	}
//
//
//	public void CreateInitialBoard() {
//		for (int i = -2; i < BoardWidth; i++) {
//			this.CreateTileRow (i);
//		}
//	}

		
//	void Update() {
//		float scroll = Input.GetAxis("Mouse ScrollWheel");
//		Camera.main.orthographicSize += scroll;
//
//		HandleDrag ();
//	}

//	void FixedUpdate() {
//		// Movement
//		float now = Time.time;
//		HandleTriggers (now);
//
//		if (now - this.lastMoveTime >= tileMoveFrequency) {
//			this.lastMoveTime = now;
//			Vector3 displacement = new Vector3 (0.0f, tileMoveRate, 0.0f);
//			this.ApplyToTiles ((Tile tile) => {
//				tile.transform.position += displacement;
//			});
//		}
//
//
//		HandleMatches ();
//	}

//	private void HandleDrag() {
//		if (Input.GetMouseButtonDown(0))
//		{
//			dragOrigin = Input.mousePosition;
//			lastDragPoint = dragOrigin;
//			return;
//		}
//
//		if (!Input.GetMouseButton(0)) return;
//
//		Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - lastDragPoint);
//		Vector3 move = Quaternion.Euler(0, 45, 0) * new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);
//		transform.Translate(-move, Space.World);  
//	}

//	private void CreateTileRow(int index) {
//		for (int j = 0; j < BoardWidth; j++) {
//			Vector3 position = new Vector3 (this.XPositionForColumn(j), this.YPositionForRow(index), 0.0f);
//			this.tiles.Add (Tile.CreateTile (position, index >= 0));
//		}
//	}

//	private void HandleTriggers(float now) {
//		float diff = now - this.lastRowCreateTime;
//		if (this.tileMoveRate * (now - this.lastRowCreateTime) / this.tileMoveFrequency >= (Tile.tileWidth + Tile.yDelta) * Tile.RenderScale) {
//			Debug.Log (this.tileMoveRate * (now - this.lastRowCreateTime) / this.tileMoveFrequency);
//			Debug.Log ((Tile.tileWidth + Tile.yDelta) * Tile.RenderScale);
//			this.lastRowCreateTime = now;
//			this.ApplyToTiles ((Tile tile) => {
//				tile.ToggleActive(true);
//			});
//			this.CreateTileRow (-2);
//		}
//	}

//	private void HandleMatches() {
//		HashSet<Tile> matchedTiles = new HashSet<Tile> ();
//		foreach (Tile tile in this.tiles) {
//			List<Tile> matchedNeighbors = tile.MatchedNeighbors ();
//			foreach (Tile matchedTile in matchedNeighbors) {
//				matchedTiles.Add (matchedTile);
//			}
//		}
//
//		foreach(Tile tile in matchedTiles) {
////			this.tiles.Remove (tile);
//			// falling
//		}
//	}

//	public void ApplyToTiles(System.Action<Tile> actionToApply) {
//		foreach (Tile tile in this.tiles) {
//			actionToApply (tile);
//		}
//	}

}
*/