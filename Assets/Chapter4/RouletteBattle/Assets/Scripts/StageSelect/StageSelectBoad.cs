using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//ステージ選択時の、選択相手の表示ボード
public class StageSelectBoad : MonoBehaviour
{
	public Image enemyMainImage;			//メインイメージ
	public Image enemySubImage;				//サブイメージ
	public Image enemyCrownImage;			//王冠イメージ
	public Text enemyNameText;				//名前テキスト
	public Text enemyLifeText;				//HPテキスト
	public BattleGauge enemyDeffenceGauge;	//防御力ゲージ
	public BattleGauge enemyMagicResGauge;	//魔法防御ゲージ
	public Image enemyWeakNessIcon;			//弱点アイコン

	//指定の敵データを表示するように開く
	public void Open( EnemyData enemyData )
	{
		this.gameObject.SetActive(true);
		InitEnemy(enemyData);
	}

	//閉じる
	public void Close()
	{
		this.gameObject.SetActive(false);
	}

	//閉じるボタンを押された
	public void OnClickClose()
	{
		Close ();
	}

	//敵データに従って表示の初期化
	void InitEnemy(EnemyData enemyData)
	{
		enemyDeffenceGauge.Value = 0;
		enemyMagicResGauge.Value = 0;

		enemyMainImage.sprite = enemyData.mainSprite;
		enemyMainImage.color = enemyData.imageColor;
		enemySubImage.sprite = enemyData.subSprite;
		enemyCrownImage.gameObject.SetActive( enemyData.IsCrown );
		enemyNameText.text = enemyData.name;
		enemyLifeText.text = string.Format( "HP:{0,4}", enemyData.hp );
		enemyDeffenceGauge.SetValueGradually(enemyData.deffence);
		enemyMagicResGauge.SetValueGradually(enemyData.magicRes);
		enemyWeakNessIcon.sprite = enemyData.weakIconSprite;
	}
}

