using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] float shootTime;
    [SerializeField] GameObject Bullet;
    [SerializeField] bool shooting = true;
    GameObject ShootPos;
    private float tempShootTime;

    // Start is called before the first frame update
    void Start()
    {
        ShootPos = GameObject.FindGameObjectWithTag("ShootPos");
        tempShootTime = shootTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (tempShootTime <= 0 && shooting)
        {
            tempShootTime = shootTime;
            Instantiate(Bullet, ShootPos.transform.position, Quaternion.identity);
        }
        else if(shooting)
            tempShootTime -= Time.deltaTime;
    }
}
