using UnityEngine;


/// <summary>
/// Active時にSEを鳴らす
/// </summary>
public class PlaySe : MonoBehaviour
{
	//Active時のSE
	public AudioClip se;

	public bool isPlayOnEnable = true;

	void OnEnable ()
	{
		if (isPlayOnEnable) PlaySound();
	}

	public void PlaySound()
	{
		if (se != null) AudioSource.PlayClipAtPoint(se, Vector3.zero);
	}
}
