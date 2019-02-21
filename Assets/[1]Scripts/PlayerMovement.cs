using UnityEngine;
using UnityEngine.UI;


//Управление игроком
public class PlayerMovement : MonoBehaviour {
	Rigidbody2D _rigidbody2D;
	Collider2D _collider2D;

	public int Speed;
	public int JumpHight;

	[SerializeField] private Text HealthText;
	[SerializeField] private Text ScoreText;

	private int _score = 0;
	private int _health = 3;

	private bool _isGrounded = false;
	private bool _inStayPlatform = false;
	private float _maxJampingPoint = 0;

	private void Start()
	{
		_collider2D = GetComponent<Collider2D>();
		_rigidbody2D = GetComponent<Rigidbody2D>();

		HealthText.text = "Health: " + _health;
		ScoreText.text = "Score: " + 0;
	}

	private void Update()
	{
		//если игрок идет на снежение и не находится в платформе, то выключаем триггер
		if ((transform.position.y < _maxJampingPoint) && (_inStayPlatform == true))
		{
			_collider2D.isTrigger = false;
		}
	}

	private void FixedUpdate ()
	{
		//движение по горизонтали
		float horizontalMovement = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
		transform.Translate(horizontalMovement, 0, 0);		

		//прыжок нажат и мы не на земле, включаем триггер
		if (Input.GetButtonDown("Jump") && _isGrounded == true)
		{
			_rigidbody2D.AddForce(Vector2.up * JumpHight * 100);
			_isGrounded = false;
			_collider2D.isTrigger = true;
		}		
	}

	private void LateUpdate()
	{
		//запоминаем позицию для определения восхождения/падения
		_maxJampingPoint = transform.position.y + 0.001f;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//врезались воврага ?
		if (collision.gameObject.tag == "Enemy")
		{
			_health -= 1;
			Destroy(collision.gameObject);
			HealthText.text = "Health: " + _health;
		}

		//собрали кису ?
		if (collision.gameObject.tag == "Pickup")
		{
			_score += 5;
			Destroy(collision.gameObject);
			ScoreText.text = "Score: " + _score;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		//врезались воврага ?
		if (collision.gameObject.tag == "Enemy")
		{
			_health -= 1;
			Destroy(collision.gameObject);
			HealthText.text = "Health: " + _health;
		}

		//собрали кису ?
		if (collision.gameObject.tag == "Pickup")
		{
			_score += 5;
			Destroy(collision.gameObject);
			ScoreText.text = "Score: " + _score;
		}
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
	}
	//мы вышли из платформы ?
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Untagged")
		{
			_inStayPlatform = false;
		}
	}
}
