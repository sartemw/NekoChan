using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionPlayerInPlatform : MonoBehaviour {

	private Collider2D _platformCollider2D;

	private void Start()
	{
		_platformCollider2D = GetComponent<Collider2D>();
	}

	//private void OnTriggerEnter2D(Collider2D collision)
	//{
	//	if (collision.tag == "Player" && collision.gameObject.transform.position.y < gameObject.transform.position.y)
	//		_platformCollider2D.isTrigger = true;
	//}

	//private void OnTriggerStay2D(Collider2D collision)
	//{
	//	if (collision.tag == "Player" && collision.bounds.min.y > gameObject.GetComponent<Collider2D>().bounds.max.y)
	//		_platformCollider2D.isTrigger = false;
	//	else _platformCollider2D.isTrigger = true;	
	//}

	//private void OnTriggerExit2D(Collider2D collision)
	//{
	//	if (collision.tag == "Player")
	//	{
	//		Debug.Log("Сибастиан");
	//		_platformCollider2D.isTrigger = true;
	//	}

	//	//Debug.Log("Max col2d" + collision.bounds.max);
	//	//Debug.Log("Min col2d" + collision.bounds.min);
	//	//Debug.Log("Max go" + gameObject.GetComponent<Collider2D>().bounds.max);
	//	//Debug.Log("Min go" + gameObject.GetComponent<Collider2D>().bounds.min);
	//}
}
