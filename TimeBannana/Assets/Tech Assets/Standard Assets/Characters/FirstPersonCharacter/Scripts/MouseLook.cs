using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        public bool clampVerticalRotation = true;
        public float MinimumX = -90F;
        public float MaximumX = 90F;
        public bool smooth;
        public float smoothTime = 5f;
		public int i = 0;

		void Update (){

			Debug.Log(Input.GetJoystickNames()[i] + " is moved");
		}
		
		private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;

        public void Init(Transform character, Transform camera)
        {
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;
        }

		public float yRot;
		public float xRot;
        public void LookRotation(Transform character, Transform camera)
        {
			float deadzone = 0.1f;

				yRot = Input.GetAxis ("Mouse X") * XSensitivity;
				xRot = Input.GetAxis ("Mouse Y") * YSensitivity;

			Vector2 stickInput = new Vector2 (xRot, yRot);
			stickInput = stickInput.normalized * ((stickInput.magnitude - deadzone) / (1 - deadzone));

			if (stickInput.magnitude < deadzone){
				stickInput = Vector2.zero;
			}
			if (stickInput.magnitude > deadzone){
            	m_CharacterTargetRot *= Quaternion.Euler (0f, stickInput.y, 0f);
            	m_CameraTargetRot *= Quaternion.Euler (-stickInput.x, 0f, 0f);
			}
            if(clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

            if(smooth)
            {
                character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
                    smoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
                    smoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = m_CharacterTargetRot;
                camera.localRotation = m_CameraTargetRot;
            }
        }


        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

    }
}
