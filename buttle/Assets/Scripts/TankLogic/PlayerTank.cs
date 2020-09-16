using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerTank : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private TankComponents tankComponents;

    private Transform tankTransform;

    private Vector3 direction;

    [SerializeField]
    private float movementSpeed = 0.0f;

    [SerializeField]
    private float rotationSpeed = 0.0f;

    [SerializeField]
    private float tankRotationSpeed = 1f;

    [SerializeField]
    private Transform sparks;
    private bool canShoot = true;
    private bool isSlow = false;

    [System.Serializable]
    private struct TankComponents
    {
        public Transform[] weels;
        public Transform tower;
        public Transform bulletInitial;
        public GameObject bullet;
    }

    void Start()
    {
        tankTransform = GetComponent<Transform>();
        direction = Vector3.zero;
    }

    void Update()
    {
        SetUpDirection();
        Shoot();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 0.5f;
        }
    }

    private void FixedUpdate()
    {
        RotateTower();
        MoveAndRotateTank();
    }


    private void SetUpDirection()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
    }

    private void MoveAndRotateTank()
    {
        tankTransform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(tankTransform.forward, -direction, tankRotationSpeed * Time.fixedDeltaTime, 0.0f));
        tankTransform.Translate(direction * movementSpeed * Time.fixedDeltaTime, Space.World);
        foreach (var weel in tankComponents.weels)
        {
            weel.Rotate(Vector3.right * rotationSpeed * Time.fixedDeltaTime * Mathf.Abs(direction.x + direction.z));
        }
    }

    /// <summary>
    /// !!!
    /// </summary>
    private void RotateTower()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            SetUpTower(hit.point);
        }
    }

    private void SetUpTower(Vector3 targetPosition)
    {
        tankComponents.tower.LookAt(targetPosition);
        Quaternion lr = tankComponents.tower.localRotation;
        lr.x = Quaternion.identity.x;
        lr.z = Quaternion.identity.z;
        tankComponents.tower.localRotation = lr;
    }


    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Instantiate(tankComponents.bullet, tankComponents.bulletInitial.position, tankComponents.tower.rotation);
            sparks.position = tankComponents.bulletInitial.position;
            sparks.rotation = tankComponents.tower.rotation;
            sparks.GetComponent<ParticleSystem>().Play();
            tankTransform.GetComponent<Rigidbody>().AddForce(-tankComponents.tower.forward * 50, ForceMode.Impulse);
            canShoot = false;
            StartCoroutine(ReloadShot());
        }
    }

    private IEnumerator ReloadShot()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        canShoot = true;
    }
}
