using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	[SerializeField] private int _speed;
	[SerializeField] private int _jumpHight;
	[SerializeField] private int _jumpSpeed;
	public int Speed => _speed;
	public int JumpHight => _jumpHight;
	public int JumpSpeed => _jumpSpeed;

	public Text HealthText;
	public Text ScoreText;
}
