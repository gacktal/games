using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private float maxDistance = 10;
    private Vector3 startPosition;

    private Transform bulletTransform;

    [SerializeField]
    private float speed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        bulletTransform = transform;
        startPosition = bulletTransform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        bulletTransform.Translate(bulletTransform.forward * speed * Time.fixedDeltaTime, Space.World);
        if (Vector3.Distance(startPosition, bulletTransform.position) >= maxDistance)
        {
            DestroyBullet();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        var expl = Instantiate(explosion, transform.position, transform.rotation);
        expl.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
    }
}
