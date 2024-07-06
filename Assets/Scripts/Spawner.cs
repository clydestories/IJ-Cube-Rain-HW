using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : Spawnable
{
    [SerializeField] protected T Prefab;

    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolSize;

    private ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<T>
            (
                createFunc: () => OnCreate(),
                actionOnGet: (obj) => ActionOnGet(obj),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolSize
            );
    }

    public void GetInstance()
    {
        _pool.Get();
    }

    public void Release(T instance)
    {
        _pool.Release(instance);
    }

    protected abstract Vector3 GetSpawnPosition();

    protected virtual T OnCreate()
    {
        return Instantiate(Prefab, Vector3.zero, Quaternion.identity);
    }

    private void ActionOnGet(T instance)
    {
        Vector3 spawnPosition = GetSpawnPosition();
        instance.ResetObject(spawnPosition);
        instance.gameObject.SetActive(true);
    }
}
