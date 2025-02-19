using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIforTown : MonoBehaviour
{
    public TMP_Text guns;
    public TMP_Text tips;
    public GameObject canvas;
    public AudioSource music;
    public AudioSource collect;
    public AudioSource car;
    public Slider musicSlider;
    public Slider collectSlider;
    public Slider carSlider;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);

        musicSlider.value = music.volume;
        collectSlider.value = collect.volume;
        carSlider.value = car.volume;
    }

    // Update is called once per frame
    void Update()
    {
        musicSlider.onValueChanged.AddListener(ChangeVolume); // volume music
        collectSlider.onValueChanged.AddListener(ChangeCollect); // collect music
        carSlider.onValueChanged.AddListener(ChangeCar); // car music

        int itemCount = Managers.Inventory.GetItemCount("CarKey");
        guns.text = itemCount.ToString();
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!canvas.activeSelf)
            {
                Time.timeScale = 0; // start game
                canvas.SetActive(true);
            }
            else
            {
                Time.timeScale = 1; // pause game
                canvas.SetActive(false);
            }
            
        }

        if (itemCount == 0)
        {
            tips.text = "Tip: The 1st key is at the end of the road";
        }
        else if (itemCount == 1)
        {
            tips.text = "Tip: The 2nd key is at the end of the parallel road";
        }
        else if (itemCount == 2)
        {
            tips.text = "Tip: Enter the Car with the goal label";
        }
    }

    void ChangeVolume(float volume)
    {
        music.volume = volume;
    }

    void ChangeCollect(float volume)
    {
        collect.volume = volume;
    }

    void ChangeCar(float volume)
    {
        car.volume = volume;
    }
}
