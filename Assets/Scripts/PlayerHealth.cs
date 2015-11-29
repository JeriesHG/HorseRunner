using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public enum DeathAction
	{
		loadLevelWhenDead,
		doNothingWhenDead
	}
	;

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
	[SerializeField]
	private int
		healthFallRate = 150;

	private bool 
		isAlive = true;	
	private Vector3 
		respawnPosition;
	private Quaternion 
		respawnRotation;
	public int 
		numberOfLives = 1;				
	public GameObject 
		explosionPrefab;
	public DeathAction 
		onLivesGone = DeathAction.doNothingWhenDead;
	public string 
		levelToLoad ;
	private float 
		respawnHealthPoints = 1f;


	void Start ()
	{
		// store initial position as respawn location
		respawnPosition = transform.position;
		respawnRotation = transform.rotation;
		if (levelToLoad == null) { // default to current scene
			levelToLoad = Application.loadedLevelName;
		}
	}

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
		if (healthBarDisplay <= 0) {				// if the object is 'dead'
			if (explosionPrefab != null) {
				Instantiate (explosionPrefab, transform.position, Quaternion.identity);
			}
			
			if (numberOfLives > 0) { // respawn
				healthBarDisplay = respawnHealthPoints;
				Debug.Log ("Resetting hp to :" + respawnHealthPoints + "- " + healthBarDisplay);
				transform.position = respawnPosition;	// reset the player to respawn position
				transform.rotation = respawnRotation;
				numberOfLives--;
				// give the player full health again
			} else { // here is where you do stuff once ALL lives are gone)
				isAlive = false;
				
				switch (onLivesGone) {
				case DeathAction.loadLevelWhenDead:
					Application.LoadLevel (levelToLoad);
					break;
				case DeathAction.doNothingWhenDead:
					// do nothing, death must be handled in another way elsewhere
					break;
				}
				Destroy (gameObject);
			}
		} else {
			if (healthBarDisplay >= 0 && healthBarDisplay <= 1) {
				healthBarDisplay += Time.deltaTime / healthFallRate * 2;
			}
		}


	}

	public void updateHealthBar (float amount)
	{
		healthBarDisplay += amount;

	}

	public void applyBonusLife (int amount)
	{
		numberOfLives = numberOfLives + amount;
	}
	
	public void updateRespawn (Vector3 newRespawnPosition, Quaternion newRespawnRotation)
	{
		respawnPosition = newRespawnPosition;
		respawnRotation = newRespawnRotation;
	}
}
