using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpMian : MonoBehaviour
{
    Collider2D c_Jump;//�W�����v�̓����蔻��
    [SerializeField]
    List<GameObject> HitLog;//�������Ă���I�u�W�F�N�g�̏���
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
