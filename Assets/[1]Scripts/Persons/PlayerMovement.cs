using System.Collections;
using UnityEngine;

//Управление игроком
public class PlayerMovement : MonoBehaviour {

	Rigidbody2D _rigidbody2D;
	PlayerStats _playerStats;
	SpriteRenderer _spriteRenderer;

	[SerializeField] private bool _isGround = true;

	private void Start()
	{
		_playerStats = GetComponent<PlayerStats>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void FixedUpdate ()
	{
		Move();
		Jump();	
	}

	private bool IsGround()
	{
		RaycastHit2D hit = Physics2D.Raycast(_rigidbody2D.position, Vector2.down, 0.6f, LayerMask.GetMask("Ground"));

		if (hit.collider != null)
		{
			_isGround = true;
		}
		else
		{
			_isGround = false;
		}
		return _isGround;
	}

	//движение по горизонтали
	private void Move()
	{
		float horizontalMovement = Input.GetAxis("Horizontal") * _playerStats.Speed / 10 * Time.deltaTime;
		transform.Translate(horizontalMovement, 0, 0);

		if (horizontalMovement > 0)
			_spriteRenderer.flipX = false;
		else _spriteRenderer.flipX = true;
	}

	//прыжок нажат и мы не на земле, включаем триггер
	private void Jump()
	{
		if (Input.GetButtonDown("Jump") && IsGround() == true)
		{
			StartCoroutine(Jumping(transform.position.y + _playerStats.JumpHight / 10));
			
			//_rigidbody2D.AddForce(new Vector2(0, _playerStats.JumpHight), ForceMode2D.Impulse);
			_isGround = false;
		}
	}

	IEnumerator Jumping(float hight)
	{
		while (transform.position.y < hight)
		{
			transform.Translate(0, _playerStats.JumpSpeed * Time.deltaTime, 0);
			yield return null;
		}
	}
}
