using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BuffEffect;
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
            BuffEffect.SetActive(true);
            StartCoroutine(GetBuff());
            GameObject.Find("GameManager").GetComponent<GameManager>().StartBuff();
            this.GetComponent<Animator>().SetInteger("EndBuff", 1);
        }
    }
    IEnumerator GetBuff()
    {
        yield return new WaitForSeconds(1f);        
        Destroy(this.gameObject);
    }
}

