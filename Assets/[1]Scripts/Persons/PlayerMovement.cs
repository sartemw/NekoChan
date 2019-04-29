using UnityEngine;

//Управление игроком
public class PlayerMovement : MonoBehaviour {

	Rigidbody2D _rigidbody2D;
	Collider2D _collider2D;
	PlayerStats _playerStats;
	SpriteRenderer _spriteRenderer;

	private bool _isGrounded = false;
	private bool _inStayPlatform = false;
	private float _maxJampingPoint = 0;

	private void Start()
	{
		_playerStats = GetComponent<PlayerStats>();
		_collider2D = GetComponent<Collider2D>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		//если игрок идет на снежение и не находится в платформе, то выключаем триггер
		if ((transform.position.y < _maxJampingPoint) && (_inStayPlatform == false))
		{
			_collider2D.isTrigger = false;
		}
	}

	private void FixedUpdate ()
	{
		//движение по горизонтали
		float horizontalMovement = Input.GetAxis("Horizontal") * _playerStats.Speed/10 * Time.deltaTime;
		transform.Translate(horizontalMovement, 0, 0);

		if (horizontalMovement > 0)
			_spriteRenderer.flipX = false;
		else _spriteRenderer.flipX = true;

		//прыжок нажат и мы не на земле, включаем триггер
		if (Input.GetButtonDown("Jump") && _isGrounded == true)
		{
			_rigidbody2D.AddForce(Vector2.up * _playerStats.JumpHight*10);
			_isGrounded = false;
			_collider2D.isTrigger = true;
		}		
	}

	private void LateUpdate()
	{
		//запоминаем позицию для определения восхождения/падения
		_maxJampingPoint = transform.position.y + 0.0005f;
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		//мы стоим на чем-то ? 
		if (collision.gameObject.tag == "Untagged")
		{
			_isGrounded = true;
		}		
	}
	
	private void OnTriggerStay2D(Collider2D collision)
	{
		//мы стоим в платформе ?
		if (collision.gameObject.tag == "Untagged")
		{
			_inStayPlatform = true;
		}		
		else
		{
			_inStayPlatform = false;
		}
	}
}
