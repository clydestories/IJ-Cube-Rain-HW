using System.Collections;
using UnityEngine;

public class Cube : Spawnable
{
    [SerializeField] private float _displayStep;
    [SerializeField] private float _minLifetime;
    [SerializeField] private float _maxLifetime;

    private CubeSpawner _cubeSpawner;
    private BombSpawner _bombSpawner;
    private bool _isTouched = false;
    
    public bool IsTouched => _isTouched;

    public void SetSpawners(CubeSpawner cubeSpawner, BombSpawner bombSpawner)
    {
        _cubeSpawner = cubeSpawner;
        _bombSpawner = bombSpawner;
    }

    public void Touch()
    {
        float lifetime = Random.Range(_minLifetime, _maxLifetime);
        Renderer.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        _isTouched = true;
        CountdownDisplay.gameObject.SetActive(true);
        StartCoroutine(ReturnToPool(lifetime));
    }

    public override void ResetObject(Vector3 position)
    {
        base.ResetObject(position);
        CountdownDisplay.gameObject.SetActive(false);
        _isTouched = false;
    }

    private IEnumerator ReturnToPool(float lifetime)
    {
        var wait = new WaitForSeconds(_displayStep);

        for (float i = lifetime; i > 0; i -= _displayStep)
        {
            CountdownDisplay.UpdateCountdown(i);
            yield return wait;
        }

        _bombSpawner.SpawnAtPosition(transform.position);
        _cubeSpawner.Release(this);
    }
}
    
