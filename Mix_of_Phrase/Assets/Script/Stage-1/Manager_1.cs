using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Manager_1 : MonoBehaviour
{
    //ギミック1
    [SerializeField] private GameObject Button1;
    [SerializeField] private GameObject Block1;
    private ButtonManager ButtonManager1;
    private bool stop1;

    //ギミック2
    [SerializeField] private GameObject Button2;
    [SerializeField] private GameObject[] Block2;
    private ButtonManager ButtonManager2;
    private bool[] stop2;

    //ギミック3
    [SerializeField] private GameObject Button3;
    [SerializeField] private GameObject Block3;
    private ButtonManager ButtonManager3;
    private bool stop3;

    //ギミック4
    [SerializeField] private GameObject[] Block4;
    [SerializeField] private GameObject[] goal;
    [SerializeField] private LayerMask box;
    private ButtonManager ButtonManager4;
    private bool[] stop4;
    private int count = 0;

    [SerializeField] GameObject fade;
    private Fade Fade;

    [SerializeField] private float rayPosX = 0;
    [SerializeField] private float rayPosY = 0;
    private int frameCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //ギミック1
        ButtonManager1 = Button1.GetComponent<ButtonManager>();

        //ギミック2
        ButtonManager2 = Button2.GetComponent<ButtonManager>();
        stop2 = new bool[Block2.Length];

        //ギミック3
        ButtonManager3 = Button3.GetComponent<ButtonManager>();

        //ギミック4
        stop4 = new bool[Block4.Length];

        Fade = fade.GetComponent<Fade>();
    }

    // Update is called once per frame
    void Update()
    {
        if (frameCount > 5)
        {
            Fade.FadeIn = true;
            gimmick1();
            gimmick2();
            gimmick3();
            gimmick4();
        }
        frameCount++;

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Stage-1");
        }
    }

    private void gimmick1()
    {
        ButtonManager Manager = ButtonManager1;
        GameObject block = Block1;
        Vector3 pos = Block1.transform.position;
        bool stop = stop1;
        Transform[] lamp = block.GetComponentsInChildren<Transform>();
        SpriteRenderer material = lamp[1].GetComponent<SpriteRenderer>();

        if (Manager.on)
        {
            stop1 = false;
            pos.y += Time.deltaTime * 2;
            pos.y = Mathf.Min(7, pos.y);
            material.color = Color.green;
            if (pos.y >= 7)
            {
                material.color = Color.red;
            }
        }
        else
        {
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            RaycastHit2D ray1 = Physics2D.Raycast(new Vector2(pos.x, pos.y - 3.62f), Vector2.down, 0f, layerMask);
            RaycastHit2D ray2 = Physics2D.Raycast(new Vector2(pos.x + 0.43f, pos.y - 3.62f), Vector2.down, 0f, layerMask);
            RaycastHit2D ray3 = Physics2D.Raycast(new Vector2(pos.x - 0.43f, pos.y - 3.62f), Vector2.down, 0f, layerMask);

            if (ray1.collider != null || ray2.collider != null || ray3.collider != null)
            {
                stop1 = true;
            }

            if (!stop)
            {
                pos.y -= Time.deltaTime * 2;
                material.color = Color.green;
            }
            else
            {
                material.color = Color.red;
            }
        }
        block.transform.position = pos;
    }

    private void gimmick2()
    {
        for (int i = 0; i < Block2.Length; i++)
        {
            ButtonManager Manager = ButtonManager2;
            Vector3 pos = Block2[i].transform.position;
            Transform[] lamp = Block2[i].GetComponentsInChildren<Transform>();
            SpriteRenderer material = lamp[1].GetComponent<SpriteRenderer>();

            if (Manager.on)
            {
                stop2[i] = false;
                pos.y -= Time.deltaTime * 2;
                pos.y = Mathf.Max(-7.8f, pos.y);
                material.color = Color.green;
                if (pos.y <= -7.8f)
                {
                    material.color = Color.red;
                }
            }
            else
            {
                int layerMask = 1 << 8;
                layerMask = ~layerMask;
                RaycastHit2D ray1 = Physics2D.Raycast(new Vector2(pos.x, pos.y + 5.23f), Vector2.up, 0f, layerMask);
                RaycastHit2D ray2 = Physics2D.Raycast(new Vector2(pos.x + 0.32f, pos.y + 5.23f), Vector2.up, 0f, layerMask);
                RaycastHit2D ray3 = Physics2D.Raycast(new Vector2(pos.x - 0.32f, pos.y + 5.23f), Vector2.up, 0f, layerMask);

                if (ray1.collider != null || ray2.collider != null || ray3.collider != null)
                {
                    stop2[i] = true;
                }
                if (!stop2[i])
                {
                    pos.y += Time.deltaTime * 2;
                    material.color = Color.green;
                }
                else
                {
                    material.color = Color.red;
                }
            }

            Block2[i].transform.position = pos;
        }
    }

    private void gimmick3()
    {
        ButtonManager Manager = ButtonManager3;
        GameObject block = Block3;
        Vector3 pos = Block3.transform.position;
        bool stop = stop3;
        Transform[] lamp = block.GetComponentsInChildren<Transform>();
        SpriteRenderer material = lamp[1].GetComponent<SpriteRenderer>();

        if (Manager.on)
        {
            stop3 = false;
            pos.y += Time.deltaTime * 2;
            pos.y = Mathf.Min(3, pos.y);
            material.color = Color.green;
            if (pos.y >= 3)
            {
                material.color = Color.red;
            }
        }
        else
        {
            pos.y -= Time.deltaTime * 2;
            material.color = Color.green;
            pos.y = Mathf.Max(-0.45f, pos.y);
            if (pos.y <= -0.45f)
            {
                material.color = Color.red;
            }
        }
        block.transform.position = pos;
    }

    private void gimmick4()
    {
        GameObject[] BOX = new GameObject[4];
        SpriteRenderer[] sprit = new SpriteRenderer[4];
        int setBox = 0;

        for (int i = 0; i < 4; i++)
        {
            Vector3 pos = Block4[i].transform.position;
            Transform[] lamp = Block4[i].GetComponentsInChildren<Transform>();
            SpriteRenderer material = lamp[1].GetComponent<SpriteRenderer>();

            RaycastHit2D ray1 = Physics2D.Raycast(new Vector2(pos.x, pos.y + 3.62f), Vector2.up, 0.1f, box);

            if (ray1.collider != null)
            {
                BOX[i] = ray1.collider.gameObject;
                sprit[i] = BOX[i].GetComponent<SpriteRenderer>();
                material.color = Color.green;
                setBox++;
            }
            else
            {
                material.color = Color.red;
            }

        }

        if (setBox >= 4)
        {
            for (int i = 0; i < 4; i++)
            {
                Vector3 pos = Block4[i].transform.position;
                Vector3 Pos = BOX[i].transform.position;

                pos.y -= Time.deltaTime * 1;
                pos.y = Math.Max(-2.85f, pos.y);
                Pos.y -= Time.deltaTime * 1;
                Pos.y = Math.Max(1.17f, Pos.y);

                Block4[i].transform.position = pos;
                BOX[i].transform.position = Pos;
            }
        }

        for (int i = 4; i < 6; i++)
        {
            Vector3 scale = Block4[i].transform.localScale;

            if (Block4[0].transform.position.y <= -2.5)
            {
                scale.x += Time.deltaTime * 2;
                scale.x = Math.Min(7f, scale.x);
            }
            else
            {
                scale.x -= Time.deltaTime * 2;
                scale.x = Math.Max(0.5f, scale.x);
            }

            Block4[i].transform.localScale = scale;
        }

        if(count == 0 && Block4[4].transform.localScale.x >= 2)
        {
            count = 1;
        }

        if (count == 1 && Block4[6].transform.localScale.x >= 10)
        {
            count = 2;
        }

        if (count == 2 && Block4[6].transform.localScale.x <= 0.5)
        {
            Fade.FadeOut = true;
            if (Fade.fade) SceneManager.LoadScene("toSelect");
        }      

        if (count == 1)
        {
            Vector3 scale = Block4[6].transform.localScale;

            scale.x += Time.deltaTime * 3;
            scale.x = Math.Min(10f, scale.x);

            Block4[6].transform.localScale = scale;
        }

        if (count == 2)
        {
            for (int i = 0; i < goal.Length; i++)
            {
                Vector3 scale = Block4[6].transform.localScale;

                scale.x -= Time.deltaTime * 1;
                scale.x = Math.Max(0.5f, scale.x);

                sprit[i].enabled = false;
                goal[i].transform.position = BOX[i].transform.position;

                Block4[6].transform.localScale = scale;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector2(Block4[0].transform.position.x + rayPosX, Block4[0].transform.position.y + rayPosY),new Vector2 (0,0.1f));
    }
}
