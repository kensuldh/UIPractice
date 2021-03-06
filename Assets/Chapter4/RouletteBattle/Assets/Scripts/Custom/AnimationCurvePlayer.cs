using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//カーブエディタで調整可能な、移動アニメーションを再生する
public class AnimationCurvePlayer : MonoBehaviour
{
	public AnimationCurve curve;		//アニメーションカーブ
	public float delay = 0;				//アニメ開始までの遅延時間
	public float duration = 1.0f;		//アニメーションする時間
	public Vector3 moveDirection;		//アニメーションする位置

	public bool isResetOnEnable = true;			//Disableになった時に、初期値にリセットする

	//RectTransformのキャッシュ
	public RectTransform CachedTransform { get { if (cachedTransform == null) cachedTransform = GetComponent<RectTransform>(); return cachedTransform; } }
	RectTransform cachedTransform;

	//アニメーション再生中か
	public bool IsPlaying { get { return isPlaying; } }
	bool isPlaying;


	//開始位置を記録しておく
	Vector3 startPosition;

	void Awake()
	{
		startPosition = CachedTransform.localPosition;
	}

	void OnEnable()
	{
		if (isResetOnEnable)
		{
			ResetPosition();
			Stop();
		}
	}

	//アニメーション再生
	public void Play()
	{
		StartCoroutine(CoPlayAnimation());
	}

	//アニメーション停止
	public void Stop()
	{
		StopAllCoroutines();
		isPlaying = false;
	}

	//初期位置にリセット
	public void ResetPosition()
	{
		CachedTransform.localPosition = startPosition;
	}

	//アニメーション再生
	IEnumerator CoPlayAnimation()
	{
		isPlaying = true;
		Vector3 initPosition = CachedTransform.localPosition;
		if (delay > 0) yield return new WaitForSeconds(delay);

		float startTime = Time.time;

		// コルーチンにより、関数が呼ばれてからduration時間が経過するまでは、グラフに従った座標にpositionを変更
		while (Time.time - startTime < duration)
		{
			CachedTransform.localPosition = initPosition + moveDirection * curve.Evaluate((Time.time - startTime) / duration) ;
			yield return 0;
		}

		// 目的地到達後は、オブジェクトの座標を目的地に代入
		CachedTransform.localPosition = initPosition + moveDirection;
		isPlaying = false;
	}
}
