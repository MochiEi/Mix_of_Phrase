using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class CreateBoxController : MonoBehaviour
{
    [SerializeField]
    GameObject Box;

    [SerializeField]
    GameObject delateBox;
    Collider2D delate;

    [SerializeField]
    bool NotCreate;
    [SerializeField]
    List<GameObject>StageThisCost = new List<GameObject>();

    public int MaxCost = 0;

    List<Collider2D> ScanDelate = new List<Collider2D>();

    // Start is called before the first frame update
    void Start()
    {
        NotCreate = true;
        delate = delateBox.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // DebugCommand///
        if (Input.GetKeyDown(KeyCode.R))
        {
            CreateBox();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            DelateBox();
        }
        // tadanosyori///
        StageThisCost.RemoveAll(Item => Item == null);
    }

    publicÅ@void CreateBox()
    {
        if (StageThisCost.Count < MaxCost && NotCreate)
        {
           GameObject box =  Instantiate(Box,this.gameObject.transform.position, Quaternion.identity);
            
            StageThisCost.Add(box);
        }
    }
    public void DelateBox()
    {
        int Hitbox = delate.OverlapCollider(new ContactFilter2D(), ScanDelate);

        if (Hitbox > 0)
        {
            for (int i = 0; i < Hitbox; i++)
            {
                if (ScanDelate[i].tag == "Box")
                {
                    Destroy(ScanDelate[i].gameObject);
                }

            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Pox")
        {
            NotCreate = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        NotCreate = true;
    }
}