using UnityEngine;

public class GameControl : MonoBehaviour
{
	public static GameControl instance = null;

	void Awake () {
		if (instance == null) {
			instance = this;
		}
	}

    void Start () {
		StartGame();
    }

	public void OnClick (string param) {
		if (param == "click") {
			Bird.instance.Jump();
		}
		else if(param == "start_game") {
			StartGame();
		}
	}

	public void StartGame () {
		Bird.instance.StartFly();
		PipeFactory.instance.StartProductPipe();
		AI.instance.StartAI();
	}

	public void EndGame () {
		Bird.instance.Died();
		PipeFactory.instance.StopProductPipe();
	}
}
