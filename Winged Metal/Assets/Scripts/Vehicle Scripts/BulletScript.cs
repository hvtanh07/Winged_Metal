using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public TankAttack.BulletOwner bulletOwner;
    private int damage;
    public LayerMask bulletBlock;


    // Start is called before the first frame update
    public void SetParameter(int damageSet, TankAttack.BulletOwner owner)
    {
        StartCoroutine(DestroyBullet());
        bulletOwner = owner;
        damage = damageSet;
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if ((bulletOwner == TankAttack.BulletOwner.ally || bulletOwner == TankAttack.BulletOwner.player) && col.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<TankResources>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
        if (bulletOwner == TankAttack.BulletOwner.enemy && (col.CompareTag("Player") || col.CompareTag("Ally")))
        {
            col.gameObject.GetComponent<TankResources>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
        if ((bulletBlock & (1 << col.gameObject.layer)) != 0)
        {
            gameObject.SetActive(false);
        }           
    }


}
