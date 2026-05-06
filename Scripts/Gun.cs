using UnityEngine;
using System.Collections;
using TMPro;

public class Gun : MonoBehaviour
{

    public float reloadTime = 1f;
    public float fireRate = 0.15f;
    public int magSize = 20;

    public GameObject bullet;
    public Transform bulletSpawnPoint;

    public int currentAmmo { get; private set; }
    private bool isReloading = false;
    private float nextTimeToFire = 0f;
    public bool isShooting; 

    private Quaternion initialRotation;
    private Vector3 initialPosition;
    private Vector3 reloadRotationOffset = new Vector3(66, 50, 50);

    public Camera fpsCam;
    //public ParticleSystem muzzleFlash;

    public int weaponDamage;

    public GameObject muzzleEffect;

    public enum WeaponModel
    {
        Pistol,
        Assault,
        Shotgun
    }

    public WeaponModel thisWeaponModel;

    


    private void Start()
    {
        currentAmmo = magSize;
        initialRotation = transform.localRotation;
        initialPosition = transform.localPosition;

    }

    public void Shoot()
    {
        
        // muzzleEffect.GetComponent<ParticleSystem>().Play();

        //SoundManager.Instance.shootingSoundPistol.Play();



        if (isReloading) return;
        if (Time.time < nextTimeToFire) return;

        //added
        if (currentAmmo <= 0)
        {
            SoundManager.Instance.emptyMagazineSoundPistol.Play();
            return;
        }

        //if (currentAmmo <= 0)
        //{
        //    StartCoroutine(Reload());
        //    return;
        //}

        nextTimeToFire = Time.time + fireRate;
        currentAmmo--;
        SoundManager.Instance.PlayShootingSound(thisWeaponModel);

        //Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        GameObject b = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Debug.Log("Spawned bullet at: " + b.transform.position);

        Debug.DrawRay(bulletSpawnPoint.position, bulletSpawnPoint.forward * 5f, Color.red, 2f);
        Debug.Log(bulletSpawnPoint.position);

        //muzzleFlash.Play();
        muzzleEffect.GetComponent<ParticleSystem>().Play();
    }

    private void Reload()
    {
        //SoundManager.Instance.reloadingSoundPistol.Play();

        SoundManager.Instance.PlayReloadSound(thisWeaponModel);

        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        currentAmmo = magSize;
        isReloading = false;
    }

    private void Update()
    {

        //if (currentAmmo <= 0 && isShooting)
        //{
        //    SoundManager.Instance.emptyMagazineSoundPistol.Play();
        //}

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magSize && isReloading == false)
        {
            Reload();
        }

        if (AmmoManager.Instance.ammoDisplay != null)
        {
            AmmoManager.Instance.ammoDisplay.text = $"{currentAmmo}/{magSize}";
        }
    }
    //IEnumerator Reload()
    //{
    //    isReloading = true;

    //    Quaternion targetRotation = Quaternion.Euler(initialRotation.eulerAngles + reloadRotationOffset);
    //    float halfReload = reloadTime / 2f;
    //    float t = 0f;

    //    while (t < halfReload)
    //    {
    //        t += Time.deltaTime;
    //        transform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, t / halfReload);
    //        yield return null;
    //    }

    //    t = 0f;

    //    while(t < halfReload)
    //    {
    //        t += Time.deltaTime;
    //        transform.localRotation = Quaternion.Slerp(targetRotation, initialRotation, t / halfReload);
    //        yield return null;
    //    }

    //    currentAmmo = magSize;
    //    isReloading = false;
    //}

    //public void TryReload()
    //{
    //    if (isReloading) return;
    //    if (currentAmmo == magSize) return;

    //    StartCoroutine(Reload());
    //}


}
