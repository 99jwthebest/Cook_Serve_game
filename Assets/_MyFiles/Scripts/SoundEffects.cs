using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static SoundEffects instance;

    public AudioSource source;
    public AudioSource customerHereSource;
    public AudioSource gaveCustomerFoodSource;
    public AudioClip hoverbuttonClip;
    public AudioClip clickButtonClip;
    public AudioClip clickButtonClip2;
    public AudioClip clickButtonClip3;
    public AudioClip customerHereSound;
    public AudioClip cookingStationClickSound;
    public AudioClip orderMessedUpSound;
    public AudioClip foodCookingSound;
    public AudioClip gaveCustomerFoodSound;
    public GameObject ambientSource;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHoverOverButtonSound()
    {
        source.PlayOneShot(hoverbuttonClip);
    }

    public void PlayClickButtonSound()
    {
        source.PlayOneShot(clickButtonClip);
    }

    public void PlayClickButtonSound2()
    {
        source.PlayOneShot(clickButtonClip2);
    }
    public void PlayClickButtonSound3()
    {
        source.PlayOneShot(clickButtonClip3);
    }
    public void PlayCustomerHereSound()
    {
        customerHereSource.PlayOneShot(customerHereSound);
    }
    public void PlayCookingStationClickSound()
    {
        source.PlayOneShot(cookingStationClickSound);
    }
    public void PlayOrderMessedUpSound()
    {
        source.PlayOneShot(orderMessedUpSound);
    }
    public void PlayFoodCookingSound()
    {
        source.PlayOneShot(foodCookingSound);
    }
    public void PlayGaveCustomerFoodSound()
    {
        gaveCustomerFoodSource.PlayOneShot(gaveCustomerFoodSound);
    }
    public void PlayAmbientClip()
    {
        ambientSource.SetActive(true);
    }
}
