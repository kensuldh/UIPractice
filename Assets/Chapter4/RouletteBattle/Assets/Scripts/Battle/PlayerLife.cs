using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//プレイヤーのライフ表示クラス
public class PlayerLife : MonoBehaviour
{
	public Image[] images;		//ライフカウンター表示用のイメージ
	public Sprite onSprite;		//ライフがある場合のスプライト
	public Sprite offSprite;	//ライフがない場合のスプライト
	
	//ライフの値設定
	public void SetLife(int life)
	{
		for (int i = 0; i < images.Length; ++i)
		{
			images[i].sprite = (i < life) ? onSprite : offSprite;
		}
	}
}
