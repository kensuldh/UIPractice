using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//アチーブメント管理＆一覧表示画面
public class AchievementManager : MonoBehaviour
{
	public StageSelectManager stage;	//ステージ選択画面
	public GameObject itemPrefab;		//アチーブメント表示用のプレハブ
	public Transform itemsGroup;		//アイテムを追加する親オブジェクト
	public List<AchievementData> achievementDataList;	//全アチーブメントデータ・インスペクター上で編集する

	//アチーブメント画面が一度でも開かれたかどうか
	public bool IsOpenedOnceAchievement {get { return this.isOpenedOnceAchievement;}}
	bool isOpenedOnceAchievement;

	//開く
	public void Open()
	{
		isOpenedOnceAchievement = true;
		this.gameObject.SetActive (true);
		StartCoroutine(CreateItems());
	}

	//閉じる
	public void Close()
	{
		StopAllCoroutines();
		ClearItems();
		this.gameObject.SetActive(false);
	}

	//戻るボタン押された
	public void OnClickBack()
	{
		Close ();
		stage.Open();
	}

	//アチーブメント一覧を作成する
	IEnumerator CreateItems()
	{
		ClearItems();

		for (int i = 0; i < achievementDataList.Count; i++)
		{
			GameObject go = GameObject.Instantiate(itemPrefab) as GameObject;
			go.transform.SetParent(itemsGroup, false);
			AchievementItem item = go.GetComponent<AchievementItem>();
			item.InitData(achievementDataList[i]);
			item.ShowAnimation(i);

			//時間差をつけてアイテムを生成
			yield return new WaitForSeconds(0.1f);
		}
	}

	//アイテムを削除
	void ClearItems()
	{
		foreach (Transform child in itemsGroup.transform)
		{
			GameObject.Destroy(child.gameObject);
		}
	}
}
