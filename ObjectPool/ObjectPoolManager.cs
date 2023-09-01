using System.Collections.Generic;
using UnityEngine;


public class ObjectPoolManager
{
    private Dictionary<string, object> _pools = new Dictionary<string, object>();


    public void Spawn<T>(IObjectSpawner<T> spawner, int count) where T : MonoBehaviour, IPoolable
    {
        if (!_pools.ContainsKey(typeof(T).Name))
            CreatePool(spawner);

        ((Pool<T>)_pools[typeof(T).Name]).Spawn(count);
    }


    public T Get<T>(IObjectSpawner<T> spawner, Vector3 position, Quaternion rotation) where T : MonoBehaviour, IPoolable
    {
        if (!_pools.ContainsKey(typeof(T).Name))
            CreatePool(spawner);

        return ((Pool<T>)_pools[typeof(T).Name]).Get(position, rotation);
    }


    public void Release<T>(T obj) where T : MonoBehaviour, IPoolable
    {
        ((Pool<T>)_pools[typeof(T).Name]).Release(obj);
    }


    private void CreatePool<T>(IObjectSpawner<T> spawner) where T : MonoBehaviour, IPoolable
    {
        _pools[typeof(T).Name] = new Pool<T>(spawner);
    }
}


public class Pool<T> where T : MonoBehaviour, IPoolable
{
    private List<T> _objects = new List<T>();
    private IObjectSpawner<T> _spawner;


    public Pool(IObjectSpawner<T> spawner)
    {
        _spawner = spawner;
    }


    public void Spawn(int count)
    {
        for (int i = 0; i < count; i++) {
            T obj = _spawner.Spawn();
            Release(obj);
        }
    }


    public T Get()
    {
        T obj;

        if (_objects.Count == 0)
            obj = _spawner.Spawn();
        else {
            obj = _objects[_objects.Count - 1];
            _objects.RemoveAt(_objects.Count - 1);
        }

        obj.Get();
        obj.gameObject.SetActive(true);
        return obj;
    }


    public T Get(Vector3 position)
    {
        T obj = Get();
        obj.transform.position = position;
        return obj;
    }


    public T Get(Vector3 position, Quaternion rotation)
    {
        T obj = Get(position);
        obj.transform.rotation = rotation;
        return obj;
    }


    public void Release(T obj)
    {
        obj.Release();
        obj.transform.SetParent(null);
        obj.gameObject.SetActive(false);
        _objects.Add(obj);
    }
}
