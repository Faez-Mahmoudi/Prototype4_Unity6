using UnityEngine;

public class Enemy : MonoBehaviour
{
    private UIHandler uiHandler;

    [SerializeField] private float speed = 3.0f;
    private Rigidbody enemyRb;
    private GameObject player;

    // Boss
    [SerializeField] private bool isBoss = false;
    [SerializeField] private float spawnInterval;
    private float nextSpawn;
    private SpawnManager spawnManager;
    public int miniEnemySpawnCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        uiHandler = GameObject.Find("Canvas").GetComponent<UIHandler>(); 

        if (isBoss)
            spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.timeScale);

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
