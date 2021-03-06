using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//戦闘画面
public class BattleManager : MonoBehaviour
{
	public StageSelectManager satageSelect;			//ステージ選択画面
	public EffectController effectController;		//エフェクトコントローラ

	public BattleRoulette roulette;					//ルーレット
	public BattlePlayerData playerData;				//プレイヤーのデータ

	public PlayerLife playerLifeGauge;				//プレイヤーのライフゲージ
	public BattleGauge playerAttackGauge;			//プレイヤーの攻撃力ゲージ
	public BattleGauge playerMagicGauge;			//プレイヤーの魔法力ゲージ

	public EnemyLife enemyLife;						//敵のライフ
	public BattleGauge enemyDeffenceGauge;			//敵の防御力ゲージ
	public BattleGauge enemyMagicResGauge;			//敵の魔法防御力ゲージ
	public Image enemyWeakNessIcon;					//敵の弱点アイコン

	public AchievementBoard achievementBoard;		//アチーブメント取得の通知ボード
	public AchievementManager achievementManager;	//アチーブメント管理クラス

	public GameObject remacthDialog;				//再戦の確認ダイアログ
	public GameObject ending;						//エンディング

	EnemyData enemyData;							//敵のデータ
	bool isEndBattle = false;						//戦闘終了フラグ

	//開く
	public void Open(EnemyData enemyData)
	{
		this.gameObject.SetActive(true);
		this.enemyData = enemyData;
		StartCoroutine(CoBattle());
	}

	//戻るがクリックされた
	public void OnClickBack()
	{
		Back();
	}

	//前の画面に戻る
	void Back()
	{
		this.gameObject.SetActive(false);
		StopAllCoroutines();
		satageSelect.Open();
	}

	//「再戦」ボタンが押された
	public void OnClickRematchYes()
	{
		StartCoroutine(CoBattle());
	}

	//「再戦しない」ボタンが押された
	public void OnClickRematchNo()
	{
		Back();
	}

	bool IsGameComplete( EnemyData enemyData)
	{
		bool isBossDead = (enemyData.type == EnemyData.Type.DarkKing && enemyData.IsDead);
		return isBossDead;
	}

	//戦闘
	IEnumerator CoBattle()
	{
		yield return 0;
		effectController.StartBattle();

		this.remacthDialog.SetActive(false);
		InitPlayer();
		InitEnemy();

		isEndBattle = false;

		//ルーレット待機開始
		roulette.Ready();

		TryOpenAchievement(AchievementData.Type.FirstBattle);

		while (!isEndBattle)
		{
			//ルーレットが始まるまで待機
			while (roulette.IsReady) yield return 0;

			//ルーレットが止まるまで待機
			while (!roulette.IsReady) yield return 0;

			//攻撃を実行
			Attack(roulette.SlotID);
			if (!isEndBattle)
			{
				//戦闘が次に進むのでライフを減らす
				playerData.CurrentLife = playerData.CurrentLife - 1;
				playerLifeGauge.SetLife(playerData.CurrentLife);
				if (playerData.CurrentLife <= 0)
				{	//HP0になったので終了
					isEndBattle = true;
				}
			}
		}

		//戦闘が終わったので、結果表示をする
		if (enemyData.IsDead)
		{
			//勝った場合
			effectController.GameWin();
			TryOpenDefactAchievement(enemyData.type);
			yield return new WaitForSeconds(4.0f);

			//相手がラスボスだった場合
			if( IsGameComplete(enemyData) )
			{
				Ending();

			} else {

				Back();
			}
		}
		else
		{
			//負けた場合
			effectController.GameLose();
			TryOpenAchievement(AchievementData.Type.GameOver);
			yield return new WaitForSeconds(1.0f);

			//再戦を促すダイアログを表示
			this.remacthDialog.SetActive(true);
		}
	}

	//攻撃を実行
	void Attack(BattleRouletteSlotID slotID)
	{
		enemyData.Attacked(slotID, playerData);

		playerAttackGauge.SetValueGradually(playerData.ChargedAttack);
		playerMagicGauge.SetValueGradually(playerData.ChargedMagic);

		enemyLife.SetEnemyLifeGradually(enemyData.CurrentHp, enemyData.hp);

		if (enemyData.IsDead) isEndBattle = true;

		switch (slotID)
		{
			case BattleRouletteSlotID.NormalAttack:
				effectController.AttackEffect(enemyData, slotID);
				break;
			case BattleRouletteSlotID.Critical:
				TryOpenAchievement(AchievementData.Type.CriticalHit);
				effectController.AttackEffect(enemyData, slotID);
				break;
			case BattleRouletteSlotID.None:
				TryOpenAchievement(AchievementData.Type.Miss);
				effectController.AttackEffect(enemyData, slotID);
				break;
			case BattleRouletteSlotID.FilreMagic:
				effectController.AttackEffect(enemyData, slotID);
				break;
			case BattleRouletteSlotID.IceMagic:
				effectController.AttackEffect(enemyData, slotID);
				break;
			case BattleRouletteSlotID.ThunderMagic:
				effectController.AttackEffect(enemyData, slotID);
				break;
			case BattleRouletteSlotID.Charge:
				effectController.AttackEffect(enemyData, slotID);
				break;
			default:
				break;
		}

		if (enemyData.IsWeakNess(slotID))
		{
			TryOpenAchievement(AchievementData.Type.WeakNess);
		}
	}

	//プレイヤーの初期化
	void InitPlayer()
	{
		playerData.Init();
		playerLifeGauge.SetLife(playerData.life);

		playerAttackGauge.Value = 0;
		playerMagicGauge.Value = 0;
		playerAttackGauge.SetValueGradually(playerData.attack);
		playerMagicGauge.SetValueGradually(playerData.magic);
	}

	//敵の初期化
	void InitEnemy()
	{
		enemyData.BattleStart();
		effectController.InitEnemy(enemyData);
		enemyLife.SetEnemyLife(enemyData.CurrentHp, enemyData.hp);

		enemyDeffenceGauge.Value = 0;
		enemyMagicResGauge.Value = 0;
		enemyDeffenceGauge.SetValueGradually(enemyData.deffence);
		enemyMagicResGauge.SetValueGradually(enemyData.magicRes);
		enemyWeakNessIcon.sprite = enemyData.weakIconSprite;
	}

	//指定したアチーブメントが未解放なら解放する
	void TryOpenAchievement(AchievementData.Type type)
	{
		foreach (var item in achievementManager.achievementDataList)
		{
			if (item.CheckNewOpen(type))
			{
				achievementBoard.AddNewOpen(item);
				break;
			}
		}
		TryOpenGameMasterAchievement();
	}

	//指定した敵撃破アチーブメントが未解放なら解放する
	void TryOpenDefactAchievement(EnemyData.Type enemyType)
	{
		foreach (var item in achievementManager.achievementDataList)
		{
			if (item.CheckNewOpenDefactEnemy(enemyType))
			{
				achievementBoard.AddNewOpen(item);
				break;
			}
		}
		TryOpenGameMasterAchievement();
	}

	//全解放アチーブメントが解放可能なら解放する
	void TryOpenGameMasterAchievement()
	{
		AchievementData masterItem = null;
		foreach (var item in achievementManager.achievementDataList)
		{
			if (item.type == AchievementData.Type.GameMaster)
			{
				masterItem = item;
			}
			else
			{
				if (!item.IsOpen)
				{
					return;
				}
			}
		}
		if (masterItem != null)
		{
			achievementBoard.AddNewOpen(masterItem);
		}
	}

	void Ending()
	{
		this.ending.SetActive(true);
	}
}
