using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpMian : MonoBehaviour
{
    Collider2D c_Jump;//ジャンプの当たり判定
    [SerializeField]
    List<GameObject> HitLog;//当たっているオブジェクトの処理
    //player
    GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Pox");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HitLogConsoll()
    {

    }

}
