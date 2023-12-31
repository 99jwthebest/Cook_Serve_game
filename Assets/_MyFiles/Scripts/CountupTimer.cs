using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountupTimer : MonoBehaviour
{
    public static CountupTimer Instance;

    float currentTime = 0;

    [SerializeField]
    private float startingTime = 0f;
    [SerializeField]
    private float addTimeFromZombie = 10f;

    [SerializeField]
    private TextMeshProUGUI minutesText;
    [SerializeField]
    private TextMeshProUGUI secondsText;
    [SerializeField]
    private TextMeshProUGUI millisecondsText;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {

        if (currentTime <= 0)
        {
            currentTime = 0;

            // Calculate minutes, seconds, and milliseconds
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            int milliseconds = Mathf.FloorToInt((currentTime * 100) % 100);

            minutesText.text = minutes.ToString("00");
            secondsText.text = seconds.ToString(":00");
            millisecondsText.text = "." + milliseconds.ToString("00");

            FoodMenuInventoryManager.Instance.populateTakeoutWindows();
            currentTime = 1;
        }
        else
        {
            currentTime -= 1 * Time.deltaTime;

            // Calculate minutes, seconds, and milliseconds
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            int milliseconds = Mathf.FloorToInt((currentTime * 100) % 100);

            minutesText.text = minutes.ToString("00");
            secondsText.text = seconds.ToString(":00");
            millisecondsText.text = "." + milliseconds.ToString("00");
        }
    }

    public void AddTimeFromZombie()
    {
        currentTime += addTimeFromZombie;
    }

    public void AddTimeFromPickup(int time)
    {
        currentTime += time;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }
}
