using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public VehicleSide bulletOwner;
    private int damage;
    public LayerMask bulletBlock;
    public LayerMask bulletLayer;
    public Vector2 shootPoint;


    // Start is called before the first frame update
    public void SetParameter(int damageSet, VehicleSide owner, Vector3 shootingPoint = default)
    {
        StartCoroutine(DestroyBullet());
        bulletOwner = owner;
        damage = damageSet;
        shootPoint = shootingPoint;
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
