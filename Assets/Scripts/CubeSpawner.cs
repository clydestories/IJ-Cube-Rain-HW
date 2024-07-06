using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _delay;
    [SerializeField] private Transform _spawnAreaStartPoint;
    [SerializeField] private Transform _spawnAreaEndPoint;
    [SerializeField] private BombSpawner _bombSpawner;

    private Coroutine _spawn;

    private void Start()
    {
        if (_spawn == null)
        {
            _spawn = StartCoroutine(Spawn());
        }
    }

    protected override Vector3 GetSpawnPosition()
    {
        float positionX = Random.Range(_spawnAreaStartPoint.position.x, _spawnAreaEndPoint.position.x);
        float positionZ = Random.Range(_spawnAreaStartPoint.position.z, _spawnAreaEndPoint.position.z);
        return new Vector3(positionX, _spawnAreaStartPoint.position.y, positionZ);
    }

    protected override Cube OnCreate()
    {
        Cube cube = Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        cube.SetSpawners(this, _bombSpawner);
        return cube;
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            GetInstance();
            yield return new WaitForSeconds(_delay);
        }
    }
}
