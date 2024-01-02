using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSystem : MonoBehaviour
{
    public GameObject startPutIn;
    public GameObject fade;
    public bool start = false;

    private float fadeDelta;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        fade.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (fade.transform.localScale.y > 0)
        {
            fadeDelta = fadeDelta + Time.deltaTime * 10;
            fade.transform.localScale = new Vector3(20, 10 - fadeDelta, 1);
        }

        if (fade.transform.localScale.y <= 0)
        {
            fade.transform.localScale = new Vector3(20, 0, 1);
            Vector3 pos = startPutIn.transform.position;
            Vector2 direction = Vector2.down;
            float maxDistance = 0.1f;
            int layerMask = 1 << LayerMask.NameToLayer("phrase");
            RaycastHit2D[] hits = Physics2D.RaycastAll(pos, direction, maxDistance, layerMask);

            if (Input.GetMouseButtonUp(0))
            {
                foreach (RaycastHit2D hit in hits)
                {
                    string name = hit.collider.gameObject.name;

                    if (name == "start")
                    {
                        start = true;
                    }
                    else if (name == "end")
                    {
                        Quit();
                    }
                }
            }

            startFade startFade = this.GetComponent<startFade>();

            if (startFade.next)
            {
                time += 1 * Time.deltaTime;

                if (time > 1)
                {
                    SceneManager.LoadScene("Select");
                }
            }
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }
}
