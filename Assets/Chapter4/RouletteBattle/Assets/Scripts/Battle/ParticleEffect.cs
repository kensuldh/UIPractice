using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleEffect : MonoBehaviour
{
	private ParticleSystem particle;

	void Awake ()
	{
		//毎フレーム呼び出すのでキャッシュしておく
		particle = GetComponent<ParticleSystem>();
	}

	// アクティブ時にコルーチンによってパーティクルの再生をチェック。エフェクトが終わると非表示にする
	void OnEnable ()
	{
		StartCoroutine(StartPatricle());
	}

	IEnumerator	StartPatricle()
	{
		while(particle.isPlaying)
		{
			yield return 0;
		}

		gameObject.SetActive(false);
	}
}
