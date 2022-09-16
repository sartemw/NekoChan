using System.Collections;
using UnityEngine;

//Управление игроком
public class PlayerMovement : MonoBehaviour {

	Rigidbody2D _rigidbody2D;
	PlayerStats _playerStats;
	SpriteRenderer _spriteRenderer;

	[SerializeField] private bool _isGround = true;
	[SerializeField] private bool _isFall = true;

	private void Start()
	{
		_playerStats = GetComponent<PlayerStats>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		Jump();
	}
	private void FixedUpdate ()
	{
		Move();
	}

	//движение по горизонтали
	#region
	private void Move()
	{
		float horizontalMovement = Input.GetAxis("Horizontal") * _playerStats.Speed / 10 * Time.deltaTime;
		transform.Translate(horizontalMovement, 0, 0);

		if (horizontalMovement > 0)
			_spriteRenderer.flipX = false;
		else _spriteRenderer.flipX = true;
	}
	#endregion

	//прыжок
	#region
	private void Jump()
	{
		if (Input.GetButtonDown("Jump") && _isGround == true && _isFall == true)
		{
			_isFall = false;
			_isGround = false;
			StartCoroutine(Jumping(transform.position.y + _playerStats.JumpHight / 10));
		}
	}

	IEnumerator Jumping(float hight)
	{

		while (transform.position.y < hight)
		{	
			var currentPosY = transform.position.y;
			transform.Translate(0, _playerStats.JumpSpeed * Time.deltaTime, 0);
			if (currentPosY > transform.position.y) break;
			yield return null;
		}
		_isFall = true;
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.collider.tag == "Ground" && _isFall == true)
		{
			var yMax = collision.gameObject.GetComponent<SpriteRenderer>().bounds.max.y;
			var yMin = gameObject.GetComponent<SpriteRenderer>().bounds.min.y;
			if ((yMax - yMin) < 0.2f)
				_isGround = true;
			
		}
	}
	#endregion
}
