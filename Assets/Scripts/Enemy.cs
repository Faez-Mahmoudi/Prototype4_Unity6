using UnityEngine;

public class Enemy : MonoBehaviour
{
    private UIHandler uiHandler;

    public float speed = 3.0f;
    private Rigidbody enemyRb;
    private GameObject player;

    // Boss
    public bool isBoss = false;
    public float spawnInterval;
    private float nextSpawn;
    public int miniEnemySpawnCount;
    private SpawnManager spawnManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        uiHandler = GameObject.Find("Canvas").GetComponent<UIHandler>(); 


        if (isBoss)
        {
            //spawnManager = FindObjectOfType<SpawnManager>();
            spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        // Call the SpawnMiniEnemy() if isBoss
        if (isBoss)
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;
                spawnManager.SpawnMiniEnemy(miniEnemySpawnCount);
            }
        }

        // Destroy out of bound
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
            if(isBoss)
                uiHandler.AddPoint(15);
            else
                uiHandler.AddPoint(5);
        }
    }
}
