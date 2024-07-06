using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    private Vector3 _currentSpawnPosition;

    public void SpawnAtPosition(Vector3 position)
    {
        _currentSpawnPosition = position;
        GetInstance();
    }

    protected override Vector3 GetSpawnPosition()
    {
        return _currentSpawnPosition;
    }

    protected override Bomb OnCreate() 
    {
        Bomb bomb = Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        bomb.SetSpawner(this);
        return bomb;
    }
}