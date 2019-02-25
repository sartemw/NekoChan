using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField] private Transform[] _nekoSpawnPoits;
	[SerializeField] private Transform[] _enemySpawnPoits;
	[Space]
	[SerializeField] private GameObject _neko;
	[SerializeField] private GameObject _enemy;

	private List<GameObject> _nekoList = new List<GameObject>();
	private List<GameObject> _enemyList = new List<GameObject>();


	private void Start()
	{
		StartCoroutine("SpawnNeko");
		StartCoroutine("SpawnEnemy");
	}

	IEnumerator SpawnNeko()
	{
		while (true)
		{
			if (_nekoList.Count < 6)
			{ 
				GameObject neko = Instantiate(_neko, _nekoSpawnPoits[Random.Range(0, _nekoSpawnPoits.Length)]);
				_nekoList.Add(neko);
			}

			yield return new WaitForSeconds(3);
		}
	}

	IEnumerator SpawnEnemy()
	{
		while (true)
		{
			if (_nekoList.Count < 4)
			{
				GameObject enemy = Instantiate(_enemy, _enemySpawnPoits[Random.Range(0, _enemySpawnPoits.Length)]);
				_enemyList.Add(enemy);
			}
			
			yield return new WaitForSeconds(3);
		}
	}
}
