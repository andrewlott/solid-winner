using UnityEngine;
using System.Collections.Generic;

public class BoardManager : Singleton<BoardManager> {
	protected BoardManager () {}

	[SerializeField]
	public RectTransform boardTransform;
	[SerializeField]
	public RectTransform visibleBoardTransform;
	[SerializeField]
	private GameObject slotPrefab;

	[SerializeField]
	private int boardWidth = 6;
	[SerializeField]
	private int maxBoardHeight = 9;
	[SerializeField] [Range(1, 100)]
	private int speed = 10;

	private List<TileRow> slots = new List<TileRow> ();
	private float lastUpdateTime;
	public float movementIncrement = 0.005f;
	public int rowCount;

	private int totalMovements = 0;

	private Tile swapTile1;
	private Tile swapTile2;

	void Awake() {
		DontDestroyOnLoad (this.gameObject);
		Initialize ();
	}

	public TileRow TileRowAboveTileRow(TileRow row) {
		int index = (row.index + BoardManager.Instance.rowCount + 1) % BoardManager.Instance.rowCount;
		return this.slots [index];
	}

	public TileRow TileRowBelowTileRow(TileRow row) {
		int index = (row.index + BoardManager.Instance.rowCount - 1) % BoardManager.Instance.rowCount;
		return this.slots [index];
	}

	private void Initialize() {
		this.Reset ();
		this.CreateInitialBoard ();
	}

	private void Reset() {
	}

	private int CalculateM() {
		return (int)(this.boardTransform.rect.height / movementIncrement);
	}

	private void CreateInitialBoard() {
		this.slots.Clear ();

		float increment = this.boardTransform.rect.width / boardWidth;
		this.rowCount = (int)(this.boardTransform.rect.height / increment);

		for (int i = 0; i < this.rowCount; i++) {
			TileRow p = TileRow.CreateTileRow ();
			p.UpdateRowPosition (this.totalMovements, this.CalculateM ());
//			for (int j = 0; j < this.boardWidth; j++) {
//				GameObject g = Instantiate (this.slotPrefab);
//				p.AddTileAtPosition (j, g);
//			}
			this.slots.Add (p);
		}
	}

	void Update() {
		if (Time.time > this.lastUpdateTime + 1.0f / this.speed) {
			this.lastUpdateTime = Time.time;
			this.totalMovements = (this.totalMovements + 1) % this.CalculateM();

			foreach (var row in this.slots) {
				row.UpdateRowPosition (this.totalMovements, this.CalculateM ());
			}
		}

		if (Input.GetMouseButtonDown(0)) {
			this.swapTile1 = Tile.selectedTile;
		} else if (Input.GetMouseButtonUp(0)) {
			this.SwapTiles (this.swapTile1, Tile.selectedTile);
		}
	}

	private void SwapTiles(Tile a, Tile b) {
		if (a == null || b == null || a == b) {
			return;
		}

		// check if neighbors
		if (!a.Neighbors ().Contains (b)) {
			return;
		}

		// swap
		Tile.Swap(a, b);

		this.swapTile1 = null;
		this.swapTile2 = null;
	}
}
