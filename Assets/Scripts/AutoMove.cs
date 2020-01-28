using UnityEngine;
using System.Collections;

public class AutoMove : MonoBehaviour {
	private ArrayList groundArray;
	private ArrayList bgArray;
	public GameObject backgroundObj;
	public GameObject bgObj;
	private float speed;

	void Awake () {
		groundArray = new ArrayList();
		bgArray = new ArrayList();
		foreach (var transform in backgroundObj.transform) {
			groundArray.Add(transform);
		}
		foreach (var transform in bgObj.transform) {
			bgArray.Add(transform);
		}
		speed = 1.5f;
	}

	void Update () {
		if (Bird.instance.IsFlying()) {
			for (int i = 0; i < groundArray.Count; ++i) { 
				var gtran = (Transform)groundArray[i];
				var pos = gtran.localPosition;
				pos.x -= speed;
				if (pos.x <= -1005) {
					pos.x = 1005;
				}
				gtran.localPosition = pos;
			}
			for (int i = 0; i < bgArray.Count; ++i) { 
				var btran = (Transform)bgArray[i];
				var pos = btran.localPosition;
				pos.x -= speed;
				if (pos.x <= -900) {
					pos.x = 900;
				}
				btran.localPosition = pos;
			}
		}
	}
}
