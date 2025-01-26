using UnityEngine;

public class Rotate : MonoBehaviour
{

    public Vector3 axis;
    public float angle;
    public Transform origin;

    void Update()
    {
        transform.RotateAround(origin.position, axis, angle * Time.deltaTime);
    }
}
