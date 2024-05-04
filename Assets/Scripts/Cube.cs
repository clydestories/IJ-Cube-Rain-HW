using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _displayStep;
    [SerializeField] private float _minLifetime;
    [SerializeField] private float _maxLifetime;

    private CountdownDisplay _countdownDisplay;
    private Color _startingColor;

    public Cube()
    {
        IsTouched = false;
    }

    public bool IsTouched { get; private set;}

    private void Awake()
    {
        _countdownDisplay = GetComponentInChildren<CountdownDisplay>();
    }

    private void Start()
    {
        _startingColor = GetComponent<Renderer>().material.color;
    }

    public void ResetObject(Vector3 position)
    {
        IsTouched = false;
        transform.position = position;
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Renderer>().material.color = _startingColor;
        _countdownDisplay.gameObject.SetActive(false);
    }

    public void Touch(Spawner spawner)
    {
        float lifetime = Random.Range(_minLifetime, _maxLifetime);
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        IsTouched = true;
        _countdownDisplay.gameObject.SetActive(true);
        StartCoroutine(ReturnToPool(spawner, lifetime));
    }

    private IEnumerator ReturnToPool(Spawner spawner, float lifetime)
    {
        var wait = new WaitForSeconds(_displayStep);

        for (float i = lifetime; i > 0; i -= _displayStep)
        {
            _countdownDisplay.UpdateCountdown(i);
            yield return wait;
        }

        spawner.Release(this);
    }
}
    
