using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIforWoodland : MonoBehaviour
{
    public GameObject canvas;
    public TMP_Text guns;
    public Button shieldButton;
    public GameObject player;
    public Button healthButton;
    private float healthCooldown = 5.0f; // Καθυστέρηση σε δευτερόλεπτα
    private float shieldCooldown = 6.0f; // Καθυστέρηση σε δευτερόλεπτα
    public Slider Vol;
    public Slider fire;
    public Slider collect;
    public AudioSource audio;
    public AudioSource fireaudio;
    public AudioSource collectaudio;
    private Color originalColor;
    private bool isCooldown = false;
    private bool isCooldownShield = false;
    public bool defence = false;
    public PointClickMovement pcm;

    // Start is called before the first frame update
    void Start()
    {
        Vol.value = audio.volume;
        fire.value = fireaudio.volume;
        collect.value = collectaudio.volume;

        canvas.SetActive(false);

        // Αποθήκευση του αρχικού χρώματος του κουμπιού
        if (healthButton != null)
        {
            originalColor = healthButton.image.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vol.onValueChanged.AddListener(ChangeVolume); // volume music
        fire.onValueChanged.AddListener(ChangeFire); // fire music
        collect.onValueChanged.AddListener(ChangeCollect); // collect music
        int itemCount = Managers.Inventory.GetItemCount("health");
        int gun = Managers.Inventory.GetItemCount("Gun");
        int numshield = Managers.Inventory.GetItemCount("shield");
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!canvas.activeSelf)
            {
                canvas.SetActive(true);
                pcm.enabled = false;

                if (itemCount == 0 || isCooldown) // HEALTH BUTTON 
                {
                    healthButton.interactable = false;
                    healthButton.image.color = Color.gray; // Κάνει το κουμπί να φαίνεται μη διαθέσιμο
                }
                else
                {
                    healthButton.interactable = true;
                    healthButton.image.color = Color.green; // Επαναφορά στο αρχικό χρώμα
                }

                if (numshield == 0 || isCooldownShield) // SHIELD BUTTON 
                {
                    shieldButton.interactable = false;
                    shieldButton.image.color = Color.gray;; // Κάνει το κουμπί να φαίνεται μη διαθέσιμο
                }
                else
                {
                    shieldButton.interactable = true;
                    shieldButton.image.color = Color.green; // Επαναφορά στο αρχικό χρώμα
                }

                if (gun == 0)
                {
                    guns.text = "0";
                }
                else{
                    guns.text = gun.ToString();
                }

            }
            else
            {
                canvas.SetActive(false);
                pcm.enabled = true ;
            }
        }
    }

    public void healing()
    {
        if (isCooldown) return; // Αν είναι σε καθυστέρηση, δεν κάνει τίποτα

        Managers.Player.ChangeHealth(10); // +10 health
        canvas.SetActive(false);

        StartCoroutine(HealthCooldown()); // Ξεκινά την καθυστέρηση
    }

    private IEnumerator HealthCooldown()
    {
        isCooldown = true; // Ορισμός της καθυστέρησης

        // Απενεργοποίηση του κουμπιού και αλλαγή χρώματος
        healthButton.interactable = false;
        healthButton.image.color = Color.gray;

        yield return new WaitForSeconds(healthCooldown); // Περιμένει για "healthCooldown" δευτερόλεπτα

        // Ενεργοποίηση του κουμπιού και επαναφορά του αρχικού χρώματος
        healthButton.interactable = true;
        healthButton.image.color = Color.green;

        isCooldown = false; // Τέλος της καθυστέρησης
    }

    public void shielding()
    {
        if (isCooldownShield) return; // Αν είναι σε καθυστέρηση, δεν κάνει τίποτα

        defence = true;
        canvas.SetActive(false);

        StartCoroutine(ShieldCooldown()); // Ξεκινά την καθυστέρηση
    }

    private IEnumerator ShieldCooldown()
    {
        isCooldownShield = true; // Ορισμός της καθυστέρησης

        // Απενεργοποίηση του κουμπιού και αλλαγή χρώματος
        healthButton.interactable = false;
        shieldButton.image.color = Color.gray;

        yield return new WaitForSeconds(shieldCooldown); // Περιμένει για "shieldCooldown" δευτερόλεπτα

        // Ενεργοποίηση του κουμπιού και επαναφορά του αρχικού χρώματος
        shieldButton.interactable = true;
        shieldButton.image.color = Color.green;

        isCooldownShield = false; // Τέλος της καθυστέρησης
        defence = false;
    }

    void ChangeVolume(float volume)
    {
        audio.volume = volume;
    }

    void ChangeFire(float volume)
    {
        fireaudio.volume = volume;
    }

    void ChangeCollect(float volume)
    {
        collectaudio.volume = volume;
    }

}
