using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CustomerLeavingProgressBar : MonoBehaviour
{
    public float maximum;
    public float current;
    public float start;
    public Image mask;

    // Start is called before the first frame update
    void Start()
    {
        start = maximum;
        current = start;
    }

    // Update is called once per frame
    void Update()
    {
        current -= 10 * Time.deltaTime;
        GetCurrentFill();
    }

    public void GetCurrentFill()
    {
        float fillAmount = current / maximum;
        mask.fillAmount = fillAmount;
    }
}
