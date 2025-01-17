using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    public TextMeshProUGUI coinText; 
    private int coinCount = 0; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin()
    {
        coinCount++;
        UpdateCoinUI();
    }

    
    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "x " + coinCount;
        }
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game..."); 
        Application.Quit(); 
    }
}
