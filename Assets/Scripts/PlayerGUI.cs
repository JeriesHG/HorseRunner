using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour
{

	//Size of Textures
	Vector2 size = new Vector2 (120, 20);
	
	//Health Variables
	[SerializeField]
	private Vector2
		healthPos = new Vector2 (20, 20);
	[SerializeField]
	private float
		healthBarDisplay = 1f;
	[SerializeField]
	private Texture2D
		healthBarEmpty;
	[SerializeField]
	private Texture2D
		healthBarFull;

	//Increase
	[SerializeField]
	private int
		healthFallRate = 150;

	void OnGUI ()
	{
		//Health GUI
		GUI.BeginGroup (new Rect (healthPos.x, healthPos.y, size.x, size.y));
		GUI.Box (new Rect (0, 0, size.x, size.y), healthBarEmpty);
		
		GUI.BeginGroup (new Rect (0, 0, size.x * healthBarDisplay, size.y));
		GUI.Box (new Rect (0, 0, size.x, size.y), healthBarFull);
		
		GUI.EndGroup ();
		GUI.EndGroup ();
	}

	void Update ()
	{
		if (healthBarDisplay >= 0 && healthBarDisplay <= 1) {
			healthBarDisplay += Time.deltaTime / healthFallRate * 2;
		}
	}

	public void updateHealthBar (float amount)
	{
		healthBarDisplay += amount;

	}
}
