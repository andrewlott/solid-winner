using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : Singleton<ScreenManager> {
	public List<GameObject> screens = new List<GameObject>();

	private int _index = 0;

	void Start() {
		MoveToCurrentIndex();
		// resize  and move all the transforms based on screenwidth
		// resize the masks
	}

	public void MoveLeft() {
		int index = Mathf.Max(0, _index - 1);
		if (index == _index) {
			return;
		}

		_index = index;
		MoveToCurrentIndex();
	}

	public void MoveRight() {
		int index = Mathf.Min(screens.Count - 1, _index + 1);
		if (index == _index) {
			return;
		}

		_index = index;
		MoveToCurrentIndex();
	}

	private void MoveToCurrentIndex() {
		foreach (GameObject g in screens) {
			g.SetActive(false);
		}

		GameObject targetObject = screens[_index];
		targetObject.SetActive(true);
		Camera.main.transform.position = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, Camera.main.transform.position.z);
	}
}
