using UnityEngine;
using System.Collections.Generic;

public class TileRow : MonoBehaviour {
	private static int capacity = 6;
	private static List<TileRow> tileRows = new List<TileRow> ();

	private RectTransform boardTransform;
	private int index;
	private float width;
	private float height;
	private GameObject[] tiles = new GameObject[capacity];

	public static TileRow CreateTileRow(RectTransform boardTransform) {
		GameObject gameObject = Instantiate(Resources.Load("Prefabs/TileRow")) as GameObject;
		TileRow tileRow = gameObject.GetComponent<TileRow> ();
		tileRow.boardTransform = boardTransform;
		tileRow.index = tileRows.Count;

		tileRows.Add (tileRow);
		return tileRow;
	}

	public void AddTileAtPosition(int index, GameObject tile) {
		tile.transform.parent = this.transform;
		tile.transform.localPosition = new Vector3 (this.TileXPositionForIndex(index), 0.0f, 0.0f);
		this.tiles [index] = tile;
	}

	public float TileXPositionForIndex(int index) {
		float increment = this.boardTransform.rect.width / capacity;
		return this.boardTransform.rect.xMin + index * increment + increment / 2;
	}

	public void UpdateRowPosition(int n, int m) {
		float increment = this.boardTransform.rect.width / capacity;
		float yPos = this.boardTransform.rect.y - this.boardTransform.rect.height / 2 + increment * 2 + (((this.index * increment/BoardManager.Instance.movementIncrement) + n) % m) * BoardManager.Instance.movementIncrement ;
		this.transform.position = new Vector3 (0.0f, yPos, 0.0f);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
