using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class ObstacleCollision : MonoBehaviour
{

	public float base_damage = 0.1f;
	private Vector3 size;

	void Start ()
	{
		size = this.gameObject.GetComponent<Collider> ().bounds.size;
	}
	
	// Update is called once per frame
	void OnCollisionEnter (Collision collision)
	{
		GameObject e = collision.gameObject;
		if (e.tag.Equals ("Player")) {
			float currentSpeed = e.GetComponent<ThirdPersonUserControl> ().getCurrentSpeed ();
			float newSpeed = currentSpeed * (1 / (size.magnitude)) + Time.deltaTime;
			e.GetComponent<ThirdPersonUserControl> ().updateCurrentSpeed (Mathf.Abs (newSpeed));

			float totalDamage = base_damage + (1 / collision.relativeVelocity.magnitude) - (1 / (size.magnitude));
			Debug.Log (totalDamage);
			e.GetComponent<PlayerGUI> ().updateHealthBar (-totalDamage);
			Destroy (this.gameObject);
		}
	}
}
