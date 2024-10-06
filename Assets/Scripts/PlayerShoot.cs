using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerShoot : MonoBehaviour
{
    public Weapon weapon;
    public TMP_Text ammoText;
    public Camera mainCamera;
    public float camAimFOV;
    public float aimSpeed;

    float camDefaultFOV;
    float camFOV;
    float lerpTime;

    private void Start()
    {
        camDefaultFOV = mainCamera.fieldOfView;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            weapon.Shoot();
        }

        else if (Input.GetMouseButtonUp(0))
        {
            weapon.StopShoot();
        }
        if (weapon.currentAmmo < weapon.maxAmmo && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(weapon.Reload());
        }
        ammoText.text = "Ammo: " + weapon.currentAmmo + "/" + weapon.currentMag;
        Aim();
    }

    void Aim()
    {
        if (Input.GetMouseButton(1))
        {
            camFOV = Mathf.Lerp(camDefaultFOV, camAimFOV, lerpTime);
            lerpTime += aimSpeed * Time.deltaTime;
        }
        else
        {
            camFOV = camDefaultFOV;
            lerpTime = 0;
        }
        mainCamera.fieldOfView = camFOV;
    }

}
