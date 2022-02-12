using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField]
    private string areaToLoad;
    public string transitionAreaName;
    public AreaEntrance areaEntrance;

    public float timeToLoad = 1f;
    private bool loadAfterFade = false;
    // Start is called before the first frame update
    void Start()
    {
        areaEntrance.transitionName = transitionAreaName;
    }

    // Update is called once per frame
    void Update()
    {
        if (loadAfterFade)
        {
            timeToLoad -= Time.deltaTime;
            if(timeToLoad < 0)
            {
                loadAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //SceneManager.LoadScene(areaToLoad);

            loadAfterFade = true;
            UIFade.instance.FadeToBlack();

            PlayerController.instance.transitionAreaName = transitionAreaName;
        }
    }
}
