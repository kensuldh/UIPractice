using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyDamageEffect : MonoBehaviour
{
	//  攻撃ヒット時にエネミーをシェイク
	public void Shake (bool isCritical)
	{
		float degree = isCritical? 0.06f : 0.02f;
		iTween.ShakePosition(gameObject, iTween.Hash(
			"x", degree,
			"y", degree,
			"islocal", true, 
			"time", 0.5f
		));
	}
}
