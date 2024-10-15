using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PoxController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0;
    [SerializeField] float jumpPower = 0;
    [SerializeField] float maxSpeed = 0;
    [SerializeField] float maxVerticalSpeed = 0; // 最大垂直速度を設定（必要に応じて調整）
    [SerializeField] GameObject JumpFlag_Base;
    public JumpFlagController jumpFlag;

    Vector3 pos;

    private bool inputCheck_A = false;
    private bool inputCheck_D = false;
    private bool inputCheck_S = false;
    private bool inputCheck_Bool = false;
    private bool inputCheck_JumpFlag = false;

    private Animator anim;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        JumpFlag_Base = GameObject.Find("JumpFlagBase");
        jumpFlag = JumpFlag_Base.GetComponent<JumpFlagController>();
    }

    // Update is called once per frame
    void Update()
    {

        MoveController();

        if (jumpFlag.check_Enter2D && jumpFlag.check_Stay2D && jumpFlag.check_Exit2D)
        {
            inputCheck_JumpFlag = true;
           // Debug.Log("JumpFlag" + inputCheck_JumpFlag);
        }
    }

    public void MoveController()
    {
        if (!jumpFlag && !inputCheck_A && !inputCheck_D)
        {
            rb.velocity = Vector2.zero;
        }
        pos = this.transform.position;

        ///DもしくはSが押されていなければ///
        if (Input.GetKey(KeyCode.A) && !inputCheck_D && !inputCheck_S )
        {
            //this.transform.localScale = new Vector3(-1, 1);
            inputCheck_A = true;
            //rb.AddForce(Vector2.left * moveSpeed);
           // anim.SetBool("MoveAnim", true);
           // pos = this.transform.position;
        }
        else if (Input.GetKey(KeyCode.D) && !inputCheck_A && !inputCheck_S)
        {
           // this.transform.localScale = Vector3.one;
            inputCheck_D = true;
           // rb.AddForce(Vector2.right * moveSpeed);
           // anim.SetBool("MoveAnim", true);
            //pos = this.transform.position;
        }
        else
        {
            anim.SetBool("MoveAnim", false);
        }
        ///AもしくはDが押されていなければ///
        if (Input.GetKey(KeyCode.S) && !inputCheck_A && !inputCheck_D)
        {
            inputCheck_S = true;
        }
        ///足がついていれば飛ぶことが出来る///
        if (Input.GetKey(KeyCode.Space) && inputCheck_JumpFlag)
        {
            rb.AddForce(Vector2.up * jumpPower);
            pos = this.transform.position;
            Debug.Log("JumpFlag" + inputCheck_JumpFlag);
            inputCheck_JumpFlag = false;
        }
      

        this.transform.position = pos;

        // 移動速度制限を適用する
        float currentHorizontalSpeed = Mathf.Abs(rb.velocity.x);  // 水平方向の速度を取得
        if (currentHorizontalSpeed > maxSpeed)
        {
            // 最大速度を超えている場合、速度を制限する
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        // 垂直方向の速度制限を追加
        if (Mathf.Abs(rb.velocity.y) > maxVerticalSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * maxVerticalSpeed);
        }
    }
    void FixedUpdate()
    {
        if (inputCheck_A)
        {
            this.transform.localScale = new Vector3(-1, 1);
            rb.AddForce(Vector2.left * moveSpeed);
            anim.SetBool("MoveAnim", true);
            pos = this.transform.position;
        }
        if (inputCheck_D)
        {
            this.transform.localScale = new Vector3(1, 1);
            rb.AddForce(Vector2.right * moveSpeed);
            anim.SetBool("MoveAnim", true);
            pos = this.transform.position;
        }
        if (inputCheck_S)
        {
           
        }

        inputCheck_A = false;
        inputCheck_D = false;
        inputCheck_S = false;

    }
}


