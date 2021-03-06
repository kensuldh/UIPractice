 using UnityEngine;
using System.Collections;

public class StageSelectAnimation : MonoBehaviour
{
	[SerializeField]
	private DamageEffect frontCamera;
	
	[SerializeField]
	private CanvasRenderer board;
	
	[SerializeField]
	private CanvasRenderer enemy;
	
	[SerializeField]
	private CanvasRenderer battleButton;
	
	[SerializeField]
	private CanvasRenderer coverBG;

	private Vector3 initEnemyPosition;
	private Vector3 initButtonPosition;

	//UIの初期座標を記録
	void Awake ()
	{
		initButtonPosition = battleButton.transform.localPosition;
		initEnemyPosition = enemy.transform.localPosition;
	}

	//アクティブ時のアニメーション
	void OnEnable ()
	{
		board.transform.localPosition = new Vector3(800.0f, 800.0f, 0);
		board.transform.eulerAngles = new Vector3(0, 0, 150.0f);
		enemy.transform.localPosition = initEnemyPosition + Vector3.up * 800.0f;
		coverBG.SetColor(new Color(0, 0, 0, 0));
		coverBG.gameObject.SetActive(false);
		battleButton.transform.localPosition -= Vector3.down * -200.0f;

		iTween.MoveTo (board.gameObject, iTween.Hash (
			"position", Vector3.zero,
			"islocal", true,
			"time", 0.5f,
			"easetype", iTween.EaseType.easeOutBack
		));
		
		iTween.RotateTo (board.gameObject, iTween.Hash (
			"rotation", Vector3.zero,
			"islocal", true,
			"time", 0.5f,
			"easetype", iTween.EaseType.easeOutQuad
		));

		iTween.MoveTo (enemy.gameObject, iTween.Hash (
			"position", initEnemyPosition,
			"islocal", true,
			"time", 0.5f,
			"oncomplete", "OnCompleteAnimation", 
			"oncompletetarget", gameObject, 
			"easetype", iTween.EaseType.easeInQuart
		));
		
		iTween.MoveTo (battleButton.gameObject, iTween.Hash (
			"position", initButtonPosition,
			"islocal", true,
			"time", 0.3f,
			"delay", 0.4f,
			"easetype", iTween.EaseType.easeOutQuart
		));
	}

	//アニメーション完了時にカメラが揺れる
	void OnCompleteAnimation()
	{
		frontCamera.Effect();
	}

	//バトル開始ボタンを押した時の演出
	public void BattleStart(float duration)
	{
		iTween.MoveTo (board.gameObject, iTween.Hash (
			"position", new Vector3(-800.0f, -800.0f, 0),
			"islocal", true,
			"time", duration * 0.5f,
			"easetype", iTween.EaseType.easeInBack
		));

		iTween.RotateTo (board.gameObject, iTween.Hash (
			"rotation", new Vector3(0, 0, -150.0f),
			"islocal", true,
			"time", duration * 0.5f,
			"easetype", iTween.EaseType.easeInQuad
		));

		iTween.MoveTo (enemy.gameObject, iTween.Hash (
			"position", Vector3.zero,
			"islocal", true,
			"time", duration,
			"easetype", iTween.EaseType.easeInOutQuad
		));
		
		iTween.ValueTo (gameObject, iTween.Hash (
			"from", 0.0f,
			"to", 1.0f,
			"time", duration,
			"onupdate", "OnUpdateValue", 
			"easetype", iTween.EaseType.easeInQuad
		));
	}

	//背景色を黒くするアニメーション
	void OnUpdateValue(float val)
	{
		coverBG.gameObject.SetActive(true);
		coverBG.SetColor(new Color(1, 1, 1, val));
	}
}
