using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//タイトル画面
public class TitleManager : MonoBehaviour
{
	public StageSelectManager stage;	//ステージ選択画面

	//開く
	public void Open()
	{
		this.gameObject.SetActive (true);
	}

	//閉じる
	public void Close()
	{
		iTween.MoveTo (gameObject, iTween.Hash (
			"y", -6.0f,
			"islocal", true,
			"time", 0.4f,
			"oncomplete", "OnCompleteClose", 
			"easetype", iTween.EaseType.easeInQuad
		));
	}

	//演出の終了後に閉じるを終わらせる
	private void OnCompleteClose()
	{
		this.gameObject.SetActive (false);
	}

	//ステージ選択画面へ
	public void OnClickStage()
	{
		this.Close ();
		stage.Open();
	}
}
