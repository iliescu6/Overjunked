using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroFlicker : MonoBehaviour
{
    public Text text;
    public float flick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flick < 4)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - .2f * Time.deltaTime);
        }
        else 
        {
            text.color= new Color(text.color.r, text.color.g, text.color.b, 1);
            flick = 0;
        }
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene(1);
        }
        flick += Time.deltaTime;
    }
}
