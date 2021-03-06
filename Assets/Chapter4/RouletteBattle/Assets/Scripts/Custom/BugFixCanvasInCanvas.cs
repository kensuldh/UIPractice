using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//キャンバス内キャンバスのバグ回避コード
[RequireComponent(typeof(Canvas))]
public class BugFixCanvasInCanvas : MonoBehaviour
{
	void Start()
	{
		Canvas canvas = GetComponent<Canvas>();
		//ゲーム開始後にsortingOrderを設定しなおす
		canvas.sortingOrder = canvas.sortingOrder;
	}
}
