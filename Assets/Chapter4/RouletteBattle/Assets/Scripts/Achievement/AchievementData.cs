using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// アチーブメントのデータ
[System.Serializable]
public class AchievementData
{
	//アチーブメントのタイプ
	public enum Type
	{
		FirstBattle,			//初回攻撃
		DefeatTallMan,			//TallManを撃破
		DefeatFireMan,			//TallManを撃破
		DefeatIceMan,			//TallManを撃破
		DefeatClayMan,			//TallManを撃破
		DefeatTallKing,			//TallManを撃破
		DefeatFireKing,			//TallManを撃破
		DefeatIceKing,			//TallManを撃破
		DefeatClayKing,			//TallManを撃破
		DefeatDarkKing,			//TallManを撃破
		GameOver,				//GameOver(敗北)
		Miss,					//攻撃をMiss
		CriticalHit,			//クリティカル攻撃
		WeakNess,				//弱点攻撃
		GameMaster,				//全ステージクリア
	};


	public Type type;				//タイプ
	public string titleText;		//タイトルテキスト
	public Sprite titleSprite;		//タイトルのスプライト
	public string detailText;		//詳細のテキスト

	//解放済みか
	public bool IsOpen
	{
		get { return isOpen; }
		set { isOpen = value; }
	}
	bool isOpen;	

	//指定のタイプのアチーブメントで、新たに開けるか
	public bool CheckNewOpen(Type type)
	{
		if (IsOpen) return false;

		return (this.type == type);
	}

	//倒した敵の撃破アチーブメントを新たに開けるか
	public bool CheckNewOpenDefactEnemy(EnemyData.Type enemyType)
	{
		if (IsOpen) return false;

		switch (type)
		{
			case Type.DefeatTallMan:
				return (enemyType == EnemyData.Type.TallMan);
			case Type.DefeatFireMan:
				return (enemyType == EnemyData.Type.FireMan);
			case Type.DefeatIceMan:
				return (enemyType == EnemyData.Type.IceMan);
			case Type.DefeatClayMan:
				return (enemyType == EnemyData.Type.ClayMan);
			case Type.DefeatTallKing:
				return (enemyType == EnemyData.Type.TallKing);
			case Type.DefeatFireKing:
				return (enemyType == EnemyData.Type.FireKing);
			case Type.DefeatIceKing:
				return (enemyType == EnemyData.Type.IceKing);
			case Type.DefeatClayKing:
				return (enemyType == EnemyData.Type.ClayKing);
			case Type.DefeatDarkKing:
				return (enemyType == EnemyData.Type.DarkKing);
			default:
				return false;
		}
	}
}
