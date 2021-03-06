using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//戦闘時のプレイヤーのデータ
[System.Serializable]
public class BattlePlayerData
{
	public int life;		//最大ライフ
	public int attack;		//攻撃力
	public int magic;		//魔法攻撃力

	//現在のHP
	public int CurrentLife { get { return currentLife; } set { currentLife = value; } }
	int currentLife;

	//チャージされたか
	public bool IsCharged { get { return isCharged; } set { isCharged = value; } }
	bool isCharged;

	//チャージされた攻撃力
	public int ChargedAttack { get { return IsCharged ? attack*2 : attack; }  }

	//チャージされた魔法攻撃力
	public int ChargedMagic { get { return IsCharged ? magic*2 : magic; }  }
	
	//初期化
	public void Init()
	{
		this.currentLife = life;
		this.isCharged = false;
	}
}
