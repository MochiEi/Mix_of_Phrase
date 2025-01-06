using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject Player;
    Collider2D HitBox;
    [SerializeField]
    Collider2D[] Box = new Collider2D[5];

    Collider2D[] Ground = new Collider2D[1];

    [SerializeField]
    bool jumpFlag;

    [SerializeField]
    string[] Hittag;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Pox");
        HitBox = Player.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        JumpFlagment(HitBox);
        if (Input.GetKey(KeyCode.S))
        {
        }

        if (Input.GetKey(KeyCode.A))
        {
        }
        else if (Input.GetKey(KeyCode.D))
        {
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
        }
    }

    //---------------------------------------------------------------------------------------//ジャンプができるかどうかの処理、前回の処理に比べてboolが6個消えて行数もめっさ減った。
    void JumpFlagment(Collider2D HitBox)
    {

        int Count = HitBox.OverlapCollider(new ContactFilter2D(), Ground);
        if (Count > 0)
        {
            foreach (Collider2D Col in Ground)
            {
                if (TargetTagResarch(Col, Hittag))
                {
                    jumpFlag = true;
                }
                else
                {
                    jumpFlag = false;
                }
            }
        }
        else jumpFlag = false;
    }
    //---------------------------------------------------------------------------------------//当たったオブジェクトの当たり判定が指定したLISTに入っているかどうか、いろんな処理使う。
    public static bool TargetTagResarch(Collider2D collder2d, params string[] tags)
    {
        return tags.Any(tag => collder2d.CompareTag(tag));
    }
    //---------------------------------------------------------------------------------------//
}
