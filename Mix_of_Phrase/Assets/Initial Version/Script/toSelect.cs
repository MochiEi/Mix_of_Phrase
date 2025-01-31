using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toSelect : MonoBehaviour
{
    [SerializeField] private GameObject isFade;
    private Fade Fade;

    private int frameTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        Fade = isFade.GetComponent<Fade>();
    }

    // Update is called once per frame
    void Update()
    {
        if(frameTime > 5)
        {
            Fade.FadeIn = true;
            if(!Fade.fade)
            {
                SceneManager.LoadScene("Select");
            }
        }
        frameTime++;
    }
}
