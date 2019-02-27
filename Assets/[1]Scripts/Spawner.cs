using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class Spawner : MonoBehaviour {

	#region Singlton
	public static Spawner instance = null;
	//RunSinglton надо добавить в Start
	private void RunSinglton()
	{
		if (instance == null)
		{ // Экземпляр менеджера был найден
			instance = this; // Задаем ссылку на экземпляр объекта
		}
		else if (instance == this)
		{ // Экземпляр объекта уже существует на сцене
			Destroy(gameObject); // Удаляем объект
		}

		// Теперь нам нужно указать, чтобы объект не уничтожался
		// при переходе на другую сцену игры
		DontDestroyOnLoad(gameObject);
	}
	#endregion

	[Header ("Возможное колличество объектов на сцене")]
	public int NekoCounts;
	public int EnemyCounts;
	[Space]
	[SerializeField] private List<Transform> _nekoSpawnPoits = new List<Transform>();
	[SerializeField] private List<Transform> _enemySpawnPoits = new List<Transform>();
	[Space]
	[SerializeField] private GameObject _neko;
	[SerializeField] private GameObject _enemy;

	[HideInInspector] public EventManager _eventManager;

	//Листы объектов на сцене
	private List<GameObject> _nekoList = new List<GameObject>();
	private List<GameObject> _enemyList = new List<GameObject>();

	//Листы занятых спаун поинтов
	private List<Transform> _nekoUsedSpawnPointsList = new List<Transform>();
	private List<Transform> _enemyUsedSpawnPointsList = new List<Transform>();

	private void Awake()
	{
		RunSinglton();
	}

	private void Start()
	{
		if (NekoCounts > _nekoSpawnPoits.Count || EnemyCounts > _enemySpawnPoits.Count)
			throw new ArgumentOutOfRangeException("Значение возможных объектов не должно быть больше спаун поинтов");		

		StartCoroutine("SpawnNeko");
		StartCoroutine("SpawnEnemy");
	}
	void OnEnable()
	{
		_eventManager.OnCollected.AddListener(NewSpawn);
	}

	void OnDisable()
	{
		_eventManager.OnCollected.RemoveListener(NewSpawn);
	}
	//Если список котов меньше максимально возможного, то
	//выбираем случайную точку
	//добавляем кота на сцену в этой точке
	//добавляем кота в список котов на сцене
	//добавляем выбранный спаун поинт в список использованных поинтов
	//убираем выбранный спаун поин из списка доступных
	IEnumerator SpawnNeko()
	{
		while (true)
		{
			if (_nekoList.Count < NekoCounts)
			{
				Transform usedSpawnPoint = _nekoSpawnPoits[UnityEngine.Random.Range(0, _nekoSpawnPoits.Count)];

				GameObject neko = Instantiate(_neko, usedSpawnPoint.position, Quaternion.identity);

				_nekoList.Add(neko);
				_nekoUsedSpawnPointsList.Add(usedSpawnPoint);
				_nekoSpawnPoits.Remove(usedSpawnPoint);
			}

			yield return new WaitForSeconds(3);
		}
	}

	//Аналогичная работа с SpawnNeko
	IEnumerator SpawnEnemy()
	{
		while (true)
		{
			if (_enemyList.Count < NekoCounts)
			{
				Transform usedSpawnPoint = _enemySpawnPoits[UnityEngine.Random.Range(0, _enemySpawnPoits.Count)];

				GameObject enemy = Instantiate(_enemy, usedSpawnPoint.position, Quaternion.identity);

				_enemyList.Add(enemy);
				_enemyUsedSpawnPointsList.Add(usedSpawnPoint);
				_enemySpawnPoits.Remove(usedSpawnPoint);
			}

				yield return new WaitForSeconds(3);
		}
	}

	
	public void NewSpawn(GameObject obj)
	{
		Debug.Log("GUTCHE!!! " + obj);
	}

	
}
