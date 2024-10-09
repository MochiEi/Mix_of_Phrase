using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box_Generator : MonoBehaviour
{
    [SerializeField] private PhraseWindow phraseWindow;
    private Text text;

    public GameObject Boxprefab;   //�����ɐ����������A�C�e���Ƃ�����悤(public�ϐ�1�ɂ�1�܂�)
    public GameObject createEffect;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        text = phraseWindow.text;

        if (text.text == "create  box" && Input.GetKeyDown(KeyCode.E) && Directlyabove()) //  E�L�[���������Ƃ�
        {
            Vector3 spawnPosition = transform.position + Vector3.up; // �^��̈ʒu���v�Z
            Instantiate(Boxprefab, spawnPosition, Quaternion.identity); // �v���n�u�𐶐�
            Instantiate(createEffect, new Vector3(spawnPosition.x, spawnPosition.y, -1), Quaternion.identity); // �v���n�u�𐶐�
        }
    }

    private bool Directlyabove()    //POX�N�̐^����v�Z
    {
        int layerMask = 1 << 7 | 1 << 8;
        layerMask = ~layerMask;

        RaycastHit2D POXLup = Physics2D.Raycast(new Vector2(transform.position.x - 0.39f, transform.position.y + 0.55f), Vector2.up, 0.8f, layerMask);
        RaycastHit2D POXRup = Physics2D.Raycast(new Vector2(transform.position.x + 0.39f, transform.position.y + 0.55f), Vector2.up, 0.8f, layerMask);

        return POXLup.collider == null && POXRup.collider == null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Vector2(transform.position.x - 0.39f, transform.position.y + 0.55f), new Vector2(0, 0.8f));
        Gizmos.DrawRay(new Vector2(transform.position.x + 0.39f, transform.position.y + 0.55f), new Vector2(0, 0.8f));
    }

}
