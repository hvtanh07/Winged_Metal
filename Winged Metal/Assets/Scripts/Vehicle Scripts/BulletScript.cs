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

    public int GetDamage()
    {
        return damage;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if ((bulletBlock.value & (1 << col.gameObject.layer)) > 0){
            gameObject.SetActive(false);
        }
            
    }
}
