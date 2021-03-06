using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EffectController : MonoBehaviour
{
	public EnemyDamage enemyDamage;			//敵のダメージ演出

	public RectTransform roulette;			//ルーレットのUI
	public RectTransform playerBoard;		//プレイヤーの情報のUI
	public RectTransform enemytBoard;		//敵の情報のUI
	public Transform enemy3D;				//敵の3Dモデル
	public Image fightImage;				//ゲーム開始時のFight表示

	public ParticleSystem slashEffect;		//通常攻撃のエフェクト
	public ParticleSystem criticalEffect;	//クリティカル攻撃のエフェクト
	public ParticleSystem fireEffect;		//炎魔法攻撃のエフェクト
	public ParticleSystem iceEffect;		//氷魔法攻撃のエフェクト
	public ParticleSystem thunderEffect;	//雷魔法攻撃のエフェクト
	public ParticleSystem confettiEffect;	//勝利時の紙吹雪エフェクトのエフェクト
	public PlaySe charge;					//「ためる」のサウンドがあるObject

	public Image resultImage;				//Win・Loseの結果表示
	public Sprite winSprite;				//Win表示のスプライト
	public Sprite loseSprite;				//Lose表示のスプライト
	
	//各UIの初期座標
	Vector3 initRoulettePosition;
	Vector3 initPlayerBoard;
	Vector3 initEnemytBoard;
	Vector3 initEnemy3D;

	bool isInit;
	//初期座標を取得
	void Init()
	{
		if(isInit) return;
		initRoulettePosition = roulette.localPosition;
		initPlayerBoard = playerBoard.localPosition;
		initEnemytBoard = enemytBoard.localPosition;
		initEnemy3D = enemy3D.localPosition;
		isInit = true;
	}

	//バトル開始時のアニメーション
	public void StartBattle()
	{
		Init();
		this.resultImage.gameObject.SetActive(false);

		roulette.localPosition += new Vector3(0, -450.0f, 0);
		playerBoard.localPosition += new Vector3(-300, 0, 0);
		enemytBoard.localPosition += new Vector3(300, 0, 0);
		enemy3D.localPosition += new Vector3(0, 0.5f, 0);
		fightImage.transform.localPosition = new Vector3(0, 400.0f, 0);
		fightImage.transform.localEulerAngles = new Vector3(0, 0, 40.0f);

		MoveTo(roulette.gameObject, initRoulettePosition, 0.5f, 0.4f, iTween.EaseType.easeInOutExpo);
		MoveTo(playerBoard.gameObject, initPlayerBoard, 0.5f, 0.3f, iTween.EaseType.easeInOutExpo);
		MoveTo(enemytBoard.gameObject, initEnemytBoard, 0.5f, 0.3f, iTween.EaseType.easeInOutExpo);
		MoveTo(enemy3D.gameObject, initEnemy3D, 0.5f, 0.2f, iTween.EaseType.easeInOutExpo);
		MoveTo(fightImage.gameObject, Vector3.zero, 1.0f, 0, iTween.EaseType.easeOutElastic);
		RotateTo(fightImage.gameObject, Vector3.zero, 1.0f, 0, iTween.EaseType.easeOutBack);
		MoveTo(fightImage.gameObject, new Vector3(0, -500.0f, 0), 0.5f, 1.8f, iTween.EaseType.easeInBack);
	}

	//勝利時のテキストおよびクラッカーパーティクル表示
	public void GameWin()
	{
		this.resultImage.gameObject.SetActive(true);
		resultImage.sprite = winSprite;
		confettiEffect.gameObject.SetActive(true);
	}

	//敗北時のテキスト表示
	public void GameLose()
	{
		this.resultImage.gameObject.SetActive(true);
		resultImage.sprite = loseSprite;
	}
	
	public void InitEnemy(EnemyData enemyData)
	{
		enemyDamage.BattleStart(enemyData);
	}

	//各種アタックエフェクト
	public void AttackEffect(EnemyData enemyData, BattleRouletteSlotID slotID)
	{
		switch (slotID)
		{
		case BattleRouletteSlotID.NormalAttack:
			enemyDamage.DamageEffect(enemyData, true, true);
			slashEffect.gameObject.SetActive(true);
			break;
		case BattleRouletteSlotID.Critical:
			enemyDamage.DamageEffect(enemyData, true, true);
			criticalEffect.gameObject.SetActive(true);
			break;
		case BattleRouletteSlotID.None:
			enemyDamage.DamageEffect(enemyData, false);
			break;
		case BattleRouletteSlotID.FilreMagic:
			enemyDamage.DamageEffect(enemyData, true, false);
			fireEffect.gameObject.SetActive(true);
			break;
		case BattleRouletteSlotID.IceMagic:
			enemyDamage.DamageEffect(enemyData, true, false);
			iceEffect.gameObject.SetActive(true);
			break;
		case BattleRouletteSlotID.ThunderMagic:
			enemyDamage.DamageEffect(enemyData, true, false);
			thunderEffect.gameObject.SetActive(true);
			break;
		case BattleRouletteSlotID.Charge:
			charge.PlaySound();
			break;
		default:
			break;
		}
	}

	//座標移動アニメーション
	void MoveTo(GameObject target, Vector3 position, float duration, float delay, iTween.EaseType easeType)
	{
		iTween.MoveTo (target, iTween.Hash (
			"position", position,
			"islocal", true,
			"time", duration,
			"delay", delay,
			"easetype", easeType
		));
	}

	//回転アニメーション
	void RotateTo(GameObject target, Vector3 rotation, float duration, float delay, iTween.EaseType easeType)
	{
		iTween.RotateTo (target, iTween.Hash (
			"rotation", rotation,
			"islocal", true,
			"time", duration,
			"delay", delay,
			"easetype", easeType
		));
	}
}
