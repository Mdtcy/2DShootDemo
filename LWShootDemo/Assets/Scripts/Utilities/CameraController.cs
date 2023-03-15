/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月10日
 * @modify date 2023年3月10日
 * @desc [相机控制器]
 */

#pragma warning disable 0649
using UnityEngine;

namespace LWShootDemo.Common
{
	/// <summary>
	/// 相机控制器
	/// </summary>
	public class CameraController : MonoBehaviour
	{
		#region FIELDS

		[SerializeField]
		private float cameraDist = 3.5f;

		[SerializeField]
		private float smoothTime = 0.2f;

		// * local
		private Transform player;
		private Camera    mainCamera;

		private Vector3 target;
		private Vector3 mousePos;
		private Vector3 refVel;
		private Vector3 shakeOffset;
		private float   zStart;

		// shake
		private float shakeMag;
		private float shakeTimeEnd;
		Vector3       shakeVector;
		private bool  shaking;

		#endregion

		#region PROPERTIES

		#endregion

		#region PUBLIC METHODS

		public void Shake(Vector3 direction, float magnitude, float length)
		{
			shaking      = true;
			shakeVector  = direction;
			shakeMag     = magnitude;
			shakeTimeEnd = Time.time + length;
		}

		#endregion

		#region PROTECTED METHODS

		#endregion

		#region PRIVATE METHODS

		private void Start()
		{
			player     = GameManager.Instance.Player;
			mainCamera = GameManager.Instance.MainCamera;

			target     = player.position;
			zStart     = transform.position.z;
		}

		private void Update()
		{
			if (player == null)
			{
				return;
			}

			mousePos    = CaptureMousePos();
			shakeOffset = UpdateShake();
			target      = UpdateTargetPos();
		}

		private void FixedUpdate()
		{
			if (player == null)
			{
				return;
			}

			UpdateCameraPosition();
		}

		// 获取鼠标位置
		private Vector3 CaptureMousePos()
		{
			Vector2 ret = mainCamera.ScreenToViewportPoint(Input.mousePosition);
			ret *= 2;
			ret -= Vector2.one;
			float max = 0.9f;

			if (Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max)
			{
				ret = ret.normalized;
			}

			return ret;
		}

		// 更新目标位置
		private Vector3 UpdateTargetPos()
		{
			Vector3 mouseOffset = mousePos * cameraDist;
			Vector3 ret         = player.position + mouseOffset;
			ret   += shakeOffset;
			ret.z =  zStart;

			return ret;
		}

		// 更新震动偏移
		private Vector3 UpdateShake()
		{
			if (!shaking || Time.time > shakeTimeEnd)
			{
				shaking = false;

				return Vector3.zero;
			}

			Vector3 tempOffset = shakeVector;
			tempOffset *= shakeMag;

			return tempOffset;
		}

		// 更新相机位置
		private void UpdateCameraPosition()
		{
			Vector3 tempPos = Vector3.SmoothDamp(transform.position, target, ref refVel, smoothTime);
			transform.position = tempPos;
		}


		#endregion

		#region STATIC METHODS

		#endregion
	}
}
#pragma warning restore 0649