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

	private int totalMovements = 0;

	void Awake() {
		DontDestroyOnLoad (this.gameObject);
		Initialize ();
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
		int rowCount = (int)(this.boardTransform.rect.height / increment);
		for (int i = 0; i < rowCount; i++) {
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
	}
}
