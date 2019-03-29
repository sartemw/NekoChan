using UnityEngine;

public class WallTeleport : MonoBehaviour {

	
	private Collider2D _oppositetWall;
	Vector2 _max;

	[SerializeField] private float _stepPlayer = 1;

	void Start () {
		if (gameObject.name == "WallLeft")
		{
			_oppositetWall = transform.parent.parent.GetComponent<PublicContainer>().RightWall;
			_stepPlayer *= -1;
		}

		if (gameObject.name == "WallRight")
		{
			_oppositetWall = transform.parent.parent.GetComponent<PublicContainer>().LeftWall;
		}
		
		_max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player" && (gameObject.name == "WallLeft" || gameObject.name == "WallRight"))
		{
			collision.gameObject.transform.position = new Vector2(_oppositetWall.transform.position.x + _stepPlayer, collision.gameObject.transform.position.y);
		}
		else collision.gameObject.transform.position = new Vector2(collision.gameObject.transform.position.x, _max.y);
	}

}
