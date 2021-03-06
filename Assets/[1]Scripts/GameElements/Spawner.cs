﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class Spawner : MonoBehaviour {
		
	[Header ("Возможное колличество объектов на сцене")]
	public int NekoCounts;
	public int EnemyCounts;
	[Space]
	[SerializeField] private List<Transform> _nekoSpawnPoits = new List<Transform>();
	[SerializeField] private List<Transform> _enemySpawnPoits = new List<Transform>();
	[Space]
	[SerializeField] private GameObject _neko;
	[SerializeField] private GameObject _enemy;

	private PlayerEvent _playerEvent;

	//подъем уровня сложности.
	private int _pickupNekos = 0;

	private int _commonNekoSpawnPoints, _commonEnemySpawnPoints;

	//Листы объектов на сцене
	private List<GameObject> _nekoList = new List<GameObject>();
	private List<GameObject> _enemyList = new List<GameObject>();

	//Листы занятых спаун поинтов
	private List<Transform> _nekoUsedSpawnPointsList = new List<Transform>();
	private List<Transform> _enemyUsedSpawnPointsList = new List<Transform>();

	
	private void Start()
	{
		_playerEvent = GetComponent<PublicContainer>().PlayerEvent;
		if (NekoCounts > _nekoSpawnPoits.Count || EnemyCounts > _enemySpawnPoits.Count)
			throw new ArgumentOutOfRangeException("Значение возможных объектов не должно быть больше спаун поинтов");
		_commonEnemySpawnPoints = _enemySpawnPoits.Count;
		_commonNekoSpawnPoints = _nekoSpawnPoits.Count;

		// подписываемся / отписываемся на/с событие	
		_playerEvent.OnCollected.AddListener(NewSpawn);

		StartCoroutine("SpawnNeko");
		StartCoroutine("SpawnEnemy");
	}	
	
	void OnDisable()
	{
		_playerEvent.OnCollected.RemoveListener(NewSpawn);
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

				neko.GetComponent<NekoController>().RespawnPoint = usedSpawnPoint;

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
			yield return new WaitForSeconds(3);

			if (_enemyList.Count < EnemyCounts)
			{
				Transform usedSpawnPoint = _enemySpawnPoits[UnityEngine.Random.Range(0, _enemySpawnPoits.Count)];

				GameObject enemy = Instantiate(_enemy, usedSpawnPoint.position, Quaternion.identity);

				enemy.GetComponent<EnemyController>().RespawnPoint = usedSpawnPoint;

				_enemyList.Add(enemy);
				_enemyUsedSpawnPointsList.Add(usedSpawnPoint);
				_enemySpawnPoits.Remove(usedSpawnPoint);
			}				
		}
	}

	//когда событие произошло, смотри что у нас за obj
	//потом возвращаем точку спауна в доступные
	//и удаляем ее из используемых
	//после удаляем obj из объектов на сцене
	public void NewSpawn(GameObject obj)
	{
		if (obj.GetComponent<NekoController>())
		{
			Transform point = obj.GetComponent<NekoController>().RespawnPoint;
			_nekoSpawnPoits.Add(point);
			_nekoUsedSpawnPointsList.Remove(point);

			_nekoList.Remove(obj);

			//Поднимаем сложность
			_pickupNekos++;
			if (_pickupNekos == 3)
			{
				int startCoroutineCount = 0;
				_pickupNekos = 0;

				if (NekoCounts < _commonNekoSpawnPoints)
				{
					NekoCounts += 2;
					if (startCoroutineCount == 0)
					{
						startCoroutineCount++;
						StartCoroutine("SpawnNeko");
					}
				}
				if (EnemyCounts < _commonEnemySpawnPoints)
				EnemyCounts += 1;
			}
		}

		if (obj.GetComponent<EnemyController>())
		{
			Transform point = obj.GetComponent<EnemyController>().RespawnPoint;
			_enemySpawnPoits.Add(point);
			_enemyUsedSpawnPointsList.Remove(point);

			_enemyList.Remove(obj);
		}
	}
}
