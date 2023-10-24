using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //nếu AttackArea của nhân vật va chạm với quái thì quái mất máu
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemiesHealth enemiesHealth = collision.gameObject.GetComponent<EnemiesHealth>();
        if (collision.gameObject.tag == "enemies")
        {
            enemiesHealth.TakeDamage(1);
        }
    }
}
