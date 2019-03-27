using UnityEngine;

public class EnemyController : MonoBehaviour {

	public Transform RespawnPoint;

	private float _directional = 1;

	private void FixedUpdate()
	{
		transform.Translate(_directional * Time.deltaTime, 0, 0);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Board")
		{
			_directional *= -1;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		 if( collision.gameObject.tag == "Enemy")
			_directional *= -1;
	}
}
