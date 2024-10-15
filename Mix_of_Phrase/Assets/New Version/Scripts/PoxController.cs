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
    [SerializeField] float maxVerticalSpeed = 0; // �ő吂�����x��ݒ�i�K�v�ɉ����Ē����j
    [SerializeField] GameObject JumpFlag_Base;
    public JumpFlagController jumpFlag;

    Vector3 pos;

    private bool inputCheck_A = false;
    private bool inputCheck_D = false;
    private bool inputCheck_S = false;
    private bool inputCheck_Space = false;
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
            anim.SetBool("JumpAnim", false);
            inputCheck_JumpFlag = true;
            // Debug.Log("JumpFlag" + inputCheck_JumpFlag);
        }
    }

    public void MoveController()
    {
        pos = this.transform.position;
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            
        }
        ///A��������D��������Ă��Ȃ����///
        if (Input.GetKey(KeyCode.S) && !inputCheck_A && !inputCheck_D)
        {
            inputCheck_S = true;
        }
        ///D��������S��������Ă��Ȃ����///
        else if (Input.GetKey(KeyCode.A) && !inputCheck_D && !inputCheck_S)
        {
            inputCheck_A = true;
        }
        ///A��������S��������Ă��Ȃ����///
        else if (Input.GetKey(KeyCode.D) && !inputCheck_A && !inputCheck_S)
        {
            inputCheck_D = true;
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        ///�������Ă���Δ�Ԃ��Ƃ��o����///
        if (Input.GetKey(KeyCode.Space) && inputCheck_JumpFlag && !inputCheck_Space)
        {
            inputCheck_Space = true;
        }

    }
    void FixedUpdate()
    {
        MoveActions();

        SettingMovement();
    }

    private void MoveActions()
    {

        if (!inputCheck_A && !inputCheck_D && !inputCheck_S && !inputCheck_Space)
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
        if (inputCheck_Space)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            pos = this.transform.position;
            anim.SetBool("JumpAnim", true);
            inputCheck_JumpFlag = false;
        }

        inputCheck_A = false;
        inputCheck_D = false;
        inputCheck_S = false;
        inputCheck_Space = false;

    }
    private void SettingMovement()
    {
        /// �ړ����x������K�p����///
        float currentHorizontalSpeed = Mathf.Abs(rb.velocity.x);  // ���������̑��x���擾
        if (currentHorizontalSpeed > maxSpeed)
        {
            /// �ő呬�x�𒴂��Ă���ꍇ�A���x�𐧌�����///
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        /// ���������̑��x������ǉ�///
        if (Mathf.Abs(rb.velocity.y) > maxVerticalSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * maxVerticalSpeed);
        }
        ///��{�̏������I������Ɉړ��ʂ̓K�p����///
        this.transform.position = pos;
    }
}


