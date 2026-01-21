using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private Transform target;
    private float speed = 15.0f;
    private bool homing;

    private float rocketStrength = 15.0f;
    private float aliveTimer = 5.0f;

    // Update is called once per frame
    void Update()
    {
        // Moving and rotating the missle towards the target
        if(homing && target != null)
        {
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }

    // Called by player when we spawn in the rockets
    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }

    // Add a force to whatever is hit
    void OnCollisionEnter(Collision col)
    {
        if (target != null)
        {
            if (col.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRb = col.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -col.contacts[0].normal; // which direction to push the target in
                targetRb.AddForce(away * rocketStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
        /**************************************************************
        * This method first checks if we have a target. If we do, we  *
        * compare the tag of the colliding object with the tag of the *
        * target. If they match, we get the rigidbody of the target.  * 
        * We then use the normal of the collision contact to determine*
        * which direction to push the target in. Finally we apply the *
        * force to the target and destroy the missile.                *
        **************************************************************/
    }
}
