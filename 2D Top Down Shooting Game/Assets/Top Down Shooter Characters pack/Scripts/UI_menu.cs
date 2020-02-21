using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace GearsAndBrains
{

public class UI_menu : MonoBehaviour {

private GameObject Player;
private Soldier_Control SolContScr;

public Image imgLifebar;
public Image imgMainWep;
public Image imgSecWep;

private int textLifeSet;

private float LifeBar;
private float LifeBarSet;

private float mainAmmo;
private float mainAmmoInt;

private float secAmmo;
private float secAmmoInt;

public Text textLife;
public Text textMainAmmo;
public Text textSecAmmo;
public Text scoreText;
public GameObject scoreObject, objectivesObject;
public GameObject restartButton, menuButton, pauseButton, playButton;

public int Score;

	// Use this for initialization
	void Start () 
		{
			SolContScr = GameObject.FindWithTag ("Player").GetComponent<Soldier_Control> ();

			LifeBarSet = SolContScr.HP;
			textLifeSet = SolContScr.HP;

			mainAmmoInt = SolContScr.mainBullets;
			secAmmoInt = SolContScr.secBullets;
		}
	
	// Update is called once per frame
	void Update () 
		{
			float LifeBarCurent = SolContScr.HP;
			int textLifeCurent = SolContScr.HP;
			float mainAmmoCurent = SolContScr.mainBullets;
			float secAmmoCurent = SolContScr.secBullets;

            float mainAmmoStock = SolContScr.mainBulletsStock;
            float secAmmoStok = SolContScr.secBulletsStock;

            LifeBar = LifeBarCurent / LifeBarSet;
			imgLifebar.fillAmount = LifeBar;
			//Debug.Log (LifeBar.ToString ()); 

			int LifeBarText = (textLifeCurent * 100) / textLifeSet;
			//LifeBar /= textLifeSet;

			if (LifeBar <= 0)
			textLife.text ="" + 0;
			else
				textLife.text ="" + LifeBarText;

			mainAmmo = mainAmmoCurent / mainAmmoInt;
			imgMainWep.fillAmount = mainAmmo;

			secAmmo = secAmmoCurent / secAmmoInt;
			imgSecWep.fillAmount = secAmmo;

			scoreText.text ="" + Score;

			textMainAmmo.text ="" + mainAmmoStock;

			textSecAmmo.text ="" + secAmmoStok;

		}
	public void RestartSet ()
		{
			scoreObject.transform.localPosition = new Vector3(0, 160, 0);
            objectivesObject.transform.localPosition = new Vector3(0, 95, 0);
            restartButton.SetActive(true);
            menuButton.SetActive(true);
        }

    public void MenuSet()
        {
            scoreObject.transform.localPosition = new Vector3(0, 160, 0);
            objectivesObject.transform.localPosition = new Vector3(0, 95, 0);
            menuButton.SetActive(true);
        }

    void Menu () 
		{ 
			Application.LoadLevel("DemoMenu");
            Time.timeScale = 1;
        }

    void Restart()
        {
            Application.LoadLevel("Demo_Scen");
            Time.timeScale = 1;
        }
    void Pause ()
        {
            restartButton.SetActive(true);
            menuButton.SetActive(true);
            pauseButton.SetActive(false);
            playButton.SetActive(true);
            Time.timeScale = 0;
        }
    void Play()
        {
            restartButton.SetActive(false);
            menuButton.SetActive(false);
            playButton.SetActive(false);
            pauseButton.SetActive(true);
            Time.timeScale = 1;
        }
    }
}