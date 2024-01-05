using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GamePlayManager.Instance.SetGemCount(1);
            GameObject.Find("GameManager").GetComponent<GameManager>().AddGem();
            Destroy(this.gameObject);
        }
    }
}
