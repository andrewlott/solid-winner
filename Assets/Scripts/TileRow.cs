﻿using UnityEngine;
using System.Collections.Generic;

public class TileRow : MonoBehaviour {
	private static int capacity = 6;
	private static List<TileRow> tileRows = new List<TileRow> ();

	private int index;
	private float width;
	private float height;
	private Tile[] tiles = new Tile[capacity];

	public static TileRow CreateTileRow() {
		GameObject gameObject = Instantiate(Resources.Load("Prefabs/TileRow")) as GameObject;
		TileRow tileRow = gameObject.GetComponent<TileRow> ();
		tileRow.index = tileRows.Count;

		tileRows.Add (tileRow);
		return tileRow;
	}

	public void AddTileAtPosition(int index, Tile tile) {
		tile.gameObject.transform.parent = this.transform;
		tile.gameObject.transform.localPosition = new Vector3 (this.TileXPositionForIndex(index), 0.0f, 0.0f);
		this.tiles [index] = tile;
	}

	public float TileXPositionForIndex(int index) {
		float increment = BoardManager.Instance.boardTransform.rect.width / capacity;
		return BoardManager.Instance.boardTransform.rect.xMin + index * increment + increment / 2;
	}

	public void UpdateRowPosition(int n, int m) {
		float increment = BoardManager.Instance.boardTransform.rect.width / capacity;
		int crazyMod = (int)((this.index * increment/BoardManager.Instance.movementIncrement) + n) % m;
		float yPos = BoardManager.Instance.boardTransform.rect.y - BoardManager.Instance.boardTransform.rect.height / 2 + increment * 2.5f + crazyMod * BoardManager.Instance.movementIncrement ;
		this.transform.position = new Vector3 (0.0f, yPos, 0.0f);

		if (crazyMod == 0) {
			this.Fill ();
		}
	}

	public static TileRow BottomRow() {
		if (tileRows.Count == 0) {
			return null;
		}

		TileRow lowestRow = tileRows[0];
		foreach (var row in tileRows) {
			if (row.transform.position.y < lowestRow.transform.position.y) {
				lowestRow = row;
			}
		}

		return lowestRow;
	}

	public static TileRow TopRow() {
		if (tileRows.Count == 0) {
			return null;
		}

		TileRow highestRow = tileRows[0];
		foreach (var row in tileRows) {
			if (row.transform.position.y > highestRow.transform.position.y) {
				highestRow = row;
			}
		}

		return highestRow;
	}

	public void Fill() {
		for (int i = 0; i < capacity; i++) {
			Tile tile = Tile.CreateTile ();
			this.AddTileAtPosition (i, tile);
		}
	}

	public bool IsVisible() {
		// does not account for height
		return RectTransformUtility.RectangleContainsScreenPoint (BoardManager.Instance.visibleBoardTransform, Camera.main.WorldToScreenPoint (this.gameObject.transform.position));
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
