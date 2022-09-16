using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatistics : MonoBehaviour
{
	#region Singlton
	public static GameStatistics instance = null;
	//RunSinglton надо добавить в Start
	private void RunSinglton()
	{
		if (instance == null)
		{ // Ёкземпл€р менеджера был найден
			instance = this; // «адаем ссылку на экземпл€р объекта
		}
		else if (instance == this)
		{ // Ёкземпл€р объекта уже существует на сцене
			Destroy(gameObject); // ”дал€ем объект
		}

		// “еперь нам нужно указать, чтобы объект не уничтожалс€
		// при переходе на другую сцену игры
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
