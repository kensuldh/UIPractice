using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

//ステージ選択画面の惑星
public class StageSelectPlanet : MonoBehaviour
{
	public float speed = 360.0f;	//ドラッグによる回転スピード
	public float friction = 0.1f;	//フリック（ドラッグを離した）時の減速係数
	Vector2 rotVel;					//現在の回転速度

	//ドラッグ時のイベント
	public void OnDrag( BaseEventData data )
	{
		PointerEventData pointer = data as PointerEventData;
		rotVel.y = -pointer.delta.x * speed;
	}

	void Update()
	{
		//回転させる
		this.transform.Rotate(new Vector3(0, rotVel.y, 0), Space.World);
		//回転を減速させる
		rotVel *= (1 - friction);
	}

	System.Action onRotateCompete;

	//選択したキャラが正面を向くように回転
	public void RorateOnSelect( Transform enemy, System.Action onRotateCompete )
	{
		this.onRotateCompete = onRotateCompete;
		Vector3 from = enemy.position;
		from.y = 0;
		from.Normalize ();
		Vector3 to = Vector3.back;
		Vector3 euler = Quaternion.FromToRotation( from, to ).eulerAngles;

		if (euler.y > 180)
			euler.y -= 360;

		iTween.RotateAdd (gameObject, iTween.Hash (
			"y", euler.y,
			"islocal", true,
			"speed", 60.0f,
			"oncomplete", "OnRotateComplete",
			"oncompletetarget", gameObject,
			"easetype", iTween.EaseType.easeInOutQuad
			));
	}

	//回転終了
	void OnRotateComplete()
	{
		onRotateCompete ();
	}
}
