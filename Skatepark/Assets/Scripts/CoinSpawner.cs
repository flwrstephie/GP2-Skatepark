using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; 
    public Transform spawnArea;   
    public float spawnHeight = 0.5f; 
    private GameObject currentCoin; 
    private bool isSpawning = false; 

    void Start()
    {
        SpawnCoin(); 
    }

    void SpawnCoin()
    {  
        if (currentCoin != null)
        {
            Destroy(currentCoin); 
        }
    
        Vector3 areaCenter = spawnArea.position;
        Vector3 areaScale = spawnArea.lossyScale; 

        float areaWidth = areaScale.x * 5f; 
        float areaLength = areaScale.z * 5f; 
        
        float randomX = Random.Range(areaCenter.x - areaWidth / 2f, areaCenter.x + areaWidth / 2f);
        float randomZ = Random.Range(areaCenter.z - areaLength / 2f, areaCenter.z + areaLength / 2f);

        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, randomZ);
        
        currentCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
    }

    public void CoinCollected()
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnAfterDelay());
        }
    }

    IEnumerator SpawnAfterDelay()
    {
        isSpawning = true;
        yield return new WaitForSeconds(10f); 
        SpawnCoin();
        isSpawning = false;
    }
}
