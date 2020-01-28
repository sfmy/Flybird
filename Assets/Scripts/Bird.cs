using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
	public Sprite[] birdsImageArray;
	private enum BirdState {
		READY,
		FLYING,
		DIED,
	};
	private BirdState birdState;
	private int imageIndex = 0;
	private float speed = 100f;
	public static Bird instance = null;

	void Awake () {
		if (instance == null) {
			instance = this;
		}
		birdState = BirdState.READY;
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
	}

	void OnCollisionEnter2D () {
		GameControl.instance.EndGame();
	}

	public void Jump () {
		if (birdState == BirdState.FLYING) {
			GetComponent<Rigidbody2D>().velocity = new Vector3(0, speed, 0);
			Audio.instance.Jump();
		}
	}

	public void StartFly () {
		birdState = BirdState.FLYING;
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		StartCoroutine(Fly());
	}

	public void Died () {
		birdState = BirdState.DIED;
	}

	IEnumerator Fly () {
		while (birdState != BirdState.DIED) {
			imageIndex = ++imageIndex%birdsImageArray.Length;
			GetComponent<Image>().sprite = birdsImageArray[imageIndex];
			yield return new WaitForSeconds(0.1f);
		}
	}

	public bool IsFlying () {
		return (birdState == BirdState.FLYING);
	}

	public Vector3 GetBirdPosition () {
		return (Vector3)transform.localPosition;
	}
}
