using UnityEngine;

public class RotateSquare : MonoBehaviour
{
    public float rotationSpeed = 30f; // Adjust the speed of rotation as needed.

    void Update()
    {
        // Rotate the square on the Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
