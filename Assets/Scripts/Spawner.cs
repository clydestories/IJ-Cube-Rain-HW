using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _spawnAreaStartPoint;
    [SerializeField] private Transform _spawnAreaEndPoint;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolSize;
    [SerializeField] private float _delay;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        Vector3 spawnPosition = GetSpawnPosition();

        _pool = new ObjectPool<Cube>
            (
                createFunc: () => Instantiate(_prefab, spawnPosition, Quaternion.identity),
                actionOnGet: (obj) => ActionOnGet(obj),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolSize
            );
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), _delay, _delay);
    }

    private Vector3 GetSpawnPosition()
    {
        float positionX = Random.Range(_spawnAreaStartPoint.position.x, _spawnAreaEndPoint.position.x);
        float positionZ = Random.Range(_spawnAreaStartPoint.position.z, _spawnAreaEndPoint.position.z);
        return new Vector3 (positionX, _spawnAreaStartPoint.position.y, positionZ);
    }

    private void ActionOnGet(Cube cube)
    {
        Vector3 spawnPosition = GetSpawnPosition();
        cube.ResetObject(spawnPosition);
        cube.gameObject.SetActive(true);
    }

    private void GetCube()
    {
        _pool.Get();
    }

    public void Release(Cube cube)
    {
        _pool.Release(cube);
    }
}
