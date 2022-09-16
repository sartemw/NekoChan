using UnityEngine;
using UnityEngine.Events;

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

		//// Теперь нам нужно указать, чтобы объект не уничтожался
		//// при переходе на другую сцену игры
		//DontDestroyOnLoad(gameObject);
	}
	#endregion

	public PlayerEvent PlayerEvent;

	public Collider2D LeftWall;
	public Collider2D RightWall;
	public Collider2D UnderFloar;

	private void Awake()
	{
		RunSinglton();
		
		LeftWall = transform.GetChild(0).GetChild(0).GetComponentInChildren<Collider2D>();
		RightWall = transform.GetChild(0).GetChild(1).GetComponentInChildren<Collider2D>();
		UnderFloar = transform.GetChild(0).GetChild(2).GetComponentInChildren<Collider2D>();
	}
}
