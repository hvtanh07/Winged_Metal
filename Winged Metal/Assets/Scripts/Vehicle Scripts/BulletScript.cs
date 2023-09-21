using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public TankAttack.BulletOwner bulletOwner;
    private int damage;
    

    // Start is called before the first frame update
    public void SetParameter(int damageSet, TankAttack.BulletOwner owner){
        StartCoroutine(DestroyBullet());
        bulletOwner = owner;
        damage = damageSet;
    }

    IEnumerator DestroyBullet(){
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false); 
    }
    

    void OnTriggerEnter2D(Collider2D col){
        if(bulletOwner == TankAttack.BulletOwner.ally && col.CompareTag("Enemy")){
            col.gameObject.GetComponent<TankResources>().TakeDamage(damage);
        }
    }

   
}
