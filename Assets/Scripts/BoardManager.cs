using UnityEngine;
using System.Collections.Generic;

public class BoardManager : Singleton<BoardManager> {
	protected BoardManager () {}

	public static int BoardWidth = 6;
	public static Vector2 InactiveTileVelocity = new Vector2 (0.0f, 0.5f);
	public static float InactiveTileMass = 1000000.0f;

	public List<Tile> inactiveTiles;
	public List<Tile> activeTiles;

	void Awake() {
		DontDestroyOnLoad (gameObject);
		Initialize ();
	}

	void Initialize() {
		CreateInitialBoard ();
		this.HandleMatches ();
	}

	private float XPositionForColumn(int columnIndex) {
		float xDelta = 0.5f;
		return (Tile.tileWidth + xDelta) * columnIndex;
	}

	private float YPositionForRow(int rowIndex) {
		float yDelta = 0.25f;
		return (Tile.tileWidth + yDelta) * rowIndex;
	}

	public void CreateInitialBoard(int width = 6, int height = 6) {
		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++) {
				Vector3 position = new Vector3 (this.XPositionForColumn(j), this.YPositionForRow(i), 0.0f);
				this.activeTiles.Add (Tile.CreateTile (position));
			}
		}

		int inactiveHeight = 2;
		for (int i = 0; i < inactiveHeight; i++) {
			for (int j = 0; j < width; j++) {
				Vector3 position = new Vector3 (this.XPositionForColumn(j), -this.YPositionForRow(i + 1), 0.0f);
				Tile tile = Tile.CreateTile (position);
				tile.GetComponent<Rigidbody2D> ().isKinematic = true;
//				tile.GetComponent<Rigidbody2D> ().velocity = InactiveTileVelocity;
//				tile.GetComponent<Rigidbody2D> ().mass = InactiveTileMass;

				this.inactiveTiles.Add (tile);
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}

	public void SwapPerformed() {
		// handle states
		// check for matches
		this.HandleMatches();
	}

	private void HandleMatches() {
		foreach (Tile tile in Tile.allTiles) {
			int myIndex = Tile.allTiles.IndexOf (tile);
			int x = myIndex % Tile.rowWidth;
			int y = myIndex / Tile.rowWidth;

			Tile topNeighbor = null;
			Tile bottomNeighbor = null;
			Tile leftNeighbor = null;
			Tile rightNeighbor = null;
			if (x - 1 >= 0) {
				leftNeighbor = Tile.allTiles [myIndex - 1];
			}
			if (x + 1 < Tile.rowWidth) {
				rightNeighbor = Tile.allTiles [myIndex + 1];
			}
			if ((y + 1) * Tile.rowWidth < Tile.allTiles.Count) {
				topNeighbor = Tile.allTiles [myIndex + Tile.rowWidth];
			}
			if ((y - 1) * Tile.rowWidth >= 0) {
				bottomNeighbor = Tile.allTiles [myIndex - Tile.rowWidth];
			}

			if (leftNeighbor != null && leftNeighbor.type == tile.type && rightNeighbor != null && rightNeighbor.type == tile.type) {
				tile.gameObject.SetActive (false);
				leftNeighbor.gameObject.SetActive (false);
				rightNeighbor.gameObject.SetActive (false);
			} else if (topNeighbor != null && topNeighbor.type == tile.type && bottomNeighbor != null && bottomNeighbor.type == tile.type) {
				tile.gameObject.SetActive (false);
				topNeighbor.gameObject.SetActive (false);
				bottomNeighbor.gameObject.SetActive (false);
			}
		}
	}

	public float dragSpeed = 1;
	private Vector3 dragOrigin;
	private Vector3 lastDragPoint;
	void Update() {
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		Camera.main.orthographicSize += scroll;

		HandleDrag ();
		this.HandleInactiveTiles ();
	}

	private void HandleInactiveTiles() {
		if (this.inactiveTiles.Count < 1) {
			return;
		}

		if (this.inactiveTiles [0].transform.position.y > -0.625) {
			List<Tile> poppedTiles = this.inactiveTiles.GetRange (0, BoardWidth);
			this.inactiveTiles.RemoveRange (0, BoardWidth);
			foreach (Tile tile in poppedTiles) {
				tile.GetComponent<Rigidbody2D> ().isKinematic = false;
				tile.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				tile.GetComponent<Rigidbody2D> ().mass = 1.0f;

			}
			this.activeTiles.AddRange(poppedTiles);

			int startingHeight = this.inactiveTiles.Count / BoardWidth;
			int inactiveHeight = 1;
			for (int i = startingHeight; i < startingHeight + inactiveHeight; i++) {
				for (int j = 0; j < BoardWidth; j++) {
					Vector3 position = new Vector3 (this.XPositionForColumn(j), -this.YPositionForRow(i + 1), 0.0f);
					Tile tile = Tile.CreateTile (position);
//					tile.GetComponent<Rigidbody2D> ().mass = InactiveTileMass;
//					tile.GetComponent<Rigidbody2D> ().velocity = InactiveTileVelocity;
					tile.GetComponent<Rigidbody2D> ().isKinematic = true;
					this.inactiveTiles.Add (tile);
				}
			}
		}

		foreach (Tile tile in this.inactiveTiles) {
			tile.gameObject.transform.position += new Vector3 (0.0f, 0.01f, 0.0f);
		}
	}

	private void HandleDrag() {
		if (Input.GetMouseButtonDown(0))
		{
			dragOrigin = Input.mousePosition;
			lastDragPoint = dragOrigin;
			return;
		}

		if (!Input.GetMouseButton(0)) return;

		Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - lastDragPoint);
		//		lastDragPoint = Input.mousePosition;
		Vector3 move = Quaternion.Euler(0, 45, 0) * new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);
		// TODO: do this with velocity
		transform.Translate(-move, Space.World);  
	}
}
