using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public float timeLeft;
    public float timePassed;
    public float shutLightsAt;
    public int minutes;
    public string minutesString;
    public int seconds;
    public string secondsString;
    public Text timeText;
    public Text robotText;
    public Text highScoreText;
    public bool gameOver = false;
    public int fixedRobots = 0;
    public int highScore =0;

    /// <summary>
    /// Audio stuff
    /// </summary>
    public AudioSource audioSource;
    public GameObject greenLight;
    public AudioClip greenLightSound;
    public GameObject redLight;
    public AudioClip redLightSound;

    public GameObject lights;

    public float flick;
    public Image overImage;
    public Text[] endGameText;
    // Start is called before the first frame update
    void Start()
    {
        shutLightsAt = 60;
        highScore = PlayerPrefs.GetInt("Highscore",highScore);
        highScoreText.text = "Highscore:" + highScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (flick < 4 && gameOver)
        {
            flick += Time.deltaTime;
            for (int i = 0; i < endGameText.Length; i++)
            {
                endGameText[i].color = new Color(endGameText[i].color.r, endGameText[i].color.g, endGameText[i].color.b, endGameText[i].color.a - .2f * Time.deltaTime);
            }

            endGameText[1].text ="Highscore:"+highScore +"\nYour score:"+fixedRobots;
            overImage.color = new Color(overImage.color.r, overImage.color.g, overImage.color.b, overImage.color.a + .4f*Time.deltaTime);
        }
        else if(gameOver)
        {
            for (int i = 0; i < endGameText.Length; i++)
            {
                endGameText[i].color = new Color(endGameText[i].color.r, endGameText[i].color.g, endGameText[i].color.b, 1);
            }
            flick = 0;
        }
        if (Input.GetButtonDown("Submit") && gameOver)
        {
            SceneManager.LoadScene(0);
        }

        TimeCountdown();   
    }

    void TimeCountdown() 
    {
        if (timeLeft <= 0)
        {
            gameOver = true;
            
            return;
        }
        timePassed += Time.deltaTime;

        if (timePassed > shutLightsAt)
        { StartCoroutine(ShutOffLights()); }

        timeLeft -= Time.deltaTime;        
        minutes = (int)(timeLeft / 60f);
        if (minutes < 10)
        {
            minutesString = "0" + minutes;
        }
        else
        {
            minutesString = minutes.ToString();
        }
        seconds = (int)(timeLeft % 60f);
        if (seconds < 10)
        {
            secondsString = "0" + seconds;
        }
        else
        {
            secondsString = seconds.ToString();
        }
        timeText.text = minutesString + ":" + secondsString;
        robotText.text = "Score:" + fixedRobots;
    }

    IEnumerator ShutOffLights()
    {
        lights.SetActive(false);
        shutLightsAt += timePassed;
        yield return new WaitForSeconds(10f);
        lights.SetActive(true);
    }

     public IEnumerator RobotCheckUp(bool fixedRobot,GameObject robot)
    {
        if (fixedRobot)
        {
            timeLeft += 10;
            audioSource.PlayOneShot(greenLightSound);
            if (greenLight != null)
            {
                greenLight.SetActive(true);
                fixedRobots += 1;
                if (fixedRobots > highScore)
                {
                    highScore = fixedRobots;
                    PlayerPrefs.SetInt("Highscore", highScore);
                    highScoreText.text = "Highscore:" + highScore;
                }
            }
        }
        else
        {
            timeLeft -= 10;
            audioSource.PlayOneShot(redLightSound);
            if (redLight != null)
            {
                redLight.SetActive(true);
            }
        }
        yield return new WaitForSeconds(1f);
            greenLight.SetActive(false); 
            redLight.SetActive(false);
    }
}
