using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFlagController : MonoBehaviour
{
    
                     public bool check_Enter2D, check_Stay2D, check_Exit2D;
    [SerializeField] GameObject PoxController_Base;
    [SerializeField] PoxController PoxController;

    // Start is called before the first frame update
    void Start()
    {
        PoxController_Base = GameObject.Find("Pox");
        PoxController = PoxController_Base.GetComponent<PoxController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PoxController.jumpFlag == false)
        {
            check_Enter2D = false;
            check_Stay2D = false;
            check_Exit2D = true;
            Debug.Log("JumpFlag"+PoxController.jumpFlag);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("JumpOK");    
        check_Enter2D = true;
        check_Exit2D = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("HitLoged");
        check_Stay2D = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("JumpNow");
        check_Exit2D = false;
    }
}
