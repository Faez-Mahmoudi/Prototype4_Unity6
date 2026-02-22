using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100.0f;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Rotate(horizontalInput);
    }

    public void Rotate(float input)
    {
        transform.Rotate(Vector3.up, input * rotationSpeed * Time.deltaTime); 
    }
}