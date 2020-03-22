using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private GameObject player;
    private PlayerController player_Controller;
    public Image healthBarImage;
    public Image rifleImage;
    public Image pistolImage;
    //public Image sniperImage;

    private int textHealthSet;
    private float healthBar;
    private float healthBarSet;

    private float rifleAmmo;
    private float rifleAmmoCount;

    private float pistolAmmo;
    private float pistolAmmoCount;

    //private float sniperAmmo;
    //private float sniperAmmoCount;

    public Text textHealth;
    public Text textRifleAmmo;
    public Text textPistolAmmo;
    //public Text textSniperAmmo;
    public Text textScore;
    public GameObject scoreGameObject, objectiveGameObject;
    public GameObject restartButton, menuButton, pauseButton, playButton;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        player_Controller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        healthBarSet = player_Controller.HP;
        textHealthSet = player_Controller.HP;
        rifleAmmoCount = player_Controller.RifleBullets;
        pistolAmmo = player_Controller.PistolBullets;
        //sniperAmmo = player_Controller.SniperBullets;
    }

    // Update is called once per frame
    void Update()
    {
        float currentHealthBar = player_Controller.HP;
        int currentHealthText = player_Controller.HP;
        float currentRifleAmmo = player_Controller.RifleBullets;
        float currentPistolAmmo = player_Controller.PistolBullets;
        //float currentSniperAmmo = player_Controller.SniperBullets;
        float rifleAmmoStock = player_Controller.RifleBulletsStock;
        float pistolAmmoStock = player_Controller.PistolBulletsStock;
        //float sniperAmmoStock = player_Controller.SniperBulletsStock;
        healthBar = currentHealthBar / healthBarSet;
        healthBarImage.fillAmount = healthBar;
        int healthBarText = (currentHealthText * 100) / textHealthSet;
        if (healthBar <= 0)
        {
            textHealth.text = "" + 0;
        }
        else 
        {
            textHealth.text = "" + healthBarText;
        }
        rifleAmmo = currentRifleAmmo / rifleAmmoCount;
        rifleImage.fillAmount = rifleAmmo;

        pistolAmmo = currentPistolAmmo / pistolAmmoCount;
        pistolImage.fillAmount = pistolAmmo;

        //sniperAmmo = currentSniperAmmo / SniperAmmoCount;
        //sniperImage.fillAmount = sniperAmmo;

        textScore.text = "" + score;
        textRifleAmmo.text = "" + rifleAmmoStock;
        textPistolAmmo.text = "" + pistolAmmoStock;
        //textSniperAmmo.text = "" + sniperAmmoStock;
    }
    public void SetRestart() 
    {
        scoreGameObject.transform.localPosition = new Vector3(0,160,0);
        objectiveGameObject.transform.localPosition = new Vector3(0,95,0);
        restartButton.SetActive(true);
        menuButton.SetActive(true);
    }
    public void SetMenu() 
    {
        scoreGameObject.transform.localPosition = new Vector3(0,160,0);
        objectiveGameObject.transform.localPosition = new Vector3(0,95,0);
        menuButton.SetActive(true);
    }
    void Menu() 
    {
        Application.LoadLevel("main_menu");
        Time.timeScale = 1;
    }
    void Restart() 
    {
        Application.LoadLevel("scene_1");
        Time.timeScale = 1;
    }
    void Pause() 
    {
        restartButton.SetActive(true);
        menuButton.SetActive(true);
        pauseButton.SetActive(false);
        playButton.SetActive(true);
        Time.timeScale = 0;
    }
    void play() 
    {
        restartButton.SetActive(false);
        menuButton.SetActive(false);
        playButton.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }
}
