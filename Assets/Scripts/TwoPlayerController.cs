using UnityEngine;
using System.Collections;

public class TwoPlayerController : MonoBehaviour
{
    private TwoPlayerUIHandler uiHandler;
    private TwoPlayerSpawnManager spawnManager;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    [SerializeField] private GameObject powerupIndicator;
    [SerializeField] private GameObject startPoint;

    [SerializeField] private string inputID;
    private float powerupStrength = 15.0f;
    private float lowerBound = -2.0f;
    public float speed = 5.0f;
    public bool hasPowerup;

    // Rocket powerup
    public PowerUpType currentPoverUp = PowerUpType.None; 
    public GameObject rocketPrefab; 
    private GameObject tmpRocket; 
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
        focalPoint = GameObject.Find("FocalPoint" + inputID);  
        uiHandler = GameObject.Find("Canvas").GetComponent<TwoPlayerUIHandler>(); 
        spawnManager = GameObject.Find("SpawnManager").GetComponent<TwoPlayerSpawnManager>(); 

    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical" + inputID);
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed); 

        if(gameObject.transform.position.y < lowerBound)
        {
            gameObject.transform.position = startPoint.transform.position;
            focalPoint.transform.rotation = startPoint.transform.rotation;
            gameObject.transform.rotation = new Quaternion();
            playerRb.linearVelocity = new Vector3();
            playerRb.angularVelocity = new Vector3();
            uiHandler.AddWin(1, inputID);
            spawnManager.SpawnPowerup();
        }

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
            uiHandler.PrintPowerup(currentPoverUp.ToString(), inputID);

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
        if (collision.gameObject.CompareTag("Player") && currentPoverUp == PowerUpType.Pushback)
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
    }

    IEnumerator Smash()
    {
        var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None); //FindObjectsOfType<Enemy>();

        floorY = transform.position.y;

        float jumpTime = Time.time + hangTime;

        while (Time.time < jumpTime)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, smashSpeed);
            yield return null;
        }

        while ( transform.position.y > floorY)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, -smashSpeed * 2);
            yield return null;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            if ( enemies[i] != null)
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
        }

        smashing = false;
    }

    IEnumerator PowerupCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        currentPoverUp = PowerUpType.None;
        uiHandler.PrintPowerup("None", inputID);
        powerupIndicator.gameObject.SetActive(false);
    }
}
