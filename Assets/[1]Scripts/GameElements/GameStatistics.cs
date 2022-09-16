using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatistics : MonoBehaviour
{
	#region Singlton
	public static GameStatistics instance = null;
	//RunSinglton ���� �������� � Start
	private void RunSinglton()
	{
		if (instance == null)
		{ // ��������� ��������� ��� ������
			instance = this; // ������ ������ �� ��������� �������
		}
		else if (instance == this)
		{ // ��������� ������� ��� ���������� �� �����
			Destroy(gameObject); // ������� ������
		}

		// ������ ��� ����� �������, ����� ������ �� �����������
		// ��� �������� �� ������ ����� ����
		DontDestroyOnLoad(gameObject);
	}
	#endregion

	public PlayerEvent PlayerEvent;

	[SerializeField] private int _score;
	[SerializeField] private bool _unlimitedGame = false;
	private void Awake()
	{
		RunSinglton();
	}

	public void SaveScore()
	{
		_score = PlayerEvent.Score;
	}

}
