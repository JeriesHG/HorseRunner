using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class Destroy : MonoBehaviour
{

	void OnCollisionEnter (Collision test)
	{
		if (test.gameObject.tag.Equals ("Player")) {
			ThirdPersonUserControl c = test.gameObject.GetComponent<ThirdPersonUserControl> ();
			c.updateCurrentSpeed (0);
			Destroy (gameObject);
		}
	}
}
