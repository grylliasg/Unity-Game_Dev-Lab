using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI : MonoBehaviour
{
    public GameObject uiPanel;
    public TMP_Text deathCount;
    public TMP_Text victory;
    public TMP_Text changes;
    private int playerLosses;
    public Shooter shooter;
    public Enemy1Attack enemy1;

    void Start()
    {
        uiPanel.SetActive(false);
        playerLosses = PlayerPrefs.GetInt("PlayerLosses", 0); // anaktisi arithmou apotyxiwn
        changes.gameObject.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && uiPanel.activeSelf) // Έλεγχος αν πατήθηκε το πλήκτρο P
        {
            uiPanel.SetActive(false);
        }

        else if (Input.GetKeyDown(KeyCode.P) && !uiPanel.activeSelf)
        {
            uiPanel.SetActive(true);
        }
        deathCount.text = "Death Count: " + playerLosses;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.DeleteAll(); // reset to death count
    }

    public void EasyLevel()
    {
        shooter.enemySpeed = 1f;
        enemy1.enemySpeed = 1f;

    }
    public void MediumLevel()
    {
        shooter.enemySpeed = 3f;
        enemy1.enemySpeed = 3f;

    }
    public void HardLevel()
    {
        shooter.enemySpeed = 6.5f;
        enemy1.enemySpeed = 6.5f;
    }
    public void Continue()
    {
        uiPanel.SetActive(false);
        StartCoroutine(DisplayMessageforChanges());
    }
    public void Victory()
    {
        victory.gameObject.SetActive(true);
    }

    private IEnumerator DisplayMessageforChanges()
    {
        changes.gameObject.SetActive(true); // Εμφάνιση του μηνύματος
        yield return new WaitForSeconds(1.5f); // Περίμενε για λίγα δευτερόλεπτα
        changes.gameObject.SetActive(false); // Απόκρυψη του μηνύματος
    }

}
