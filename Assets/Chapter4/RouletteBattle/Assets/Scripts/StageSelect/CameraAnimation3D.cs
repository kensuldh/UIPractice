using UnityEngine;
using System.Collections;

public class CameraAnimation3D : MonoBehaviour
{
	[SerializeField]
	private Camera cameraBody;

	public float duration = 1.0f;

	//ステージセレクト画面が出てくるときの演出
	void OnEnable ()
	{
		transform.localPosition = new Vector3(1.0f, -4.5f, 2.0f);
		transform.localEulerAngles = new Vector3(0, 180.0f, 0);
		cameraBody.fieldOfView = 60.0f;

		iTween.MoveTo (gameObject, iTween.Hash (
			"x", 0,
			"y", 0,
			"z", 0,
			"islocal", true,
			"time", duration,
			"easetype", iTween.EaseType.easeOutQuad
		));

		iTween.RotateTo (gameObject, iTween.Hash (
			"y", 0,
			"islocal", true,
			"time", duration,
			"easetype", iTween.EaseType.easeOutQuad
		));
	}

	//戦闘決定時に惑星が近くなる演出（実際にはカメラのFovを変えている）
	public void CloseToPlanet(float duration)
	{
		iTween.ValueTo (gameObject, iTween.Hash (
			"from", 60.0f,
			"to", 40.0f,
			"time", duration,
			"onupdate", "OnUpdateValue", 
			"easetype", iTween.EaseType.easeOutQuad
		));
	}

	private void OnUpdateValue(float val)
	{
		cameraBody.fieldOfView = val;
	}
}
