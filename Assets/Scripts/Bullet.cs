using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            BulletImpact(collision);
        }


        Destroy(gameObject);

    }

    public void BulletImpact(Collision hitObject)
    {
        ContactPoint contact = hitObject.contacts[0];
        GameObject hole = Instantiate(GlobalReferences.Instance.BulletHolePrefab, contact.point,Quaternion.LookRotation(contact.normal));
        hole.transform.SetParent(hitObject.gameObject.transform);
    }
}
