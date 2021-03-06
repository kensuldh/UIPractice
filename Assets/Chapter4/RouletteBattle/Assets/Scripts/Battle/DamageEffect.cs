using UnityEngine;
using System.Collections;

public class DamageEffect : MonoBehaviour
{
	private float force = 40.0f;
	private float diminishingRate = 0.5f;

	private Vector3 initPosition;
	private float initForce;

	//初期座標などを記録
	void Awake()
	{
		initPosition = transform.localPosition;
		initForce = force;
	}

	//iTWeenを使用しない場合のダメージエフェクト
	public void Effect ()
	{
		force = initForce;
		StartCoroutine(VibrateCoroutine());
	}

	private IEnumerator VibrateCoroutine()
	{
		while(force > 1.0f)
		{
			Vector2 offset = Random.insideUnitCircle * force;
			transform.localPosition = initPosition + new Vector3(offset.x, offset.y, 0);
			force *= diminishingRate;

			yield return new WaitForEndOfFrame();
		}

		transform.localPosition = initPosition;
	}
}
