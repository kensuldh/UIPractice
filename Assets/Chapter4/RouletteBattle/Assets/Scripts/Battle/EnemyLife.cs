using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//敵の体力表示
public class EnemyLife : MonoBehaviour
{
	//前面の体力ゲージ
	[SerializeField]
	private Slider lifeSlider;

	//背面の体力ゲージ
	[SerializeField]
	private Slider lifeSliderDelayed;

	//体力のテキストUI。現在のライフ/最大のライフで表示されている
	[SerializeField]
	private Text counterText;

	//敵キャラクターの最大ライフ
	private int maxLife;

	//敵キャラクターの現在のライフ
	private int currentLife;

	//敵キャラクターの残りライフを設定。初期化など、アニメーションさせずに即時に代入する場合に使用。
	public void SetEnemyLife(int current, int max)
	{
		maxLife = max;
		currentLife = current;
		float value = (float)currentLife / (float)maxLife;

		lifeSlider.value = value;
		lifeSliderDelayed.value = value;

		string format = "{0,4}/{1,4}";
		counterText.text = string.Format(format, currentLife, maxLife);
	}

	//敵キャラクターの残りライフを設定。徐々に変化させる場合に使用。
	public void SetEnemyLifeGradually(int current, int max)
	{
		maxLife = max;
		float fromValue = (float)currentLife / (float)maxLife;
		float fromLife = currentLife;
		currentLife = current;
		float toValue = (float)currentLife / (float)maxLife;
		float toLife = currentLife;

		//前面の体力スライダーの値を素早く変化させる
		ValueTransition(fromValue, toValue, 0.2f, 0, "OnSliderUpdate", iTween.EaseType.easeOutExpo);

		//少し遅れて、背面の体力スライダーの値をゆっくり変化させる
		ValueTransition(fromValue, toValue, 0.8f, 0.2f, "OnSliderDelayedUpdate", iTween.EaseType.linear);

		//体力のテキスト表示をを徐々に変化させる
		ValueTransition(fromLife, toLife, 1.0f, 0, "OnTextUpdate", iTween.EaseType.linear);
	}

	//値の変化を変えるiTween.ValueToをラップしたメソッド
	private void ValueTransition(float from, float to, float duration, float delay, string update, iTween.EaseType easetype)
	{
		iTween.ValueTo (gameObject, iTween.Hash (
			"from", from,
			"to", to,
			"time", duration,
			"delay", delay,
			"onupdate", update,
			"easetype", easetype
		));
	}

	//前面スライダーのValueに変化し続ける体力の割合（コールバック値）を代入。iTween.ValueTo実行中に毎フレーム呼び出される。
	private void OnSliderUpdate(float value)
	{
		lifeSlider.value = value;
	}

	//背面スライダーのValueに変化し続ける体力の割合（コールバック値）を代入。iTween.ValueTo実行中に毎フレーム呼び出される。
	private void OnSliderDelayedUpdate(float value)
	{
		lifeSliderDelayed.value = value;
	}

	//Textコンポーネントのtextにフォーマットを整えたコールバック値を代入。iTween.ValueTo実行中に毎フレーム呼び出される。
	private void OnTextUpdate(int life)
	{
		string format = "{0,4}/{1,4}";
		counterText.text = string.Format(format, life, maxLife);
	}
}
