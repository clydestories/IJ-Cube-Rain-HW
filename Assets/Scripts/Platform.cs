using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Cube cube))
        {
            if (cube.IsTouched == false)
            {
                cube.Touch();
            }
        }
    }
}
