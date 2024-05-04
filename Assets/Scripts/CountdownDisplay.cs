using TMPro;
using UnityEngine;

public class CountdownDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;
    [SerializeField] private AnimationCurve _colorBehaviour;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.forward = _camera.transform.forward;
    }

    public void UpdateCountdown(float time)
    {
        _text.text = time.ToString("F2");
        _text.color = Color.Lerp(_startColor, _endColor, _colorBehaviour.Evaluate(time));
    }

}
