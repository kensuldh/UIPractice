using UnityEngine;
using System.Collections;

//勝利時に出てくるクラッカーのパーティクル演出のソートオーダーを変更
public class Confetti : MonoBehaviour
{
	public string sortingLayerName; 	//デフォルトは空で良い
	public int sortingOrder;			//ソートオーダー

	void Start ()
	{
		Renderer renderer = this.GetComponent<Renderer>();
		renderer.sortingLayerName = sortingLayerName;
		renderer.sortingOrder = sortingOrder;
	}
}
