using UnityEngine;

public class RotatePickupAnim : MonoBehaviour
{
    public float speed = 240;

    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
