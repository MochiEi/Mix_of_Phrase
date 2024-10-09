using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;

public class Select_Controller : MonoBehaviour
{
    [SerializeField] private GameObject POX;
    [SerializeField] private GameObject[] BOX;
    [SerializeField] private CanvasGroup keyCanvas;
    [SerializeField] private float keyFadeTime;
    [SerializeField] private CanvasGroup SelectCanvas;
    [SerializeField] private float SelectFadeTime;

    private int num = 0;
    private bool play = false;

    [SerializeField] private float speed = 0;
    private int animeCount = 0;
    private int frameCount = 0;

    private Animator anime;
    private Rigidbody2D rb;

    [SerializeField] private GameObject fadeScreen;
    private Fade fade;
    
    void Start()
    {
        anime = POX.GetComponent<Animator>();
        rb = POX.GetComponent<Rigidbody2D>();
        keyCanvas.alpha = 0;
        SelectCanvas.alpha = 0;
        fade = fadeScreen.GetComponent<Fade>();
    }

    // Update is called once per frame
    void Update()
    {
        if (frameCount > 5)
        {
            process();
            Animation();
            Fade();
        }
        frameCount++;
    }

    private void process()
    {
        Vector3 pos = POX.transform.position;

        switch (animeCount)
        {
            case 0:
                rb.velocity = new Vector2(speed, rb.velocity.y);

                if(pos.x > -3)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    animeCount = 1;
                }
                break;
            case 1:
                if(Input.GetKey(KeyCode.Space))
                {
                    play = true;
                }

                if (!play) break;

                if (num == 0)
                {
                    foreach (GameObject n in BOX)
                    {
                        PolygonCollider2D p = n.GetComponent<PolygonCollider2D>();
                        p.enabled = false;
                    }
                    animeCount = 3;
                }
                else
                {
                    PolygonCollider2D p = BOX[0].GetComponent<PolygonCollider2D>();
                    p.enabled = false;
                    p = BOX[num].GetComponent<PolygonCollider2D>();
                    p.enabled = false;
                    if (BOX[0].transform.position.y < -5) animeCount = 2;
                }

                break;
            case 2:
                rb.velocity = new Vector2(speed, rb.velocity.y);

                break;
            case 3:
                POX.transform.localScale = new Vector3(-1, 1, 1);

                if (POX.transform.position.x < -9)
                {
                    SceneManager.LoadScene("Title");
                }
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                break;
        }

        POX.transform.position = pos;
    }

    private void Animation()
    {
        switch (animeCount)
        {
            case 0:
                anime.SetBool("walk", true);
                break;
            case 1:
                anime.SetBool("walk", false);
                break;
            case 2:
                anime.SetBool("walk", true);
                break;
            case 3:
                POX.transform.localScale = new Vector3(-1, 1, 1);
                anime.SetBool("walk", true);
                break;
        }
    }

    private void Fade()
    {
        if (animeCount <= 0)
        {
            KeyfadeIn();
        }
        if (animeCount == 3)
        {
            KeyfadeOut();
        }
        if(animeCount == 1)
        {
            SelectfadeIn();
        }
        if(BOX[0].transform.position.y < -2.9f)
        {
            SelectfadeOut();
        }
        if(animeCount == 2)
        {
            fade.FadeOut = true;

            if (!fade.fade) return;
                
            if(num == 1)
            {
                SceneManager.LoadScene("Stage-1");
            }
            if(num == 2)
            {
                SceneManager.LoadScene("Stage-2");
            }
            if (num == 3)
            {
                SceneManager.LoadScene("Stage-3");
            }
            if (num == 4)
            {
                SceneManager.LoadScene("Stage-4");
            }
            if (num == 5)
            {
                SceneManager.LoadScene("Stage-5");
            }
        }
    }
    private void KeyfadeIn()
    {
        keyCanvas.alpha += Time.deltaTime * keyFadeTime;
        keyCanvas.alpha = Math.Min(1, keyCanvas.alpha);
    }
    private void KeyfadeOut()
    {
        keyCanvas.alpha -= Time.deltaTime * keyFadeTime;
        keyCanvas.alpha = Math.Max(0, keyCanvas.alpha);
    }
    private void SelectfadeIn()
    {
        SelectCanvas.alpha += Time.deltaTime * SelectFadeTime;
        SelectCanvas.alpha = Math.Min(1, SelectCanvas.alpha);
    }
    private void SelectfadeOut()
    {
        SelectCanvas.alpha -= Time.deltaTime * SelectFadeTime;
        SelectCanvas.alpha = Math.Max(0, SelectCanvas.alpha);
    }

    public void button1()
    {
        if (animeCount != 1) return;
        num = 1;
        play = true;
    }
    public void button2()
    {
        //if (animeCount != 1) return;
        //num = 2;
        //play = true;
    }
    public void button3()
    {
        //if (animeCount != 1) return;
        //num = 3;
        //play = true;
    }
    public void button4()
    {
        //if (animeCount != 1) return;
        //num = 4;
        //play = true;
    }
    public void button5()
    {
        //if (animeCount != 1) return;
        //num = 5;
        //play = true;
    }
}
