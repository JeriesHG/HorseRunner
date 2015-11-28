using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof(ThirdPersonCharacter))]
	public class ThirdPersonUserControl : MonoBehaviour
	{
		private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
		private Transform m_Cam;                  // A reference to the main camera in the scenes transform
		private Vector3 m_CamForward;             // The current forward direction of the camera
		private Vector3 m_Move;
		private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

		public float min_Speed = 1f;
		public float max_Speed = 2f;
		public float acceleration = 0.2f;
		public float repeatRate = 5f;
		public float current_Speed;
		private int speedCycles = 0;

        
		private void Start ()
		{
			current_Speed = min_Speed;
			// get the transform of the main camera
			if (Camera.main != null) {
				m_Cam = Camera.main.transform;
			} else {
				Debug.LogWarning (
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
				// we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
			}


			// get the third person character ( this should never be null due to require component )
			m_Character = GetComponent<ThirdPersonCharacter> ();

			InvokeRepeating ("increaseSpeed", repeatRate, repeatRate);
		}


		private void Update ()
		{
			if (!m_Jump) {
				m_Jump = CrossPlatformInputManager.GetButtonDown ("Jump");
			}
		}


		// Fixed update is called in sync with physics
		private void FixedUpdate ()
		{
			// read inputs
			float h = CrossPlatformInputManager.GetAxis ("Horizontal");

			bool crouch = Input.GetKey (KeyCode.C);

			// calculate move direction to pass to character
			if (m_Cam != null) {
				// calculate camera relative direction to move:
				m_CamForward = Vector3.Scale (m_Cam.forward, new Vector3 (1, 0, 1)).normalized;
				m_Move = current_Speed * m_CamForward + h * m_Cam.right;
			} else {
				// we use world-relative directions in the case of no main camera
				m_Move = current_Speed * Vector3.forward + h * Vector3.right;
			}
			#if !MOBILE_INPUT
			// walk speed multiplier
			if (Input.GetKey (KeyCode.LeftShift))
				m_Move *= 0.5f;
			#endif
			// pass all parameters to the character control script
			m_Character.Move (m_Move, crouch, m_Jump);
			m_Jump = false;

		}

		private void increaseSpeed ()
		{
			if (current_Speed < max_Speed) {
				current_Speed += min_Speed + Time.deltaTime + acceleration;
				m_Character.addAnimSpeedMultiplier (acceleration);
				speedCycles++;
			} else {
				current_Speed = max_Speed;
			}
		}

		public void updateCurrentSpeed (float speed)
		{
			this.current_Speed = speed;
			m_Character.addAnimSpeedMultiplier (-(speedCycles * acceleration));
		}

		public float getCurrentSpeed ()
		{
			return this.current_Speed;
		}
	}
}
