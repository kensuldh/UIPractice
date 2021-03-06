using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
	public EnemyDamageEffect enemyDamageEffect;
	public Animator modelAnimator;
	public GameObject body;
	public GameObject crown;
	public Text damageText;
	float damageTextInitY;

	//敵のダメージUIの初期座標を代入
	void Awake ()
	{
		damageTextInitY = damageText.transform.localPosition.y;
	}

	//アクティブ時、ダメージテキストを非表示に
	void OnEnable ()
	{
		damageText.gameObject.SetActive(false);
	}

	//敵のアニメーション
	public void BattleStart(EnemyData enemyData)
	{
		if (enemyData.IsDead)
		{
			modelAnimator.Play("Down");
		}
		else
		{
			modelAnimator.Play("Walk");
		}
		body.GetComponent<Renderer>().material.color = enemyData.imageColor;
		crown.SetActive(enemyData.IsCrown);
	}

	//敵のダメージエフェクト
	public void DamageEffect (EnemyData enemyData, bool isHit = true, bool isCritical = false)
	{
		damageText.gameObject.SetActive(true);
		damageText.text = isHit? enemyData.CurrentDamage.ToString() : "miss";
		damageText.transform.localPosition = new Vector3 (0, damageTextInitY - 50.0f, 0);

		iTween.MoveTo (damageText.gameObject, iTween.Hash (
			"y", damageTextInitY,
			"islocal", true,
			"time", 0.5f,
			"oncomplete", "OnEffectComplete", 
			"oncompletetarget", gameObject, 
			"easetype", iTween.EaseType.easeOutBack
		));

		if (enemyData.IsDead)
		{
			modelAnimator.Play("Down");

		} else if(isHit) {

			// shakeエフェクト
			enemyDamageEffect.Shake(isCritical);
		}
	}

	//0.5秒後にテキストを非表示に
	void OnEffectComplete()
	{
		Invoke("HideDamageText", 0.5f);
	}

	void HideDamageText()
	{
		damageText.gameObject.SetActive(false);
	}
}
