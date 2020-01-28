using UnityEngine;

public class Audio : MonoBehaviour {
	public AudioSource audioSource;
	public static Audio instance = null;
	public AudioClip[] audioArray;

	void Awake () {
		if (instance == null) {
			instance = this;
		}
	}

	public void Die () {
		audioSource.clip = audioArray[0];
		audioSource.Play();
	}

	public void GetScore () {
		audioSource.clip = audioArray[2];
		audioSource.Play();
	}

	public void Jump () {
		audioSource.clip = audioArray[1];
		audioSource.Play();
	}
}
