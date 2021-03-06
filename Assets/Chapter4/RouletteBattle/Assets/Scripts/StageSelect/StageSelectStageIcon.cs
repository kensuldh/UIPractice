using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

//ステージ選択用の各キャラクターアイコン（3Dモデル）
public class StageSelectStageIcon : MonoBehaviour
{
	public EnemyManager enemyManager;	//敵の管理
	public Renderer bodyRenderer;		//体の表示
	public EnemyData.Type enemy;		//敵データのタイプ
	public GameObject crown;

	//初期化
	public void Init()
	{
		if (enemyManager != null)
		{
			EnemyData data = enemyManager.FindData(enemy);
			bodyRenderer.material.color = data.imageColor;
			crown.SetActive(data.IsCrown);

			if (data.IsDead)
			{
				this.GetComponent<Animator>().Play("Down");
				this.GetComponent<Collider>().enabled = false;
			}
			this.gameObject.SetActive(data.IsOpen);
		}
	}

}
