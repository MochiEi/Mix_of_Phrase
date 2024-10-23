using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    ///���蓮��
    [SerializeField] bool t_Enter = false, t_Exit = false, t_Stay = false; ///�����蔻�萧��
    [SerializeField] bool jump_flag = false, jump_Hit = false;///�W�����v���쐧��

    ///��������
    [SerializeField] bool frame_One = false, frame_Chche = false;///�t���[�����쐧��֘A
    [SerializeField] float moveSpeed = 0, jumpPower = 0, MaxvarticalSpeed = 0, MaxSpeed = 0;///�ړ��֘A�A���x����
    [SerializeField] bool input_A = false, input_D = false, input_S = false, input_Space = false;///���͐���֘A
    [SerializeField] Vector2 pos;
    [SerializeField] string[] StepTags;

    ///�R���|�[�l���g�֘A
    Animator anim;
    Rigidbody2D rb;
    public GameObject Pox;

    // Start is called before the first frame update
    void Start()
    {   Pox = GameObject.Find("Pox");
        anim = Pox.gameObject.GetComponent<Animator>();
        rb = Pox.gameObject.GetComponent<Rigidbody2D>();
        frame_One = true;  
    }

    // Update is called once per frame
    void Update()
    {
        pos = Pox.transform.position;
        input_S = false;
        input_A = false;
        input_D = false;

        if (t_Enter && t_Stay && !t_Exit)
        {
            jump_flag = true;
        }
        else if (t_Exit)
        {
            jump_flag= false;
        }
     
        if (Input.GetKey(KeyCode.S))
        {
            input_S = true;
        }

        if (Input.GetKey(KeyCode.A) && !input_S)
        {
            input_A = true;
        }
        else if (Input.GetKey(KeyCode.D) && !input_S)
        {
            input_D = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jump_Hit && jump_flag)
        {
            input_Space = true;
        }

        MoveActions();
    }

    private void FixedUpdate()
    {
        if (!input_A && !input_D && !input_S)
        { 
            anim.SetBool("DownAnim", false);
            anim.SetBool("MoveAnim", false);
            rb.velocity = new Vector2(0,rb.velocity.y);
        }

        if (input_S)
        {
            rb.velocity = new Vector2(0f , rb.velocity.y);
        }

        if (input_A)
        {
            if (rb.velocity.normalized.x > 0f)
                rb.velocity = new Vector2(0f, rb.velocity.y);

            Pox.transform.localScale = new Vector3(-1, 1);
            rb.AddForce(Vector2.left * moveSpeed, ForceMode2D.Force);
            pos = Pox.transform.position;
        }
        else if (input_D)
        {
            if (rb.velocity.normalized.x < 0f)
                rb.velocity = new Vector2(0f, rb.velocity.y);

            Pox.transform.localScale = new Vector3(1, 1);
            rb.AddForce(Vector2.right * moveSpeed, ForceMode2D.Force);
            pos = Pox.transform.position;   
        }

        if (input_Space && jump_flag)
        {
            jump_Hit = false;
            Invoke(nameof(Delay), 0.3f);
        }

        SettingSpeed();
        input_Space = false;
    }

    private void MoveActions()
    {
        if (jump_flag)
        {
            if (input_S)
            {
                anim.SetBool("DownAnim", true);
            }
            else if (!input_S)
            {
                anim.SetBool("DownAnim", false);
            }

            if (input_A)
            {
                anim.SetBool("MoveAnim", true);
            }
            else if (input_D)
            {
                anim.SetBool("MoveAnim", true);
            }
            else anim.SetBool("MoveAnim", false);

            if (input_Space && jump_flag)
            {
                anim.SetBool("JumpAnim", true);
            }
        }
        else if(t_Exit &&jump_Hit)
        {
            anim.SetBool("JumpAnim", false);
        }
    }

    private void Delay()
    {

        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        pos = this.transform.position;
        Debug.Log(jump_flag);
    }

    private void SettingSpeed()
    {
        /// �ړ����x������K�p����///
        float currentHorizontalSpeed = Mathf.Abs(rb.velocity.x);  // ���������̑��x���擾
        if (currentHorizontalSpeed > MaxSpeed)
        {
            /// �ő呬�x�𒴂��Ă���ꍇ�A���x�𐧌�����///
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * MaxSpeed, rb.velocity.y);
        }

        /// ���������̑��x������ǉ�///
        if (Mathf.Abs(rb.velocity.y) > MaxvarticalSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * MaxvarticalSpeed);
        }
        ///��{�̏������I������Ɉړ��ʂ̓K�p����///
        Pox.transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var Step in StepTags)
        {
            if (collision.tag == Step)
            {
                t_Enter = true;
                jump_Hit = true;
                jump_flag = false;
                Debug.Log(jump_Hit);
            }
        } 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        foreach (var Step in StepTags)
        {
            if (collision.tag == Step )
            {
                t_Stay = true;
               
                    t_Exit = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (var Step in StepTags)
        {
            if (collision.tag ==Step)
            {
                t_Exit = true;
                t_Stay = false;
            }
            else if (!t_Stay)
            {
                t_Enter = false;
            }
        }
    }
}
