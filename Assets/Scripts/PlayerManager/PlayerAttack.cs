using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator ani;
    public GameObject attackArea;
    private bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            Attack();
        }
    }
    void Attack()
    {
        isAttacking = true;
        ani.SetTrigger("attack2");
        attackArea.SetActive(true);
        StartCoroutine(TurnOffAttackArea());
    }
    IEnumerator TurnOffAttackArea()
    {
        yield return new WaitForSeconds(0.5f);
        attackArea.SetActive(false);
        isAttacking = false;
    }
    
    
}
