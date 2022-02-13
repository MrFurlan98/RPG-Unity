using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    public static UIFade instance;

    public Image imageToFade;
    private bool fadeToBlack;
    private bool fadeFromBlack;
    public float fadeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeToBlack)
        {
            imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, 
                                          Mathf.MoveTowards(imageToFade.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (imageToFade.color.a == 1)
                fadeToBlack = false;
        }
        if (fadeFromBlack)
        {
            imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, 
                                          Mathf.MoveTowards(imageToFade.color.a, 0, fadeSpeed * Time.deltaTime));
            if (imageToFade.color.a == 0)
                fadeFromBlack = false;
        }
    }

    public void FadeToBlack()
    {
        fadeToBlack = true;
        fadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        fadeFromBlack = true;
        fadeToBlack = false;
    }
}
