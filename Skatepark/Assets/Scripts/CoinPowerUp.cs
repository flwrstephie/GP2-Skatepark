using UnityEngine;

public class CoinPowerUp : MonoBehaviour
{
    public float boostDuration = 3f; 
    public float jumpBoostMultiplier = 1.5f; 
    public float speedBoostMultiplier = 1.5f; 
    private CoinSpawner coinSpawner;

    void Start()
    {
        coinSpawner = FindObjectOfType<CoinSpawner>(); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {              
                int randomBuff = Random.Range(0, 2); 

                if (randomBuff == 0)
                {
                    StartCoroutine(player.ApplySpeedBoost(boostDuration, speedBoostMultiplier));
                }
                else
                {
                    StartCoroutine(player.ApplyJumpBoost(boostDuration, jumpBoostMultiplier));
                }

                GameManager.Instance.AddCoin(); 
                coinSpawner.CoinCollected(); 
                Destroy(gameObject);
            }
        }
    }
}
