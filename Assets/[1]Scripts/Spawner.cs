using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField] private Transform[] _nekoSpawnPoits;
	[SerializeField] private Transform[] _enemySpawnPoits;
	[Space]
	[SerializeField] private GameObject _neko;
	[SerializeField] private GameObject _enemy;

	private void Start()
	{
		StartCoroutine("SpawnNeko");
		StartCoroutine("SpawnEnemy");
	}

	IEnumerator SpawnNeko()
	{
		while (true)
		{
			Instantiate(_neko, _nekoSpawnPoits[Random.Range(0, _nekoSpawnPoits.Length)]);
			yield return new WaitForSeconds(3);
		}
	}

	IEnumerator SpawnEnemy()
	{
		while (true)
		{
			Instantiate(_enemy, _enemySpawnPoits[Random.Range(0, _enemySpawnPoits.Length)]);
			yield return new WaitForSeconds(3);
		}
	}
}
