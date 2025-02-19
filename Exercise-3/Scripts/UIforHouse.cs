using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIforHouse : MonoBehaviour
{
    public TMP_Text guns;
    public TMP_Text tips;
    public TMP_Text keys;
    public GameObject canvas;
    public AudioSource music;
    public AudioSource fire;
    public AudioSource dead;
    public Slider musicSlider;
    public Slider fireSlider;
    public Slider deadSlider;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);

        musicSlider.value = music.volume;
        fireSlider.value = fire.volume;
        deadSlider.value = dead.volume;
    }

    // Update is called once per frame
    void Update()
    {
        musicSlider.onValueChanged.AddListener(ChangeVolume); // volume music
        fireSlider.onValueChanged.AddListener(ChangeFire); // fire music
        deadSlider.onValueChanged.AddListener(ChangeDead); // dead music

        int itemCount = Managers.Inventory.GetItemCount("Revolver");
        int itemCount1 = Managers.Inventory.GetItemCount("key");
        guns.text = itemCount.ToString();
        keys.text = itemCount1.ToString();
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
            tips.text = "Tip: The gun is behind the house";
        }
        else if (itemCount == 1)
        {
            tips.text = "Tip: Kill the guard with the right click";
        }
    }

    void ChangeVolume(float volume)
    {
        music.volume = volume;
    }

    void ChangeFire(float volume)
    {
        fire.volume = volume;
    }

    void ChangeDead(float volume)
    {
        dead.volume = volume;
    }
}
