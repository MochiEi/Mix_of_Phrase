using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box_Generator : MonoBehaviour
{
    [SerializeField] private PhraseWindow phraseWindow;
    private Text text;

    public GameObject Boxprefab;   //ここに生成したいアイテムとか入れよう(public変数1つにつき1つまで)
    public GameObject createEffect;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        text = phraseWindow.text;

        if (text.text == "create  box" && Input.GetKeyDown(KeyCode.E) && Directlyabove()) //  Eキーを押したとき
        {
            Vector3 spawnPosition = transform.position + Vector3.up; // 真上の位置を計算
            Instantiate(Boxprefab, spawnPosition, Quaternion.identity); // プレハブを生成
            Instantiate(createEffect, new Vector3(spawnPosition.x, spawnPosition.y, -1), Quaternion.identity); // プレハブを生成
        }
    }

    private bool Directlyabove()    //POX君の真上を計算
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
