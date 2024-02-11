using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Gimmick : MonoBehaviour
{
    //ÉMÉ~ÉbÉNÇP
    [SerializeField] private GameObject Button1;
    [SerializeField] private GameObject Block1;
    private ButtonManager ButtonManager1;
    private bool stop1;

    //ÉMÉ~ÉbÉNÇQ
    [SerializeField] private GameObject Button2;
    [SerializeField] private GameObject[] Block2;
    private ButtonManager ButtonManager2;
    private bool[] stop2;

    //ÉMÉ~ÉbÉN3
    [SerializeField] private GameObject Button3;
    [SerializeField] private GameObject Block3;
    private ButtonManager ButtonManager3;
    private bool stop3;

    [SerializeField] private float rayPosX = 0;
    [SerializeField] private float rayPosY = 0;
    private int frameCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //ÉMÉ~ÉbÉNÇP
        ButtonManager1 = Button1.GetComponent<ButtonManager>();

        //ÉMÉ~ÉbÉN2
        ButtonManager2 = Button2.GetComponent<ButtonManager>();
        stop2 = new bool[Block2.Length];

        //ÉMÉ~ÉbÉN3
        ButtonManager3 = Button3.GetComponent<ButtonManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (frameCount > 5)
        {
            gimmick1();
            gimmick2();
            gimmick3();
        }
        frameCount++;
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector2(Block2[0].transform.position.x + rayPosX, Block2[0].transform.position.y + rayPosY),new Vector2 (0,0.01f));
    }
}
