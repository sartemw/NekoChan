using UnityEngine.Events;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MyGOEvent : UnityEvent<GameObject> { }

public class PlayerEvent : MonoBehaviour
{

	[HideInInspector] public MyGOEvent OnCollected;

	private PlayerStats _playerStats;

	private int _score = 0;
	public int Score => _score;
	[SerializeField] private int _health = 3;

	private GameStatistics _statistics;

	private void Start()
	{
		GameStatistics.instance.PlayerEvent = this;
		PublicContainer.instance.PlayerEvent = this;

		_playerStats = GetComponent<PlayerStats>();

		_playerStats.HealthText.text = "Health: " + _health;
		_playerStats.ScoreText.text = "Score: " + 0;

		_statistics = GameStatistics.instance;
	}

	#region Для событий
	public void SetDamage(GameObject obj)
	{
		_health -= 1;
		Destroy(obj, 0.0001f);
		_playerStats.HealthText.text = "Health: " + _health;
		//IsDeath(_health);
	}

	private void IsDeath(int healt)
	{
		if (healt <= 0)
		{
			GameOver();
		}
	}

	public void GameOver()
	{
		_statistics.SaveScore();
		SceneManager.LoadScene("GameOver");
	}

	public void PickupNeko(GameObject obj)
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
