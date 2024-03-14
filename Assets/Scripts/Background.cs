using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    Vector3 offset = new Vector3(0f, 0.2f, 0f);
    Vector3 originalPos;
    float delay;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        delay = 0.5f;
        originalPos = transform.position;
        coroutine = Switcher(delay);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //IMAGE NEEEEEDS TO BE AT (0,0,0)!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    private IEnumerator Switcher(float delay)
    {
        while (true)
        {

            if (transform.position == offset)
            {
                transform.position = transform.position - offset;
            }

            else if (transform.position == originalPos)
            {
                transform.position = originalPos + offset;
            }

            yield return new WaitForSeconds(delay);
            
        }
       
    }

}
 