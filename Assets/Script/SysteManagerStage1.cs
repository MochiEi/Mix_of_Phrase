using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SysteManagerStage1 : MonoBehaviour
{
    public GameObject phrase;
    public GameObject phraseGUI;
    public GameObject phraseText;
    public GameObject phraseWindow;

    public GameObject nTab;
    public GameObject vTab;

    public GameObject[] Noun;
    public GameObject[] Verb;

    private Text hitText;
    private Text text;
    private string NounText;
    private string VerbText;

    private GameObject hitObject;
    private Vector3 position;

    private bool TabCheck = false; 
    private float TabMove;
    private float TabDelta;

    private bool nvSelect = false;
    private float nvScale;
    private float nvDelta; 
        
    // Start is called before the first frame update
    void Start()
    {
        text = phrase.GetComponent<Text>();
        VerbText = "";
        NounText = "";
        text.text = VerbText;
        TabMove = -8.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            phraseUpdate();
        }

        TabUpdate();

        if (true) ;
    }

    void phraseUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = Vector2.down;
        float maxDistance = 0.1f;
        int layerMask = 1 << LayerMask.NameToLayer("phrase") | 1 << LayerMask.NameToLayer("GUI"); ;

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, direction, maxDistance, layerMask);

        RaycastHit2D check = default;

        check = hit;

        if (check != default)
        {
            int layer = hit.collider.gameObject.layer;
            string tab = hit.collider.gameObject.tag;
            string name = hit.collider.gameObject.name;

            if (layer == LayerMask.NameToLayer("phrase"))
            {
                phraseControl phraseControl = hit.collider.gameObject.GetComponent<phraseControl>();
                hitText = phraseControl.LinkText.GetComponent<Text>();

                if (tab == "Noun" || tab == "StageNum")
                {
                    NounText = hitText.text;
                }
                if (tab == "Verb")
                {
                    VerbText = hitText.text;
                }

                text.text = VerbText + "  " + NounText;
            }
            if(layer == LayerMask.NameToLayer("GUI"))
            {
                nvDelta = 0;

                if (name == "V Tab")
                {
                    nvSelect = false;
                }
                if (name == "N Tab")
                {
                    nvSelect = true;
                }
            }
        }
    }

    void TabUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TabCheck = !TabCheck;
            TabDelta = 0;
        }

        if (TabCheck)
        {
            TabDelta += Time.deltaTime * 1;
            TabMove += TabDelta;
            TabMove = Mathf.Min(0, TabMove);
        }
        if (!TabCheck)
        {
            TabDelta += Time.deltaTime * 1;
            TabMove -= TabDelta;
            TabMove = Mathf.Max(-8.8f, TabMove);
        }

        phraseGUI.transform.position = new Vector3(TabMove, phraseGUI.transform.position.y, 0);
        phraseText.transform.position = new Vector3(TabMove, phraseText.transform.position.y, 0);

        if (nvSelect)
        {
            foreach (GameObject N in Noun)
            {
                N.SetActive(true);
            }
            foreach (GameObject V in Verb)
            {
                V.SetActive(false);
            }
            nvDelta += Time.deltaTime * 0.5f;
            nvScale += nvDelta;
            nvScale = Mathf.Min(0.4f, nvScale);
        }
        if (!nvSelect)
        {
            foreach (GameObject N in Noun)
            {
                N.SetActive(false);
            }
            foreach (GameObject V in Verb)
            {
                V.SetActive(true);
            }
            nvDelta += Time.deltaTime * 0.5f;
            nvScale -= nvDelta;
            nvScale = Mathf.Max(0f, nvScale);
        }

        nTab.transform.localScale = new(0.6f + nvScale, vTab.transform.localScale.y, 0);
        vTab.transform.localScale = new(1 - nvScale, vTab.transform.localScale.y, 0);
    }
}