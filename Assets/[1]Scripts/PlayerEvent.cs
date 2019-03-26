using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class MyGOEvent : UnityEvent<GameObject> { }

public class PlayerEvent : MonoBehaviour
{

	[HideInInspector] public MyGOEvent OnCollected;

	private PlayerStats _playerStats;

	private int _score = 0;
	private int _health = 3;

	private void Awake()
	{
		PublicContainer.instance.PlayerEvent = this;
	}

	private void Start()
	{
		_playerStats = GetComponent<PlayerStats>();

		_playerStats.HealthText.text = "Health: " + _health;
		_playerStats.ScoreText.text = "Score: " + 0;
	}

	#region Для событий
	void SetDamage(GameObject obj)
	{
		_health -= 1;
		Destroy(obj, 0.0001f);
		_playerStats.HealthText.text = "Health: " + _health;
	}

	void PickupNeko(GameObject obj)
	{
		_score += 5;
		Destroy(obj, 0.0001f);
		_playerStats.ScoreText.text = "Score: " + _score;
	}

	private void OnGameObjectEvent(GameObject obj)
	{
		if (OnCollected != null)
			OnCollected.Invoke(obj);
	}
	#endregion

	//врезались во врага или собрали киску ?
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			OnGameObjectEvent(collision.gameObject);
			SetDamage(collision.gameObject);
		}

		if (collision.gameObject.tag == "Pickup")
		{
			OnGameObjectEvent(collision.gameObject);
			PickupNeko(collision.gameObject);
		}
	}

	//врезались во врага или собрали киску ?
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			OnGameObjectEvent(collision.gameObject);
			SetDamage(collision.gameObject);
		}

		if (collision.gameObject.tag == "Pickup")
		{
			OnGameObjectEvent(collision.gameObject);
			PickupNeko(collision.gameObject);

		}
	}

}
