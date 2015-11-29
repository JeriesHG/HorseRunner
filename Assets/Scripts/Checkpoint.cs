using UnityEngine;
using System.Collections;

// Sets the respawn location of a player when trigger
public class Checkpoint : MonoBehaviour
{
	void OnTriggerEnter (Collider collision)
	{
		if ((collision.gameObject.tag == "Player") && (collision.gameObject.GetComponent<Health> () != null)) {
			collision.gameObject.GetComponent<PlayerHealth> ().updateRespawn (collision.gameObject.transform.position, collision.gameObject.transform.rotation);
		}
	}
}
