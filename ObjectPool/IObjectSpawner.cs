using UnityEngine;


public interface IObjectSpawner<T> where T: MonoBehaviour, IPoolable
{
    GameObject Prefab { get; }

    T Spawn();
}
