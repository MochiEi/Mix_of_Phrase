using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateBoxController : MonoBehaviour
{
    [SerializeField]
    GameObject Box;
    [SerializeField]
    GameObject delateBox;
    [SerializeField]
    bool NotCreate = false;
    [SerializeField]
    List<GameObject>StageThisCost = new List<GameObject>();
    public int MaxCost = 0;

    // Start is called before the first frame update
    void Start()
    {
       Collider2D delate = delateBox.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            CreateBox();
        }
        StageThisCost.RemoveAll(Item => Item == null);
    }

    void CreateBox()
    {
        if (StageThisCost.Count < MaxCost)
        {
            Instantiate(Box,this.gameObject.transform.position, Quaternion.identity);
            StageThisCost.Add(Box);
        }
    }
    void DelateBox()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NotCreate = true;
        if (Input.GetKeyDown(KeyCode.G))
        {
            DelateBox();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        NotCreate = false;
    }
}