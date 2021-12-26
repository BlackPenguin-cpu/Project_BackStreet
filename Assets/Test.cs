using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Test : MonoBehaviour
{
    public GameObject obj;
    public TextMeshPro tmpro;
    void Update()
    {
        this.gameObject.GetComponent<SpriteRenderer>().size = new Vector2(this.gameObject.GetComponent<SpriteRenderer>().size.x, tmpro.preferredHeight + 0.2f);
        this.gameObject.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + tmpro.preferredHeight/2);
        tmpro.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + tmpro.preferredHeight / 2);
    }
}
