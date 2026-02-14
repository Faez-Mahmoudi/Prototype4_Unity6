using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100.0f;
    [SerializeField] private string inputID;


    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal" + inputID);
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime); 
    }
}