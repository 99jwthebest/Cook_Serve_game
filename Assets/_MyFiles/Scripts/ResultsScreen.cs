using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsScreen : MonoBehaviour
{
    public bool GameIsPaused = false;
    public float timeScaleValue;
    //public GameObject pauseMenuUI;

    public TextMeshProUGUI customersServedText;
    public TextMeshProUGUI perfectOrdersText;
    public TextMeshProUGUI totalStrikesText;
    //public TextMeshProUGUI timeBonusText;
    //public TextMeshProUGUI totalScoreNumText;
    //[SerializeField]
    //private int numOfZombiesToKill;
    //private int totalScore;
    public int customersServed;
    public Transform rankIcons;

    public GameObject resultsScreen;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

        //float timeBonus = CountupTimer.Instance.GetCurrentTime();
        //timeBonus *= 1000;

    }

    // Update is called once per frame
    void Update()
    {
        if (FoodMenuInventoryManager.Instance.totalAmountOfCustomersPerRoundRemaining <= 0)
        {
            WinMenuPause();

        }

    }


    void WinMenuPause()
    {

        resultsScreen.SetActive(true);

        customersServed = FoodMenuInventoryManager.Instance.totalAmountOfCustomersPerRound - FoodMenuInventoryManager.Instance.amountOfStrikes;
        customersServedText.text = customersServed.ToString();
        perfectOrdersText.text = FoodMenuInventoryManager.Instance.perfectFoodOrderCombo.ToString();
        totalStrikesText.text = FoodMenuInventoryManager.Instance.amountOfStrikes.ToString();

        if(customersServed >= FoodMenuInventoryManager.Instance.totalAmountOfCustomersPerRound)
        {
            rankIcons.GetChild(0).gameObject.SetActive(true);
        }
        if (customersServed < FoodMenuInventoryManager.Instance.totalAmountOfCustomersPerRound && customersServed >= 30)
        {
            rankIcons.GetChild(1).gameObject.SetActive(true);
        }
        if (customersServed < 30 && customersServed >= 15)
        {
            rankIcons.GetChild(2).gameObject.SetActive(true);
        }
        if (customersServed < 15)
        {
            rankIcons.GetChild(3).gameObject.SetActive(true);
        }

        GameIsPaused = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //public int sumOfScore()
    //{
    //    int sum = ScoreSystem.instance.GetCurrentScore() + timeBonus();
    //    return sum;
    //}

    //int timeBonus()
    //{
    //    float time = CountupTimer.Instance.GetCurrentTime();
    //    int timeBonusF = Mathf.FloorToInt(time * 1000);
    //    return timeBonusF;
    //}

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;

        SceneManager.LoadScene(0);
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;

        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
