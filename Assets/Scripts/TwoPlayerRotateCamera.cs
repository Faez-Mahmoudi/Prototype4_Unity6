using UnityEngine;

public class TwoPlayerRotateCamera : RotateCamera
{
    [SerializeField] private string inputID;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal" + inputID);
        Rotate(horizontalInput);
    }
}
