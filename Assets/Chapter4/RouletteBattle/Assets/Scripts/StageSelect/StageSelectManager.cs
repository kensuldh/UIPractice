using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//ステージ選択画面
public class StageSelectManager : MonoBehaviour
{
	public TitleManager title;							//タイトル画面
	public BattleManager battle;						//戦闘画面
	public EnemyManager enemy;							//敵の管理
	public AchievementManager achievement;				//アチーブメントの管理
	public StageSelectBoad boad;						//ステージ選択時の、選択相手の表示ボード
	public Image explanationImage;						//（アチブメントアイコンの）説明イメージ
	public StageSelectAnimation stageSelectAnimation;	//ステージ選択演出アニメーション
	public CameraAnimation3D cameraAnimation3D;			//カメラアニメーション
	public StageSelectPlanet planet;					//惑星


	//開く
	public void Open()
	{
		this.gameObject.SetActive(true);
		InitIcons();
		boad.Close();
		explanationImage.gameObject.SetActive(!achievement.IsOpenedOnceAchievement);
	}

	//閉じる
	public void Close()
	{
		this.gameObject.SetActive(false);
	}

	//戻るボタンがクリックされた
	public void OnClickBack()
	{
		Close();
		title.Open();
	}

	//アチーブメントボタンがクリックされた
	public void OnClickAchievement()
	{
		Close();
		achievement.Open();
	}

	//敵を選択した
	public void OnSelectEnemy(StageSelectStageIcon iocn)
	{
		enemy.Select(iocn.enemy);
		planet.RorateOnSelect (iocn.transform,OnRotateComplete);
	}

	//選択後の回転終了
	void OnRotateComplete()
	{
		boad.Open(enemy.Selected);
	}
	

	//Battleボタンがクリックされた
	public void OnClickBattle()
	{
		//TODO アニメーション追加
		float duration = 2.0f;
		stageSelectAnimation.BattleStart(duration);
		cameraAnimation3D.CloseToPlanet(duration);
		Invoke("OpenBattle", duration + 0.1f);
	}

	//バトル画面へ
	private void OpenBattle()
	{
		Close();
		battle.Open(enemy.Selected);
	}

	//表示アイコン初期化
	void InitIcons()
	{
		enemy.UpdateData();
		StageSelectStageIcon[] icons = GetComponentsInChildren<StageSelectStageIcon>(true);
		foreach (StageSelectStageIcon icon in icons)
		{
			icon.Init();
		}
	}
}

