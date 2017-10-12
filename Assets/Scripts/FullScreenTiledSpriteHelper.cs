using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FullScreenTiledSpriteHelper : MonoBehaviour {
	public RectTransform parentTransform;
	public SpriteRenderer sr;

	public bool scaleX = true;
	public bool scaleY = true;

	void Awake () {
		if (sr == null) {
			sr = this.gameObject.GetComponent<SpriteRenderer>();
		}
	}
	
	void Update () {
		if (parentTransform == null) {
			return;
		}

		if (parentTransform.rect.width != sr.size.x || parentTransform.rect.height != sr.size.y) {
			Vector2 newSize = parentTransform.rect.size;
			if (!scaleX) {
				newSize.x = sr.size.x;
			}

			if (!scaleY) {
				newSize.y = sr.size.y;
			}

			sr.size = newSize;
		}
	}
}
