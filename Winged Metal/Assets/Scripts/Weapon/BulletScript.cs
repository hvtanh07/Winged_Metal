using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public VehicleAttack.BulletOwner bulletOwner;
    private int damage;
    public LayerMask bulletBlock;
    public LayerMask bulletLayer;


    // Start is called before the first frame update
    public void SetParameter(int damageSet, VehicleAttack.BulletOwner owner)
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
        if ((bulletBlock.value & (1 << col.gameObject.layer)) > 0)
        {
            gameObject.SetActive(false);
        }
        else if ((bulletLayer.value & (1 << col.gameObject.layer)) > 0)
        {
            BulletScript bullet = col.gameObject.GetComponent<BulletScript>();
            if (bullet.bulletOwner != bulletOwner)
                gameObject.SetActive(false);
        }
    }
}
