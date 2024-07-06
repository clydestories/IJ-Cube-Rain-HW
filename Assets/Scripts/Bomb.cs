using System.Collections;
using UnityEngine;

public class Bomb : Spawnable
{
    [SerializeField] float _explosionForce;
    [SerializeField] float _explosionRadius;
    [SerializeField] float _displayStep;
    [SerializeField] float _minDelay;
    [SerializeField] float _maxDelay;
    [SerializeField] ParticleSystem _explosion;

    private BombSpawner _bombSpawner;

    private void OnEnable()
    {
        float delay = Random.Range(_minDelay, _maxDelay);
        StartCoroutine(StartCountdown(delay));
    }

    public void SetSpawner(BombSpawner bombSpawner)
    {
        _bombSpawner = bombSpawner;
    }

    private IEnumerator StartCountdown(float delay)
    {
        var wait = new WaitForSeconds(_displayStep);

        for (float i = delay; i > 0; i -= _displayStep)
        {
            CountdownDisplay.UpdateCountdown(i);
            yield return wait;
        }

        Detonate();
    }

    private void Detonate()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                hit.attachedRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

        Instantiate(_explosion, transform.position, Quaternion.identity);
        _bombSpawner.Release(this);
    }
}
