using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//敵データの管理
[System.Serializable]
public class EnemyManager : MonoBehaviour
{
	public List<EnemyData> enemyDataList;	//敵のデータ（インスペクターから設定する）

	//現在選択されている敵
	public EnemyData Selected{ get{ return selected; } }
	EnemyData selected;

	//デバッグ用ステージショートカット
	public DebugShortCut debugShortCut = DebugShortCut.LV1;

	public enum DebugShortCut
	{
		LV1, LV2, LASTBOSS
	}

	void Awake()
	{
		selected = enemyDataList [0];
		if (debugShortCut == DebugShortCut.LASTBOSS) {
			foreach (EnemyData enemyData in enemyDataList) {
				if (enemyData.type != EnemyData.Type.DarkKing) {
					enemyData.IsDead = true;
				}
			}
		}
		else if(debugShortCut == DebugShortCut.LV2)
		{
			foreach (EnemyData enemyData in enemyDataList) {
				if (enemyData.typeLV == 1) {
					enemyData.IsDead = true;
				}
			}
		}
	}

	//敵の選択
	public void Select( EnemyData.Type type )
	{
		selected = FindData(type);
	}

	//タイプから敵を選択
	public EnemyData FindData(EnemyData.Type type)
	{
		return enemyDataList.Find(item => item.type == type);
	}

	//データを更新（撃破によるステージ切り替え）
	public void UpdateData()
	{
		bool isAllDeadLv1 = true;
		bool isAllDeadLv2 = true;
		foreach( EnemyData enemyData in enemyDataList )
		{
			if (!enemyData.IsDead)
			{
				if (enemyData.typeLV == 1)
				{
					isAllDeadLv1 = false;
				}
				else if (enemyData.typeLV == 2)
				{
					isAllDeadLv2 = false;
				}
			}
		}
		foreach (EnemyData enemyData in enemyDataList)
		{
			if (enemyData.typeLV == 1)
			{
				enemyData.IsOpen = true;
			}
			else if (enemyData.typeLV == 2)
			{
				enemyData.IsOpen = isAllDeadLv1;
			}
			else
			{
				enemyData.IsOpen = isAllDeadLv2;
			}
		}
	}
}
