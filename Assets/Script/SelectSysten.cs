using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Burst.CompilerServices;

public class SelectSysten : MonoBehaviour
{
    private mousDrag Drag;

    public GameObject Putin;

    public GameObject PushText;
    private Text pushText;

    public GameObject POX;
    private Rigidbody2D POXrb;

    public GameObject[] Num;
    public GameObject[] stageNum;
    private List<Text> NumText;
    private List<Box_Controller> Box_Controller;
    private List<phraseControl> phraseControl;
    private List<Rigidbody2D> rb;

    private bool point;
    private float time = 0;

    RaycastHit2D hit = default;

    // Start is called before the first frame update
    void Start()
    {
        Drag = this.GetComponent<mousDrag>();

        pushText = PushText.GetComponent<Text>();

        POXrb = POX.GetComponent<Rigidbody2D>();

        NumText = new List<Text>();
        foreach (GameObject num in Num)
        {
            NumText.Add(num.GetComponent<Text>());
        }
        foreach(Text text in NumText)
        {
            
        }

        Box_Controller = new List<Box_Controller>();
        phraseControl = new List<phraseControl>();
        rb = new List<Rigidbody2D>();
        foreach (GameObject Num in stageNum)
        {
            Box_Controller.Add(Num.GetComponent<Box_Controller>());
            phraseControl.Add(Num.GetComponent<phraseControl>());
            rb.Add(Num.GetComponent<Rigidbody2D>());
        }
        point = false;
    }

    // Update is called once per frame
    void Update()
    {
        POX_selectAnimation pox = POX.GetComponent<POX_selectAnimation>();

        point = pox.point;

        if (!point)
        {
            time = time + Time.deltaTime;

            if (time > 0.5f)
            {
                POXrb.isKinematic = true;
                Drag.enabled = true;

                foreach (Box_Controller Controller in Box_Controller)
                {
                    Controller.enabled = false;
                }
                foreach (phraseControl control in phraseControl)
                {
                    control.enabled = true;
                }
                foreach (Rigidbody2D rb in rb)
                {
                    rb.isKinematic = true;
                }

                Vector2 direction = Vector2.down;
                float maxDistance = 0.1f;
                int layerMask = 1 << LayerMask.NameToLayer("phrase");

                if (Input.GetMouseButtonUp(0))
                {
                    hit = Physics2D.Raycast(Putin.transform.position, direction, maxDistance, layerMask);
                }

                if (hit != default)
                {
                    int layer = hit.collider.gameObject.layer;

                    if (layer == LayerMask.NameToLayer("phrase"))
                    {
                        pushText.text = "Push Spase to Play Game";
                    }
                }
                else
                {
                    pushText.text = "Push Spase to Title";

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SceneManager.LoadScene("Title");
                    }
                }
            }
        }
    }
}