using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//戦闘画面のルーレットのID
public enum BattleRouletteSlotID
{
	NormalAttack,		//通常攻撃
	Critical,			//クリティカル
	FilreMagic,			//炎魔法
	IceMagic,			//氷魔法
	ThunderMagic,		//雷魔法
	Charge,				//チャージ
	None,				//なし（ハズレ）
};

//戦闘画面のルーレットのID
public class BattleRoulette : MonoBehaviour
{

	//各スロットのデータ
	[System.Serializable]
	public class BattleRouletteSlot
	{
		public BattleRouletteSlotID id;	//ID
		public float eulerAngle;		//角度
	}

	public Image bg;							//ルーレットの背景
	public Button button;						//ルーレットのボタン
	public Text buttonText;						//ルーレットのボタンのテキスト
	public Image buttonBg;						//ルーレットのボタンの背景イメージ
	public Sprite startBackgroundSprite;		//スタートボタンのスプライト
	public Sprite stopBackgroundSprite;			//ストップボタンのスプライト

	public float speed = 360;					//一秒間の回転速度
	public List<BattleRouletteSlot> slots;		//各スロット（インスペクター上で値を設定する）

	//ルーレットの状態
	public enum State
	{
		Ready,		//待機中
		Rolling,	//回転中
		Stopping,	//止まっている最中（減速中）
	};

	State state;	//現在のルーレットの状態

	//待機中か
	public bool IsReady { get { return state == State.Ready; } }
	//回転中か
	public bool IsRolling { get { return state == State.Rolling; } }
	//止まっている最中か
	public bool IsStopping { get { return state == State.Stopping; } }

	//ルーレットが指しているスロットのID
	BattleRouletteSlotID slotID = 0;
	public BattleRouletteSlotID SlotID {
		get {
			return slotID;
		}
	}

	//ルーレットを待機状態に
	public void Ready()
	{
		state = State.Ready;
		this.gameObject.SetActive(true);
		button.interactable = true;
		buttonText.text = "START";
		buttonBg.sprite = startBackgroundSprite;
	}

	//ルーレット回すボタンがクリックされた
	public void OnClikStart()
	{
		switch (state)
		{
			case State.Ready:	//待機中ならルーレット回す
				buttonText.text = "STOP";
				buttonBg.sprite = stopBackgroundSprite;
				state = State.Rolling;
				StartCoroutine(CoRolling());
				break;
			default:
				break;
		}
	}

	//止めるボタンが押された
	public void OnDownStop()
	{
		switch (state)
		{
			case State.Rolling:	//回っている最中ならルーレット止める
				button.interactable = false;
				state = State.Stopping;
				break;
			default:
				break;
		}
	}

	//ルーレットの回転処理
	IEnumerator CoRolling()
	{
		float rotZ = bg.transform.localEulerAngles.z;
		while(IsRolling)
		{
			rotZ += Time.deltaTime * speed;
			if( rotZ > 360 ) rotZ -= 360;
			bg.transform.localEulerAngles = new Vector3( 0, 0, rotZ );
			yield return 0;
		}

		//ピッタリとスロット中心を指すように止めるため、
		//ボタン押したときに指していたスロットの中心角度を記録
		float centerRot;
		CheckSlot(rotZ, out centerRot);

		//スロットを約一周させて徐々に減速させて止める
		//必ず指定の角度ピッタリで止めるので、速度を使わずに角度を計算して出す
		float from = rotZ;
		float to = centerRot + 360;
		float t = 0;
		float stopTime = (360.0f/speed)*2;
		while(IsStopping)
		{
			t = Mathf.Min( t+Time.deltaTime, stopTime);
			rotZ = from + (to- from)*Mathf.Sin ( Mathf.PI/2*t/stopTime);
			if( rotZ > 360 ) rotZ -= 360;
			bg.transform.localEulerAngles = new Vector3( 0, 0, rotZ );
			yield return 0;
			if (t >= stopTime)
			{
				Ready();
			}
		}
	}

	//指定角度のスロットの中心角度を取得
	void CheckSlot(float rotZ, out float centerRot )
	{
		centerRot = 0;
		float rot = 0;
		foreach( var item in slots )
		{
			rot += item.eulerAngle;
			if( rotZ < rot  )
			{
				slotID = item.id;
				centerRot = rot - (item.eulerAngle/2);
				break;
			}
		}
	}
}
