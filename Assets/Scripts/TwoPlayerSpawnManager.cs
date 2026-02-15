using UnityEngine;

public class TwoPlayerSpawnManager : MonoBehaviour
{
    public GameObject[] powerupPrefabs;

    //private UIHandler uiHandler;

    private float spawnRange = 9.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //uiHandler = GameObject.Find("Canvas").GetComponent<UIHandler>(); 

        SpawnPowerup();
    }

    public void SpawnPowerup()
    {
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);
    }

    // Random position generator
    private Vector3 GenerateSpawnPosition()
    {
        float SpawnPosX = Random.Range(-spawnRange, spawnRange);
        float SpawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(SpawnPosX, 0, SpawnPosZ);
        return randomPos;
    }
}
