using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//アチーブメント取得を知らせるボード
[RequireComponent(typeof(AnimationCurvePlayer))]
public class AchievementBoard : MonoBehaviour
{
	public Text titleText;				//アチーブメントのタイトルテキスト
	public Text detailText;				//アチーブメントの詳細テキスト
	public AnimationCurvePlayer startAnimation;		//開始演出
	public float waitTime = 2.0f;					//待機時間
	public AnimationCurvePlayer endAnimation;		//終了演出

	//RectTransformの取得
	public RectTransform CachedRectTransform { get { if (cachedRectTransform == null) cachedRectTransform = GetComponent<RectTransform>(); return cachedRectTransform; } }
	RectTransform cachedRectTransform;

	Queue<AchievementData> queueData = new Queue<AchievementData>();	//表示するアチーブメント。複数ためられるようにQueueを使う
	bool isPlaying;		//演出中か

	//新しく開くアチーブメントを追加する
	public void AddNewOpen(AchievementData data)
	{
		data.IsOpen = true;
		queueData.Enqueue(data);
	}

	void OnDisable()
	{
		StopAllCoroutines();
		isPlaying = false;
	}

	void Update()
	{
		//表示するアチーブメントが残っていて演出中じゃないなら、
		//アチーブメントを表示する
		if (queueData.Count > 0 && !isPlaying)
		{
			StartCoroutine(CoOpenBoad(queueData.Dequeue()));
		}
	}

	//アチーブメントの表示演出
	IEnumerator CoOpenBoad(AchievementData data)
	{
		//今表示中のデータ
		isPlaying = true;
		//表示テキストを設定
		titleText.text = data.titleText;
		detailText.text = data.detailText;

		//アチーブメントを表示開始
		startAnimation.Play();
		while (startAnimation.IsPlaying) yield return 0;

		//しばらく表示する
		yield return new WaitForSeconds(waitTime);

		//アチーブメントを表示をしまう
		endAnimation.Play();
		while (endAnimation.IsPlaying) yield return 0;
		isPlaying = false;
	}
}
