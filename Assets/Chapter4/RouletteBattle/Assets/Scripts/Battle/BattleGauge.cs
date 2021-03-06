using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Slider))]
public class BattleGauge : MonoBehaviour
{
	Slider slider;
	int value;

	void Awake()
	{
		slider = GetComponent<Slider>();
	}

	public int Value
	{
		get { return this.value; }
		set
		{
			this.value = value;
			slider.value = value;
		}
	}

	//スライダーのvalue値をiTweenで変化させる
	public void SetValueGradually(int to)
	{
		iTween.ValueTo (gameObject, iTween.Hash (
			"from", (float)this.value,
			"to", (float)to,
			"time", 0.6f,
			"delay", 0.3f, 
			"onupdate", "OnUpdate",
			"easetype", iTween.EaseType.easeInOutQuad
		));

		this.value = to;		
	}

	//ValueTo実行中に毎フレーム呼び出される
	void OnUpdate(float value)
	{
		slider.value = value;
	}
}
