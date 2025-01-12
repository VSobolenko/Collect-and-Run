using UnityEngine;

namespace Runner.CameraFeature
{
public class CameraOrbit : MonoBehaviour
{
    public Transform target;
    public float distance = 10.0f;
    public float rotationSpeed = 50.0f;

    private float currentAngle = 0f;

    void Update()
    {
        if (target == null) return;

        currentAngle += rotationSpeed * Time.deltaTime;
        float radians = currentAngle * Mathf.Deg2Rad;
        float x = target.position.x + distance * Mathf.Cos(radians);
        float z = target.position.z + distance * Mathf.Sin(radians);

        transform.position = new Vector3(x, transform.position.y, z);
        transform.LookAt(target);
    }
}
}