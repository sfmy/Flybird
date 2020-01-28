using System.Collections;
using UnityEngine;

public class PipeFactory : MonoBehaviour
{
	public GameObject pipeObj;
	private ArrayList pipePool;
	private ArrayList pipeList;
	private int poolSize = 10;
	private Vector2 startPos;
	private struct PipeMsg {
		public GameObject pipe;
		public bool isPassed;
	};
	private enum PipeState {
		ON, OFF,
	};
	private PipeState pipeState;
	public static PipeFactory instance = null;
	public float minY;
	public float maxY;
	public float endX;

	void Awake () {
		if (instance == null) {
			instance = this;
		}
		pipeState = PipeState.OFF;
		pipeObj.SetActive(false);
		pipePool = new ArrayList();
		pipeList = new ArrayList();
		for (int i = 0; i < poolSize; ++i) {
			PipeMsg pipMsg;
			pipMsg.pipe = Instantiate(pipeObj, pipeObj.transform.position, Quaternion.identity, transform);
			pipMsg.isPassed = false;
			pipePool.Add(pipMsg);
		}
	}

	public void StartProductPipe () {
		pipeState = PipeState.ON;
		StartCoroutine(NewPipe());
	}

	public void StopProductPipe () {
		pipeState = PipeState.OFF;
	}

	IEnumerator NewPipe () {
		if (pipeState == PipeState.ON && pipeList.Count != 0) {
			yield return new WaitForSeconds(5.0f);
		}
		if (pipeState == PipeState.ON) {
			if (pipePool.Count > 0) {
				Debug.Log("new pipe");
				PipeMsg pipeMsg = (PipeMsg)pipePool[0];
				pipePool.RemoveAt(0);
				float y = Random.Range(minY, maxY);
				pipeMsg.pipe.transform.localPosition = new Vector3(-endX, y, 0);
				pipeMsg.pipe.SetActive(true);
				pipeMsg.isPassed = false;
				pipeList.Add(pipeMsg);
				StartCoroutine(NewPipe());
			}
			else {
				Debug.LogError("pipePool is null");
			}
		}
	}

    void Update () {
		if (pipeState == PipeState.ON) {
			for (int i = 0; i < pipeList.Count; ++i) {
				var pipeMsg = (PipeMsg)pipeList[i];
				var pos = pipeMsg.pipe.transform.localPosition;
				pos.x -= 1.5f;
				pipeMsg.pipe.transform.localPosition = pos;
				if (pipeMsg.isPassed == false && pos.x <= 1) {
					pipeMsg.isPassed = true;
					pipeList[i] = pipeMsg;
					Score.instance.AddScore();
					Audio.instance.GetScore();
				}
				if (pos.x <= endX) {
					pipeMsg.pipe.SetActive(false);
					pipePool.Add(pipeMsg);
					pipeList.RemoveAt(i);
				}
			}
		}
    }

	public Vector3 GetPassPosition () {
		for (int i = 0; i < pipeList.Count; ++i) {
			var pipeMsg = (PipeMsg)pipeList[i];
			if ((pipeMsg.isPassed == true && pipeMsg.pipe.transform.localPosition.x >= -26) || pipeMsg.isPassed != true) {
				return (Vector3)pipeMsg.pipe.transform.localPosition;
			}
		}
		return new Vector3(0f, 0f, 0f);
	}
}
