using UnityEngine;
using System.Collections;

public class PlayerMotion : MonoBehaviour
{
	public float duration = 0.4f;// 移動にかかる時間（秒）

	// プレイヤーをX軸方向にdistanceの距離だけ移動させる
	public void MoveX (float distance)
	{
		StartCoroutine(MoveXCoroutine(distance));
	}

	IEnumerator MoveXCoroutine(float distance)
	{
		float currentX = transform.position.x;
		float targetX = currentX + distance;
		float startTime = Time.time;
		float t = 0;

		while(t < 1.0f)
		{
			t = (Time.time - startTime) / duration;
			transform.position = new Vector3(Mathf.SmoothStep(currentX, targetX, t), 0, 0);
			yield return 0;
		}
	}
}
