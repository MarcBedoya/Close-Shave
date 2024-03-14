using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    IEnumerator crTimer, crCountdown;
    int timeLimit, countdown, transitionDelay, score;
    float timeElapsed, t;

    TMP_Text timerText, scoreText;
    public GameObject player, homeButton, replayButton, timerObj, scoreObj;

    [SerializeField] AudioSource music;
    [SerializeField] AudioClip endSong;

    // Start is called before the first frame update
    void Start()
    {
        countdown = 3;
        timeLimit = 10;

        timerText = timerObj.GetComponent<TMP_Text>();
        scoreText = timerObj.GetComponent<TMP_Text>();
        player.SetActive(false);
        homeButton.SetActive(false);
        replayButton.SetActive(false);
        scoreText.text = "Get Ready";
        //timerText.text = timeLimit.ToString();
        crTimer = Timer(timeLimit);
        crCountdown = Countdown(countdown);

        //change game state to start
        StartCoroutine(crCountdown);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        //t (on a scale of 0 to 1) is the total time elapsed - the countdown timer length (start when the game starts) - half the time limit (start transition when time is halfway done)
        float t = (timeElapsed - countdown - timeLimit/2) / timeLimit;

        if (timerText != null && player != null) 
        { 
            if(t < 1 && player.activeSelf)
            {
                timerText.color = Color.Lerp(Color.white, Color.red, t);
            }
        }
    }

    /*
    void ScreenTransition()
    {
        lerp/slerp? between off screen to on screen;
    }
    */


    IEnumerator Countdown(int countdown)
    {
        while (countdown > 0)
        {
            timerText.text = countdown.ToString() + "...";
            countdown--;
            yield return new WaitForSeconds(1);
        }

        player.SetActive(true);
        StartCoroutine(crTimer);
    }

    IEnumerator Timer(int timeLimit)
    {
        while (timeLimit >= 0)
        {
            
            timerText.text = timeLimit.ToString();
            timeLimit--;

            yield return new WaitForSeconds(1);
        }


        Destroy(player);

        timerText.color = Color.white;
        timerText.fontSize = 15.75f;

        Debug.Log(scoreText.text);

        timerObj.transform.position = new Vector3(timerObj.transform.position.x, timerObj.transform.position.y + .5f, timerObj.transform.position.z); 
        timerText.text = "Lookin' handsome!\r\nTryAgain?";
        music.Stop();
        music.clip = endSong;
        music.Play();


        homeButton.SetActive(true);
        replayButton.SetActive(true);

        //countdown from *transitionDelay*
        //stop.SetActive(false);
        //ScreenTransition();
    }   


}