using System;
using UnityEngine;
using System.Threading;

public class PoxController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0;
    [SerializeField] float jumpPower = 0;
    [SerializeField] float maxSpeed = 0;
    [SerializeField] float maxVerticalSpeed = 0; // 最大垂直速度を設定（必要に応じて調整）
    [SerializeField] int jumpCount = 0;
    [SerializeField] GameObject JumpFlag_Base;
    public JumpFlagController jumpFlagController;

    Vector3 pos;

    private bool inputCheck_A = false;
    private bool inputCheck_D = false;
    private bool inputCheck_S = false;
    private bool inputCheck_Space = false;
    [SerializeField] private bool Check_JumpFlag = false;

    [SerializeField] private bool frameOne;
    private bool frameCache;

    private Animator anim;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        JumpFlag_Base = GameObject.Find("JumpFlagBase");
        jumpFlagController = JumpFlag_Base.GetComponent<JumpFlagController>();
        frameOne = true;
    }

    // Update is called once per frame
    void Update()
    {

        MoveController();

        if (jumpFlagController.check_Enter2D && jumpFlagController.check_Stay2D && jumpFlagController.check_Exit2D)
        {
            Check_JumpFlag = true;
            Debug.Log("JumpFlag" + Check_JumpFlag);
        }else if (!jumpFlagController.check_Exit2D)
        {
            Check_JumpFlag= false;
        }
    }

    public void MoveController()
    {
        pos = this.transform.position;
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {

        }
        ///AもしくはDが押されていなければ///
        if (Input.GetKey(KeyCode.S) && !inputCheck_A && !inputCheck_D)
        {
            inputCheck_S = true;
        }
        ///DもしくはSが押されていなければ///
        else if (Input.GetKey(KeyCode.A) && !inputCheck_D && !inputCheck_S)
        {
            inputCheck_A = true;
        }
        ///AもしくはSが押されていなければ///
        else if (Input.GetKey(KeyCode.D) && !inputCheck_A && !inputCheck_S)
        {
            inputCheck_D = true;
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        ///足がついていれば飛ぶことが出来る///
        if (Input.GetKeyDown(KeyCode.Space) && Check_JumpFlag && !inputCheck_Space)
        {
            inputCheck_Space = true;
             anim.SetBool("JumpAnim", true);
            Invoke(nameof(DelayJump), 0.3f);
        }
    }
    void FixedUpdate()
    {
        MoveActions();

        SettingMovement();
    }

    private void MoveActions()
    {

        if (!inputCheck_A && !inputCheck_D && !inputCheck_S && Check_JumpFlag)
        {
            anim.SetBool("MoveAnim", false);
            anim.SetBool("DownAnim", false);
        }
        if (inputCheck_S)
        {
            anim.SetBool("MoveAnim", false);
            anim.SetBool("DownAnim", true);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (inputCheck_A)
        {
            this.transform.localScale = new Vector3(-1, 1);
            rb.AddForce(Vector2.left * moveSpeed, ForceMode2D.Force);
            anim.SetBool("MoveAnim", true);
            anim.SetBool("DownAnim", false);
            pos = this.transform.position;
        }
        if (inputCheck_D)
        {
            this.transform.localScale = new Vector3(1, 1);
            rb.AddForce(Vector2.right * moveSpeed, ForceMode2D.Force);
            anim.SetBool("MoveAnim", true);
            anim.SetBool("DownAnim", false);
            pos = this.transform.position;
        }

        inputCheck_A = false;
        inputCheck_D = false;
        inputCheck_S = false;
        inputCheck_Space = false;
        if (jumpCount == 1 && Check_JumpFlag)
        {
            anim.SetBool("JumpAnim", false);
            if (frameCache == frameOne)
                return;
            CoolTime();
            frameCache = !frameOne;
        }     
    }

    private void DelayJump()
    {
        if (!inputCheck_Space && Check_JumpFlag && jumpCount == 0)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            pos = this.transform.position;
            Check_JumpFlag = false;
            Debug.Log(Check_JumpFlag);
            jumpCount = 1;
        }
    }
    private void CoolTime()
    {
        jumpCount = 0;
        Debug.Log(jumpCount);
    }
    private void SettingMovement()
    {
        /// 移動速度制限を適用する///
        float currentHorizontalSpeed = Mathf.Abs(rb.velocity.x);  // 水平方向の速度を取得
        if (currentHorizontalSpeed > maxSpeed)
        {
            /// 最大速度を超えている場合、速度を制限する///
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        /// 垂直方向の速度制限を追加///
        if (Mathf.Abs(rb.velocity.y) > maxVerticalSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * maxVerticalSpeed);
        }
        ///基本の処理を終えた後に移動量の適用処理///
        this.transform.position = pos;
    }
}



