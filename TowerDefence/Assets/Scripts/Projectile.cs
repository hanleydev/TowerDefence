//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private Transform target;

    public float speed = 70f;
    public GameObject projectileImpact;

    public void Seek (Transform _target)
    {
        target = _target;
    }

    void Update() {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame) {
            HitTarget();
            return;
        }

        transform.Translate (dir.normalized * distanceThisFrame, Space.World);
	}

    void HitTarget() {
        GameObject effectIns = (GameObject)Instantiate(projectileImpact, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        Destroy(target.gameObject);
        Destroy(gameObject);
    }
}
