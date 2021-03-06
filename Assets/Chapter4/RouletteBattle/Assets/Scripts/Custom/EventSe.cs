using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// イベントを受け取ってSEを鳴らす
/// </summary>
public class EventSe : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, ISubmitHandler, IMoveHandler
{
	//クリック時のSE
	public AudioClip clicked;
	//ハイライト時のSE
	public AudioClip highlited;

	//クリックイベントでSEを鳴らす
	public void OnPointerClick(PointerEventData data)
	{
		if (clicked != null) AudioSource.PlayClipAtPoint(clicked, Vector3.zero);
	}
	
	//ハイライトでSEを鳴らす
	public void OnPointerEnter(PointerEventData data)
	{
		if (highlited != null) AudioSource.PlayClipAtPoint(highlited, Vector3.zero);
	}
	
	//決定ボタンでSEを鳴らす
	public void OnSubmit(BaseEventData eventData)
	{
		if (clicked != null) AudioSource.PlayClipAtPoint(clicked, Vector3.zero);
	}
	
	//キー移動でSE鳴らす
	public void OnMove(AxisEventData eventData)
	{
		if (eventData.selectedObject == this.gameObject) return;
		if (highlited != null) AudioSource.PlayClipAtPoint(highlited, Vector3.zero);
	}
}
