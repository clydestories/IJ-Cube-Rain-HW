using UnityEngine;

[RequireComponent (typeof(Renderer), typeof(Rigidbody))]
public class Spawnable : MonoBehaviour
{
    [SerializeField] protected CountdownDisplay CountdownDisplay;

    protected Renderer Renderer;

    private Color _startingColor;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _startingColor = Renderer.material.color;
    }

    public virtual void ResetObject(Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        Renderer.material.color = _startingColor;
    }
}
