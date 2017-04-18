using UnityEngine;
using System.Collections.Generic;

public class BoardManager : Singleton<BoardManager> {
	protected BoardManager () {}

	[SerializeField]
	private RectTransform boardTransform;
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

		for (int i = 0; i < 12; i++) {
			TileRow p = TileRow.CreateTileRow (this.boardTransform);
			p.UpdateRowPosition (this.totalMovements, this.CalculateM ());
//			GameObject p = new GameObject ();
//			p.transform.position = new Vector3 (0.0f, this.boardTransform.rect.y - this.boardTransform.rect.height / 2 + i * increment + increment / 2, 0.0f);
			for (int j = 0; j < this.boardWidth; j++) {
				GameObject g = Instantiate (this.slotPrefab);
				p.AddTileAtPosition (j, g);
//				g.transform.parent = p.transform;
//				g.transform.localPosition = new Vector3 (this.boardTransform.rect.xMin + j * increment + increment / 2, 0.0f, 0.0f);
			}
			this.slots.Add (p);
		}
	}

	void Update() {
		if (Time.time > this.lastUpdateTime + 1.0f / this.speed) {
			this.lastUpdateTime = Time.time;
			this.totalMovements = (this.totalMovements + 1) % this.CalculateM();

			foreach (var row in this.slots) {
				row.UpdateRowPosition (this.totalMovements, this.CalculateM ());
//				row.gameObject.transform.position += new Vector3(0.0f, movementIncrement, 0.0f);
			}
		}


//		float increment = this.boardTransform.rect.width / boardWidth;
//
//		for (int i = this.slots.Count - 1; i >= 0; i--) {
//			TileRow row = this.slots [i];
//			if (row.transform.position.y + increment / 2 >= 0.5f * this.boardTransform.rect.height + this.boardTransform.rect.y + increment * 2) {
//				GameObject.Destroy (row);
//				this.slots.RemoveAt (i);
//
//				Tile p = new GameObject ();
//
//				p.transform.position = new Vector3 (0.0f, this.boardTransform.rect.y - this.boardTransform.rect.height / 2 + increment / 2, 0.0f);
//				for (int j = 0; j < this.boardWidth; j++) {
//					GameObject g = Instantiate (this.slotPrefab);
//					g.transform.parent = p.transform;
//					g.transform.localPosition = new Vector3 (this.boardTransform.rect.xMin + j * increment + increment / 2, 0.0f, 0.0f);
//				}
//				this.slots.Insert (0, p);
//			}
//		}

		// need to use math to calculate row heights given time
		// keep track of number of bumps, then position is some formula based on start time / position and mod 
		// 
	}
}
