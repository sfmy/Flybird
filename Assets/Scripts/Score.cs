using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public Sprite[] scoreArray;
	public Image score1;
	public Image score2;
	private int score;
	public static Score instance = null;

	void Awake () {
		if (instance == null) {
			instance = this;
		}
		score = 0;
	}

	public void AddScore () {
		score += 1;
		SetScore(score);
	}

	public void SetScore (int score) {
		this.score = score;
		int s1 = score/10%10;
		int s2 = score%10;
		score1.sprite = scoreArray[s1];
		score2.sprite = scoreArray[s2];
	}
}
