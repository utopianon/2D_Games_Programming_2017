using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
	public class GameObjectPool : MonoBehaviour
	{
		// The initial size of the pool.
		[SerializeField]
		private int _poolSize;

		// The prefab from which all objects in the pool are instantiated.
		[SerializeField]
		private GameObject _objectPrefab;

		// When the pool runs out of objects, should the pool grow or just
		// return null.
		[SerializeField]
		private bool _shouldGrow;

		// The list containing all the objects in this pool.
		private List<GameObject> _pool;

		protected void Awake()
		{
			// Initialize the pool by adding '_poolSize' amount of objects to the pool.
			_pool = new List<GameObject>(_poolSize);

			for(int i = 0; i < _poolSize; ++i)
			{
				AddObject();
			}
		}

		/// <summary>
		/// Adds an object to the pool.
		/// </summary>
		/// <param name="isActive">Should the object be active when it is added to the pool or not.</param>
		/// <returns>The object added to the pool.</returns>
		private GameObject AddObject( bool isActive = false )
		{
			GameObject go = Instantiate(_objectPrefab);

			if(isActive)
			{
				Activate(go);
			}
			else
			{
				Deactivate(go);
			}

			_pool.Add(go);

			return go;
		}

		/// <summary>
		/// Called when the object is returned to the pool. Deactivates the object.
		/// </summary>
		/// <param name="go">Object to deactivate</param>
		protected virtual void Deactivate(GameObject go)
		{
			go.SetActive(false);
		}

		/// <summary>
		/// Called when the object is fetched from the pool. Activates the object.
		/// </summary>
		/// <param name="go">Object to activate</param>
		protected virtual void Activate(GameObject go)
		{
			go.SetActive(true);
		}

		/// <summary>
		/// Fetches the object form the pool.
		/// </summary>
		/// <returns>An object from the pool or if all objects are already in use and pool cannot grow, returns null</returns>
		public GameObject GetPooledObject()
		{
			GameObject result = null;
			for (int i = 0; i < _pool.Count; i++)
			{
				if ( _pool[i].activeSelf == false )
				{
					result = _pool[i];
					break; // Jumps out from the loop.
				}
			}

			// If there were no inactive GameObjects in the pool and the pool should
			// grow, then let's add a new object to the pool.
			if(result == null && _shouldGrow)
			{
				result = AddObject();
			}

			// If we found an incative object let's activate it.
			if (result != null)
			{
				Activate(result);
			}

			return result;
		}

		/// <summary>
		/// Returns an object back to the pool.
		/// </summary>
		/// <param name="go">The object which should be returned to the pool.</param>
		/// <returns>Could the object be returned back to the pool.</returns>
		public bool ReturnObject(GameObject go)
		{
			bool result = false;

			foreach(GameObject pooledObject in _pool)
			{
				if(pooledObject == go)
				{
					Deactivate(go);
					result = true;
					break;
				}
			}

			if (!result)
			{
				Debug.LogError("Tried to return an object which doesn't belong to this pool!");
			}

			return result;
		}
	}
}
