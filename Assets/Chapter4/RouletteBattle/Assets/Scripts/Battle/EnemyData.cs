using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//敵のデータ
[System.Serializable]
public class EnemyData
{
	//敵のタイプ
	public enum Type
	{
		TallMan,
		FireMan,
		IceMan,
		ClayMan,
		TallKing,
		FireKing,
		IceKing,
		ClayKing,
		DarkKing,
		Max,
	};

	public string name;						//名前
	public Type type = Type.TallMan;		//タイプ
	public int typeLV;						//レベル。（前半か後半か、ラスボスか）
	public int hp;							//HP
	public int deffence;					//防御力
	public int magicRes;					//魔法防御力
	public BattleRouletteSlotID weakNess;	//弱点
	public Sprite weakIconSprite;			//弱点のスプライト

	public Color imageColor = Color.white;	//表示色
	public Sprite mainSprite;				//2D表示の場合の、本体の表示スプライト
	public Sprite subSprite;				//2D表示の場合の、サブパーツの表示スプライト

	//現在のダメージ
	public int CurrentDamage {
		get {
			return currentDamage;
		}
	}
	int currentDamage;

	//現在のダメージテキスト(デバッグ用）
	string currentDamageText;
	public string CurrentDamageText
	{
		get
		{
			return currentDamageText;
		}
	}

	//現在のHP
	public int CurrentHp {
		get {
			return currentHp;
		}
	}
	int currentHp;


	//死亡したか
	public bool IsDead
	{
		get {
			return isDead;
		}
		set
		{
			isDead = value;
		}
	}
	bool isDead;

	//解放済み（ステージ選択可能か）
	public bool IsOpen
	{
		get {
			return isOpen;
		}
		set {
			isOpen = value;
		}
	}
	bool isOpen;

	//現在のダメージテキスト(デバッグ用）
	public bool IsCrown
	{
		get
		{
			return typeLV != 1;
		}
	}

	//戦闘開始
	public void BattleStart()
	{
		currentDamageText = "";
		currentDamage = 0;
		currentHp = hp;
	}

	/// 攻撃された
	public void Attacked(BattleRouletteSlotID slotID, BattlePlayerData playerData)
	{
		switch(slotID)
		{
		case BattleRouletteSlotID.None:
			currentDamage = 0;
			currentDamageText = "ミス！ダメージを与えられない！";
			playerData.IsCharged = false;
			break;
		case BattleRouletteSlotID.Charge:
			currentDamage = 0;
			currentDamageText = "力をためた！！";
			playerData.IsCharged = true;
			break;
		default:
			CalcDamage(slotID, playerData);

			currentDamageText = AttackedName(slotID) + "\n"
				+ name + "に" + (currentDamage) + "のダメージを与えた！";
			currentHp = Mathf.Max( currentHp-currentDamage, 0 );
			isDead = (currentHp <= 0);
			playerData.IsCharged = false;
			break;
		}
		Debug.Log(CurrentDamageText);
	}

	// ダメージ計算
	void CalcDamage(BattleRouletteSlotID slotID, BattlePlayerData playerData)
	{
		switch (slotID)
		{
			case BattleRouletteSlotID.NormalAttack:
			currentDamage = (playerData.ChargedAttack - deffence) * 100 + RandomDice(3,6,-7);
				break;
			case BattleRouletteSlotID.Critical:
			currentDamage = (playerData.ChargedAttack - deffence) * 200 + RandomDice(3,6,-7);
				break;
			case BattleRouletteSlotID.FilreMagic:
			case BattleRouletteSlotID.IceMagic:
			case BattleRouletteSlotID.ThunderMagic:
				currentDamage = (playerData.ChargedMagic - magicRes) * 100 + RandomDice(3,6,-7);
				break;
		}
		currentDamage = Mathf.Max(0, currentDamage);
		if( IsWeakNess(slotID) ) currentDamage *= 2;
	}

	//弱点が突かれたか？
	public bool IsWeakNess( BattleRouletteSlotID slotID )
	{
		return (slotID == weakNess);
	}

	//サイコロを複数転がす要領で、正規表現に近い偏りのランダム値を出す
	int RandomDice( int num, int ranomMax, int bonus )
	{
		int ret = bonus;
		for (int i = 0; i < num; ++i)
		{
			ret += Random.Range(1, ranomMax );
		}
		return ret;
	}

	//攻撃の名前を取得（デバッグ用）
	static string AttackedName(BattleRouletteSlotID slotID)
	{
		switch (slotID)
		{
			case BattleRouletteSlotID.NormalAttack:
				return "通常攻撃";
			case BattleRouletteSlotID.Critical:
				return "クリティカル";
			case BattleRouletteSlotID.FilreMagic:
				return "炎の魔法";
			case BattleRouletteSlotID.IceMagic:
				return "氷の魔法";
			case BattleRouletteSlotID.ThunderMagic:
				return "雷の魔法";
			default:
				return "";
		}
	}

}
