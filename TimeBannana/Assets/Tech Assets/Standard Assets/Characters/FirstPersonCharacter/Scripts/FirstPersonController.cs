using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using System.Collections;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

		private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;
		private float JumpCount;
		public bool Left_WallRun;
		public bool Right_WallRun;
		public bool m_Crouched;
		public bool isSliding;
		private float playerSize;
		private CapsuleCollider m_CollisionTrig;
		private bool canRun;
		private float crouchCounter;
		public Vector2 Velocity;
		private float wallRunTimer =0F;

		// Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);
			playerSize = m_CharacterController.height;
			m_CollisionTrig = GetComponent<CapsuleCollider>();
		}
		
		// Update is called once per frame
        private void Update()
        {
			//Velocity= m_CharacterController.velocity.magnitude;
			RotateView ();
			// the jump state needs to read here to make sure it is not missed
			if (!m_Jump) {
				m_Jump = Input.GetButtonDown ("Jump");
			}

			if (!m_PreviouslyGrounded && m_CharacterController.isGrounded) {
				StartCoroutine (m_JumpBob.DoBobCycle ());
				PlayLandingSound ();
				m_MoveDir.y = 0f;
				m_Jumping = false;
			}
			if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded) {
				m_MoveDir.y = 0f;
			}

			m_PreviouslyGrounded = m_CharacterController.isGrounded;

			RaycastHit upCheck;
			// Crouch only when the button is held down
			if (Input.GetButtonDown ("B")) {
				m_Crouched = true;
			} else if (Input.GetButton ("B") == false && Physics.Raycast (transform.position, Vector3.up, out upCheck, 1.0f) == false){
				m_Crouched = false;
			}
			/*Crouch when the button is pressed...must press the button again in order to stand up
			if (Input.GetButtonDown ("B")) {
					if(crouchCounter == 0){
						crouchCounter = 1;
				}else if(crouchCounter == 1 && m_IsWalking == true){
						crouchCounter = 0;
					}
			}
			if (crouchCounter == 1) {
				m_Crouched = true;
			} else {
				m_Crouched = false;
			}
			*/
			if (m_Crouched == true) {
				if(playerSize >1f){
					playerSize = playerSize -0.25f;
				}else{}
				if(m_PreviouslyGrounded == true && m_CharacterController.velocity.magnitude > 7.0f && m_Input.y > 0){
					isSliding = true;
 					StartCoroutine (Slide());
				}else{}
					m_WalkSpeed = 2f;
					canRun = false;
			} else {
				if(playerSize <1.8f){
					playerSize = playerSize +0.25f;
				}else{}
				m_WalkSpeed = 5f;
				isSliding = false;
				canRun = true;
			}
		}

		IEnumerator Slide(){
			yield return new WaitForSeconds (0.3f);
				isSliding = false;
		}

        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }

        private void FixedUpdate()
        {
			m_CharacterController.height = playerSize;
			m_CollisionTrig.height = playerSize + 0.02f;

			float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it's being aimed at
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

			// get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f);
			//Check for a left side Wall
			RaycastHit leftHit;
			if (Physics.Raycast (transform.position,-transform.right, out leftHit, 0.8f) && m_CharacterController.isGrounded == false) {
				Left_WallRun = true;
			} else{
				Left_WallRun = false;
			}
			//Check for a right side Wall
			RaycastHit rightHit;
			if (Physics.Raycast (transform.position,transform.right, out rightHit, 0.8f) && m_CharacterController.isGrounded == false) {
				Right_WallRun = true;
			} else {
				Right_WallRun = false;
			}

			if (Left_WallRun == false && Right_WallRun == false) {
				desiredMove = Vector3.ProjectOnPlane (desiredMove, hitInfo.normal).normalized;
				wallRunTimer = 0;
			} else if(Left_WallRun == true){
				if(wallRunTimer < 10f){
					wallRunTimer = wallRunTimer +0.009f;
				}
				//Reset for one more jump off the wall  "JumpCount = 1;"
				JumpCount = 1;
				m_CharacterController.Move (new Vector3(0f,-wallRunTimer/10f,0f));
				m_MoveDir.y =  leftHit.normal.y;
			
			} else if (Right_WallRun == true){
				if(wallRunTimer < 10f){
						wallRunTimer = wallRunTimer +0.009f;
				}
				//Reset for one more jump off the wall  "JumpCount = 1;"
				JumpCount = 1;
				m_CharacterController.Move (new Vector3(0f,-wallRunTimer/10f,0f));
				m_MoveDir.y =  rightHit.normal.y;
			}
				m_MoveDir.x = desiredMove.x *speed;
				m_MoveDir.z = desiredMove.z * speed;
			
			//Reset Jump Count if Player has hit the ground
			if (m_CharacterController.isGrounded) {
				JumpCount = 0;
			}

			//Check if the Player has jumped less the max jumps allowed (2)
            if (m_Jump && JumpCount <2 && isSliding == false){
				//Check if it is the first or double (second) jump
				if(m_Jump && JumpCount <1){

					/*Check if the Player is wall running or not
					if(Left_WallRun == true){
						m_MoveDir = new Vector3 (transform.right.x +30f,transform.right.y,transform.right.z);
						Left_WallRun = false;
						m_MoveDir.y = m_JumpSpeed -4f;
						PlayJumpSound();
						m_Jump = false;
						m_Jumping = true;
						JumpCount = JumpCount +1;
					}else if (Right_WallRun == true){
						Right_WallRun = false;
						m_MoveDir.y = m_JumpSpeed;
						PlayJumpSound();
						m_Jump = false;
						m_Jumping = true;
						JumpCount = JumpCount +1;
					}else{ */
						m_MoveDir.y = -m_StickToGroundForce;
						m_MoveDir.y = m_JumpSpeed;
						PlayJumpSound();
                    	m_Jump = false;
						m_Jumping = true;
						JumpCount = JumpCount +1;

				}else{
						m_MoveDir.y = -m_StickToGroundForce;
						m_MoveDir.y = m_JumpSpeed -0.6f;
						PlayJumpSound();
						m_Jump = false;
						m_Jumping = true;
						JumpCount = JumpCount +1;
				}
			}else {
				//Make the player fall back to the ground (Activate Gravity)
				m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
				m_Jump = false;
            }
			//Initiate a slide if the requirments are met...(i.e. speed, crouch, etc)
			if (isSliding == false) {
				m_CollisionFlags = m_CharacterController.Move (m_MoveDir * Time.fixedDeltaTime);
			} else {
				m_CollisionFlags = m_CharacterController.Move (m_MoveDir * 0.12f);
			}
			ProgressStepCycle(speed);
            UpdateCameraPosition(speed);
        }


        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }
	
		//Keeps track of the player's steps and activates the corresponding "HeadBob" to match
        private void ProgressStepCycle(float speed)
        {
			if (isSliding == false) {
				if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0)) {
					m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunstepLenghten))) *
						Time.fixedDeltaTime;
				}

				if (!(m_StepCycle > m_NextStep)) {
					return;
				}

				m_NextStep = m_StepCycle + m_StepInterval;

				PlayFootStepAudio ();
			} else {}
        }

        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded && isSliding == false)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
			if (m_Crouched == true) {
				newCameraPosition.y = newCameraPosition.y -0.2f;
			}
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
			// Read input
			float deadzone = 0.2f;
			Vector2 stickInput = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
			if (stickInput.magnitude < deadzone){
				stickInput = Vector2.zero;
			}else {
				stickInput = stickInput.normalized * ((stickInput.magnitude - deadzone) / (1 - deadzone));
			}

            bool waswalking = m_IsWalking;

			#if !MOBILE_INPUT
            // keep track of whether the character is walking or running
            if(Input.GetButton ("Sprint") && m_PreviouslyGrounded == true && canRun ==true){
				m_IsWalking = false;
				if(m_RunSpeed <9.0f && m_Input.y >0){
					m_RunSpeed = m_RunSpeed +0.15f;
				}
			}else{
				if(m_PreviouslyGrounded == true){
					m_IsWalking = true;
					m_RunSpeed =6f;
				}
			}
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
			if (stickInput.x > 0 && stickInput.y > 0 ||stickInput.x < 0 && stickInput.y < 0 ||
			    stickInput.x >0 && stickInput.y <0 ||stickInput.x <0 && stickInput.y >0){
				speed *= stickInput.magnitude/1.4f;
			} else {
				speed *= stickInput.magnitude;
			}
            m_Input = new Vector2(stickInput.x, stickInput.y);
			Velocity = m_Input;

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }


            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }

        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
}
