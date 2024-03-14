using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Vector2 mousePosition;
    Vector3 hairDiscplace, hairAnchor;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    bool handEmpty, inDrop, inGrab;
    int points, winThreshold;
    static int hairNum;
    static List<Object> hairList;

    public Image faceSprite;
    public GameObject audioHandler;
    public AudioClip ripSound;
    public TMP_Text scoretext, timerText;
    public Sprite open, close, normal, hurt, faceWin, faceLose;
    public LineRenderer hair;

    void Start()
    {
        points = 0;
        winThreshold = 50;

        handEmpty = true;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        audioSource = audioHandler.GetComponent<AudioSource>();
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x, mousePosition.y);
        scoretext.text = "Hairs ripped:\r\n" + points.ToString();
        
        ClickCheck();
        HairTracker();
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Grab") 
        {
            inGrab = true;
        }

        if (collision.gameObject.tag == "Drop")
        {
            inDrop = true;
        }

        ScoreHandler();
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Grab")
        {
            inGrab = false;
        }

        if (collision.gameObject.tag == "Drop")
        {
            inDrop = false;
        }
    }

    void ClickCheck()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (handEmpty && inGrab)
            {
                handEmpty = false;
                faceSprite.sprite = hurt;
                HairSpawner();
            }

            spriteRenderer.sprite = close;
        }

        if (Input.GetMouseButtonUp(0))
        {
            handEmpty = true;
            spriteRenderer.sprite = open;
            faceSprite.sprite = normal;
            HairDestroyer();

        }
    }

    void HairSpawner()
    {
        hairNum = Random.Range(1, 4);
        for (int i = 0; i < hairNum; i++)
        {
            LineRenderer temp = Instantiate(hair);
            hairDiscplace = new Vector3(DisplaceCalc(), DisplaceCalc(), 0f);
            temp.SetPosition(1, hairAnchor + hairDiscplace);
            hairList.Add(temp);
        }
      

        /*
        line point 1 = +/-(.5,.5) of area clicked
        line point 2 = cursor/anchor point
        */


    }
    
    void HairTracker()
    {
        hairAnchor = transform.position + new Vector3(-.7f, .5f, 0f);

        if (hairList == null)
        {
            hairList = new List<Object>(LineRenderer.FindObjectsOfType(typeof(LineRenderer))); 
        }
         
        foreach (LineRenderer hairs in hairList)
        {
            hairs.SetPosition(0, hairAnchor);

        }



    }

    void HairDestroyer()
    {
        for (int i = 0; i < hairList.Count; i++)
        {
            Destroy(hairList[i]);
        }

        hairList.Clear();
    }

    float DisplaceCalc()
    {
        return Random.Range(-.3f, .3f);
    }

    void ScoreHandler()
    {
        if (!handEmpty && inDrop)
        {

            audioSource.PlayOneShot(ripSound, .5f);
            
            points += hairNum;
            HairDestroyer();
            hairNum = 0;
        }
    }

    void OnDestroy()
    {
        HairDestroyer();

        if (points >= winThreshold)
        {
            timerText.text = "Lookin' handsome!\r\nTry Again?";
            faceSprite.sprite = faceWin;
        }

        else if (points < winThreshold)
        {
            timerText.text = "Looks...fine\r\nTry Again?";
            faceSprite.sprite = faceLose;
        }
    }
}
