using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//アチーメト一覧画面の表示アイテム
public class AchievementItem : MonoBehaviour
{
	public Image boad;				//
	public Image titleImage;		//テキストのイメージ

	//RectTransformの取得
	public RectTransform CachedRectTransform { get{ if( cachedRectTransform == null ) cachedRectTransform = GetComponent<RectTransform>(); return cachedRectTransform; } }
	RectTransform cachedRectTransform;

	//初期化
	public void InitData(AchievementData data)
	{
		float alpha = data.IsOpen ? 1.0f : 0.4f;
		Color color = boad.color;
		color.a = alpha;
		boad.color = color;
		titleImage.sprite = data.titleSprite;
		titleImage.SetNativeSize();
	}

	//アニメーション演出開始
	public void ShowAnimation(int num)
	{
		float initRotationZ = (num % 2 == 0) ? 60.0f : -60.0f;
		float targetRotationZ = (num % 2 == 0) ? -6.0f : 6.0f;
		CachedRectTransform.localEulerAngles = new Vector3(0, 0, initRotationZ);
		CachedRectTransform.localScale = new Vector3(2.0f, 2.0f, 1.0f);

		iTween.RotateTo(CachedRectTransform.gameObject, iTween.Hash(
			"z", targetRotationZ,
			"islocal", true,
			"time", 2.0f,
			"easetype", iTween.EaseType.easeOutElastic
		));

		iTween.ScaleTo(CachedRectTransform.gameObject, iTween.Hash(
			"x", 1.0f,
			"y", 1.0f,
			"islocal", true,
			"time", 2.0f,
			"easetype", iTween.EaseType.easeOutExpo
		));
	}
}
