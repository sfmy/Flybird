using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour{
	public static AI instance = null;
	private float off = 20;
	public bool useAI;

	void Awake () {
		if (instance == null) {
			instance = this;
		}
	}

	public void StartAI () {
		if (useAI) {
			StartCoroutine(ControlBird());
		}
	}

	IEnumerator ControlBird () {
		if (Bird.instance.IsFlying()) {
			yield return new WaitForSeconds(0.2f);
		}
		if (Bird.instance.IsFlying()) {
			var birdPos = Bird.instance.transform.localPosition;
			var pipePos = PipeFactory.instance.GetPassPosition();
			if (birdPos.y <= pipePos.y-off) {
				Bird.instance.Jump();
			}
			StartCoroutine(ControlBird());
		}
	}
}
