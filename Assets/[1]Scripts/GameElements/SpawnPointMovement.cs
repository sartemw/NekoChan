using UnityEngine;

public class SpawnPointMovement : MonoBehaviour {

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
}
