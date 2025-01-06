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

    //---------------------------------------------------------------------------------------//�W�����v���ł��邩�ǂ����̏����A�O��̏����ɔ�ׂ�bool��6�����čs�����߂����������B
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
    //---------------------------------------------------------------------------------------//���������I�u�W�F�N�g�̓����蔻�肪�w�肵��LIST�ɓ����Ă��邩�ǂ����A�����ȏ����g���B
    public static bool TargetTagResarch(Collider2D collder2d, params string[] tags)
    {
        return tags.Any(tag => collder2d.CompareTag(tag));
    }
    //---------------------------------------------------------------------------------------//
}
