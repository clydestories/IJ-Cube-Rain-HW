using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class Spawnable : MonoBehaviour
{
    [SerializeField] protected CountdownDisplay CountdownDisplay;

    private Color _startingColor;

    private void Start()
    {
        _startingColor = GetComponent<Renderer>().material.color;
    }

    public virtual void ResetObject(Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Renderer>().material.color = _startingColor;
        CountdownDisplay.gameObject.SetActive(false);
    }
}
