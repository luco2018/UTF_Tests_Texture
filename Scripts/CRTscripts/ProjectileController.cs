using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    private bool hasHit;

    void OnCollisionEnter(Collision col)
    {
        if (!hasHit)
        {
            hasHit = true;
            Vector3 contactPoint = col.contacts[0].point;
            GameObject go = col.gameObject;
            Vector3 coord =
                new Vector3(
                 0.5f - (Mathf.Abs(contactPoint.x) - Mathf.Abs(col.transform.position.x)),
                Mathf.Abs(contactPoint.z) - Mathf.Abs(col.transform.position.z) + 0.5f,
                0
                );

            CustomTextureHandler cth = go.GetComponent<CustomTextureHandler>();
            if (cth != null)
                cth.Hit(coord);

        }
    }

}
