using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyExplosion());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DestroyExplosion()
    {
        yield return new WaitForSecondsRealtime(1);
        Destroy(gameObject);
    }
}
