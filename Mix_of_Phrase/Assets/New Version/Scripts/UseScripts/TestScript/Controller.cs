using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Controller : MonoBehaviour
{
    GameObject player;
    Collider2D hitBox;
    Animator anim;
    Rigidbody2D rb_Pllyer;
    Collider2D[] Box = new Collider2D[5];
    [SerializeField]
    Collider2D[] Ground = new Collider2D[1];
    [SerializeField]
    string[] _SettingTag;
    bool input_A, input_S, input_D, input_Space;///入力の確認
    [SerializeField]
    float moveSpeed = 0, jumpPower = 0, MaxvarticalSpeed = 0, MaxSpeed = 0;///移動関連、速度制御
    Vector2 pos;

    [SerializeField]
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        hitBox = player.GetComponent<Collider2D>();
        rb_Pllyer = player.GetComponent<Rigidbody2D>();
        anim = player.GetComponent<Animator>();
        pos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            input_A = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input_D = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input_S = true;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            input_Space = true;
        }
    }

    private void FixedUpdate()
    {
        if (!input_A && !input_D && !input_S)
        {
            rb_Pllyer.velocity = new Vector2(0, rb_Pllyer.velocity.y);
            pos = player.transform.position;
        }
        if (input_S)
        {
            rb_Pllyer.velocity = new Vector2(0f, rb_Pllyer.velocity.y);
        }
        if (input_A)
        {
            player.transform.localScale = new Vector3(-1, 1);
            rb_Pllyer.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
            pos = player.transform.position;
        }
        if (input_D)
        {
            player.transform.localScale = new Vector3(1, 1);
            rb_Pllyer.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
            pos = player.transform.position;
        }
        if (input_Space)
        {
            Invoke(nameof(Delay), 0.3f);
        }
        ReturnSetting();
    }

    //----------------------------------------------------------//


    /// <summary>
    ///変数1のCollder2Dの当たり判定をString[]で確認できる
    /// </summary>
    /// <param name="Coll"></param>
    /// <param name="ListTag"></param>
    /// <returns></returns>
    bool HitFlagment(Collider2D Coll, params string[] ListTag)
    {
        int HitCount = hitBox.OverlapCollider(new ContactFilter2D(), Ground);
        direction = (Ground[0].ClosestPoint(transform.position)) - ((Vector2)transform.position).normalized;
        if (direction.y < -1.0 && direction.x > -1)
        {
            Debug.Log("Clear");
            if (HitCount > 0)
            {
                foreach (Collider2D Col in Ground)
                {
                    if (TargetTagResarch(Col, ListTag))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        return false;
    }
    


    //----------------------------------------------------------//
    public static bool TargetTagResarch(Collider2D collder2d, params string[] tags)
    {
        return tags.Any(tag => collder2d.CompareTag(tag));
    }
    //----------------------------------------------------------//
    void Delay()
    {
        if (HitFlagment(hitBox, _SettingTag))
        {
            rb_Pllyer.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            pos = player.transform.position;
        }
    }
    //----------------------------------------------------------//

    /// <summary>
    /// boolのリセットと座標更新を行うだけの関数
    /// </summary>
    void ReturnSetting()
    {
        input_A = false;
        input_D = false;
        input_S = false;
        input_Space = false;
        SettingSpeed();
    }
    //----------------------------------------------------------//
    private void SettingSpeed()
    {
        /// 移動速度制限を適用する///
        float currentHorizontalSpeed = Mathf.Abs(rb_Pllyer.velocity.x);  // 水平方向の速度を取得
        if (currentHorizontalSpeed > MaxSpeed)
        {
            /// 最大速度を超えている場合、速度を制限する///
            rb_Pllyer.velocity = new Vector2(Mathf.Sign(rb_Pllyer.velocity.x) * MaxSpeed, rb_Pllyer.velocity.y);
        }

        /// 垂直方向の速度制限を追加///
        if (Mathf.Abs(rb_Pllyer.velocity.y) > MaxvarticalSpeed)
        {
            rb_Pllyer.velocity = new Vector2(rb_Pllyer.velocity.x, Mathf.Sign(rb_Pllyer.velocity.y) * MaxvarticalSpeed);
        }
        ///基本の処理を終えた後に移動量の適用処理///
        player.transform.position = pos;
    }


}
