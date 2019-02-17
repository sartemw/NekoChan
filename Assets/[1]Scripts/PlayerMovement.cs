using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	Rigidbody2D _rigidbody2D;
	Collider2D _collider2D;

	[SerializeField]	private int _speed;
	[SerializeField]    private int _jumpSpeed;

	private bool _isGrounded = false;
	private float _maxJampingPoint = 0;

	private void Start()
	{
		_collider2D = GetComponent<Collider2D>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (transform.position.y < _maxJampingPoint)
		{
			_collider2D.isTrigger = false;
		}
	}

	private void FixedUpdate ()
	{
		float horizontalMovement = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
		transform.Translate(horizontalMovement, 0, 0);		

		if (Input.GetButtonDown("Jump") && _isGrounded == true)
		{
			_rigidbody2D.AddForce(Vector2.up * _jumpSpeed * 100);
		}			
	}

	private void LateUpdate()
	{
		_maxJampingPoint = transform.position.y + 0.01f;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Untagged")
		{
			_isGrounded = false;
			_collider2D.isTrigger = true;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Untagged")
		{
			_isGrounded = true;
		}		
	}
}
