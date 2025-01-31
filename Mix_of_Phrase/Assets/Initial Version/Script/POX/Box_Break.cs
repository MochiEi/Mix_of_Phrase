using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box_Break : MonoBehaviour
{
    [SerializeField] private PhraseWindow phraseWindow;
    private Text text;

    [SerializeField] LayerMask box;
    [SerializeField] GameObject breakEffect;

    public float rayPosX;
    public float rayPosY;

    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        text = phraseWindow.text;

        pos = this.transform.position;

        RaycastHit2D[] ray = new RaycastHit2D[12];

        ray[0] = Physics2D.Raycast(new Vector2(pos.x, pos.y + 0.53f), Vector2.up, 0.1f, box);
        ray[1] = Physics2D.Raycast(new Vector2(pos.x + 0.39f, pos.y + 0.53f), Vector2.up, 0.1f, box);
        ray[2] = Physics2D.Raycast(new Vector2(pos.x - 0.39f, pos.y + 0.53f), Vector2.up, 0.1f, box);

        ray[3] = Physics2D.Raycast(new Vector2(pos.x, pos.y - 0.53f), Vector2.down, 0.1f, box);
        ray[4] = Physics2D.Raycast(new Vector2(pos.x + 0.39f, pos.y - 0.53f), Vector2.down, 0.1f, box);
        ray[5] = Physics2D.Raycast(new Vector2(pos.x - 0.39f, pos.y - 0.53f), Vector2.down, 0.1f, box);

        ray[6] = Physics2D.Raycast(new Vector2(pos.x - 0.42f, pos.y), Vector2.left, 0.1f, box);
        ray[7] = Physics2D.Raycast(new Vector2(pos.x - 0.42f, pos.y + 0.5f), Vector2.left, 0.1f, box);
        ray[8] = Physics2D.Raycast(new Vector2(pos.x - 0.42f, pos.y - 0.5f), Vector2.left, 0.1f, box);

        ray[9] = Physics2D.Raycast(new Vector2(pos.x + 0.42f, pos.y), Vector2.right, 0.1f, box);
        ray[10] = Physics2D.Raycast(new Vector2(pos.x + 0.42f, pos.y + 0.5f), Vector2.right, 0.1f, box);
        ray[11] = Physics2D.Raycast(new Vector2(pos.x + 0.42f, pos.y - 0.5f), Vector2.right, 0.1f, box);

        for (int i = 0; i < ray.Length; i++)
        {
            if (text.text == "break  box" && ray[i].collider != null && Input.GetKeyDown(KeyCode.E)) 
            {
                Destroy(ray[i].collider.gameObject);
                Instantiate(breakEffect, new Vector3(ray[i].collider.gameObject.transform.position.x, ray[i].collider.gameObject.transform.position.y, -1), Quaternion.identity); // ƒvƒŒƒnƒu‚ð¶¬
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector2(transform.position.x + rayPosX, transform.position.y + rayPosY), new Vector2(0.1f, 0));
    }
}
