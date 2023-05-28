using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject shootPoint;
    public bool bRightController = false;
    public List<GameObject> laserProjectiles;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bRightController)
        {
            if (Input.GetButtonDown("XRI_Right_TriggerButton"))
            {
                ShootProjectile();
            }
        }
        else
        {
            if (Input.GetButtonDown("XRI_Left_TriggerButton"))
            {
                ShootProjectile();
            }
        }
    }

    private void ShootProjectile() 
    {
        GameObject projectile = laserProjectiles.First();
        projectile.transform.SetPositionAndRotation(shootPoint.transform.position, Quaternion.Euler(shootPoint.transform.rotation.eulerAngles));
        laserProjectiles.Remove(laserProjectiles.First());
        laserProjectiles.Add(projectile);
        audioManager.Play("Shoot");
    }

}
