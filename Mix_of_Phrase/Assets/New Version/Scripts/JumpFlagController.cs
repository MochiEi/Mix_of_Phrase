using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFlagController : MonoBehaviour
{
    
    public bool check_Enter2D, check_Stay2D, check_Exit2D;
    public float frameCount; 
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
        if (PoxController.jumpFlagController == false)
        {
            check_Enter2D = false;
            check_Stay2D = false;
            check_Exit2D = true;
           // PoxController.jump
           // Debug.Log("JumpFlag"+PoxController.jumpFlag);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log("JumpOK");    
        check_Enter2D = true;
        check_Exit2D = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
         frameCount += Time.deltaTime;
        //Debug.Log("HitLoged");
        if (frameCount >= 0.2f)
        {
            check_Stay2D = true;
            frameCount = 0;
        }
        else
        {
            check_Stay2D = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
       // Debug.Log("JumpNow");
        check_Exit2D = false;
        check_Stay2D = false;

    }
}
