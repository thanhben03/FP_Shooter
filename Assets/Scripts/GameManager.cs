using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject youWinUI;
    public TextMeshProUGUI enemyLeftText;
    int enemyLeft = 0;


    private void Awake()
    {
        Debug.Log("Game Manager");
        Instance = this;
    }

    

    public void AdjustEnemyLeftUI(GameObject obj, int amount)
    {
        if (amount > 0)
        {

            Debug.Log("Enemy Left: " + enemyLeft + " Name: " + obj.name);
        }
        enemyLeft += amount;
        enemyLeftText.text = "Enemies Left: " + enemyLeft;
        if (enemyLeft <= 0)
        {
            youWinUI.SetActive(true);
            SetCursorState(false);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetCursorState(bool status)
    {
        StarterAssetsInputs starterAssetsInputs = FindAnyObjectByType<StarterAssetsInputs>();
        starterAssetsInputs.SetCursorState(status);
    }
}
