using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private UIHandler uiHandler;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerupIndicator;

    private float powerupStrength = 15.0f;
    private float lowerBound = -2.0f;
    public float speed = 5.0f;
    public bool hasPowerup;

    // Rocket powerup
    public PowerUpType currentPoverUp = PowerUpType.None; // determine which logic to enable for the player
    public GameObject rocketPrefab; // is used for the homing rocket prefab
    private GameObject tmpRocket; // will be used for spawning in the homing rockets
    private Coroutine powerupCountdown;

    // Smash powerup
    public float hangTime;
    public float smashSpeed;
    public float explosionForce;
    public float explosionRadius;
    bool smashing = false;
    float floorY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");  
        uiHandler = GameObject.Find("Canvas").GetComponent<UIHandler>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed); 

        if(gameObject.transform.position.y < lowerBound)
            uiHandler.GameIsOver();

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);  

        if ( currentPoverUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }

        if ( currentPoverUp == PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space) && !smashing)
        {
            smashing = true;
            StartCoroutine(Smash());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            currentPoverUp = other.gameObject.GetComponent<Powerup>().powerUpType;
            uiHandler.PrintPowerup(currentPoverUp.ToString());

            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);

            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCountDownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && currentPoverUp == PowerUpType.Pushback)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            Debug.Log("Player collided with: " + collision.gameObject.name + " with powerup set to " + currentPoverUp.ToString());
        }
    }

    void LaunchRockets()
    {
        foreach (var enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
        /********************************************************************************
        * Here we are using the same logic as our spawn manager to find all the enemies.*
        * We are then launching our missiles at each one. We launch the missiles from   *
        * above the player, to stop the collision from pushing us back.                 *
        ********************************************************************************/
    }

    IEnumerator Smash()
    {
        var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None); //FindObjectsOfType<Enemy>();

        // Store the y position before taking off
        floorY = transform.position.y;

        // Calculate the amount of time we will go up
        float jumpTime = Time.time + hangTime;

        while (Time.time < jumpTime)
        {
            // Move the player up while still keeping their x velocity
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, smashSpeed);
            yield return null;
        }

        // Now move the player down
        while ( transform.position.y > floorY)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, -smashSpeed * 2);
            yield return null;
        }

        // Cycle through all enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            // Apply an explosion force that originates from our position
            if ( enemies[i] != null)
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
        }

        // We are no longer smashing
        smashing = false;

        /***********************************************************************************
        * This method will be a coroutine so that we can wait while in the method. We will *
        * then launch the player in the air, and then smack them into the ground. When the *
        * hit the ground, they will do a shockwave like force to all nearby enemies.       *
        ***********************************************************************************/
    }

    IEnumerator PowerupCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        currentPoverUp = PowerUpType.None;
        uiHandler.PrintPowerup("None");
        powerupIndicator.gameObject.SetActive(false);
    }
}
