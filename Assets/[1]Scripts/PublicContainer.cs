using UnityEngine.Events;
using UnityEngine;

public class PublicContainer : MonoBehaviour {

	#region Singlton
	public static PublicContainer instance = null;
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

	public PlayerEvent PlayerEvent;

	private void Awake()
	{
		RunSinglton();
	}
}
