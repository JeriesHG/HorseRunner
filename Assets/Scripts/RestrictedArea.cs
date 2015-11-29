using UnityEngine;
using System.Collections;

public class RestrictedArea : MonoBehaviour
{

	void OnTriggerEnter (Collider collider)
	{
		GameObject e = collider.gameObject;
		if (e.tag.Equals ("Player")) {
			e.GetComponent<PlayerHealth> ().updateHealthBar (-1.01f);
		}
	}
}
