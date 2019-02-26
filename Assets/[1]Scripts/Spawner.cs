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

	//Листы объектов на сцене
	private List<GameObject> _nekoList = new List<GameObject>();
	private List<GameObject> _enemyList = new List<GameObject>();

	//Листы занятых спаун поинтов
	private List<Transform> _nekoUsedSpawnPointsList = new List<Transform>();
	private List<Transform> _enemyUsedSpawnPointsList = new List<Transform>();


	private void Start()
	{
		if (NekoCounts > _nekoSpawnPoits.Count || EnemyCounts > _enemySpawnPoits.Count)
			throw new ArgumentOutOfRangeException("Значение возможных объектов не должно быть больше спаун поинтов");

		StartCoroutine("SpawnNeko");
		StartCoroutine("SpawnEnemy");
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

				Debug.Log(_enemySpawnPoits.Count);

				_enemyList.Add(enemy);
				_enemyUsedSpawnPointsList.Add(usedSpawnPoint);
				_enemySpawnPoits.Remove(usedSpawnPoint);
			}

				yield return new WaitForSeconds(3);
		}
	}
}
