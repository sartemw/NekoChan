using UnityEngine;

public class WallTeleport : MonoBehaviour {

	private Collider2D _thisWall;
	private Collider2D _oppositetWall;

	void Start () {
		if (gameObject.name == "WallLeft")
		{
			_thisWall = transform.parent.GetComponent<PublicContainer>().LeftWall;
			_oppositetWall = transform.parent.GetComponent<PublicContainer>().RightWall;
		}

		if (gameObject.name == "WallRight")
		{
			_thisWall = transform.parent.GetComponent<PublicContainer>().LeftWall;
			_oppositetWall = transform.parent.GetComponent<PublicContainer>().RightWall;
		}

		if (gameObject.name == "Floar")
		{
			_thisWall = transform.parent.GetComponent<PublicContainer>().Floar;
			_oppositetWall = transform.parent.GetComponent<PublicContainer>().RightWall;
		}
	}
	
}
