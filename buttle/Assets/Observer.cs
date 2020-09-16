using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    private Transform thisTransform;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private Vector3 rotation;

    [SerializeField]
    private Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
        thisTransform.localEulerAngles = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        thisTransform.position = targetTransform.position + offset;
        //thisTransform.localEulerAngles = rotation;
    }
}
