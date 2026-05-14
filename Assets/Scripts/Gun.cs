using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{

    public float reloadTime = 1f;
    public float fireRate = 0.15f;
    public int magSize = 20; //reserve ammo in ui
    public int magCapacity = 20; //bullets per reload

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

    public Sprite weaponSprite;
    public Sprite ammoSprite;

    private Animator animator;

    bool isADS;


    public enum WeaponModel
    {
        Pistol,
        Assault,
        Shotgun
    }

    public WeaponModel thisWeaponModel;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }




    private void Start()
    {
        currentAmmo = magCapacity;
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
        if (isADS)
        {
            animator.SetTrigger("RECOIL_ADS");
        }
        else
        {
            animator.SetTrigger("RECOIL");
        }
            
    }

    private void Reload()
    {
        //SoundManager.Instance.reloadingSoundPistol.Play();

        SoundManager.Instance.PlayReloadSound(thisWeaponModel);

        animator.SetTrigger("RELOAD");

        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        int bulletsNeeded = magCapacity - currentAmmo;

        int bulletsToLoad = Mathf.Min(bulletsNeeded, magSize);

        currentAmmo += bulletsToLoad;

        magSize -= bulletsToLoad;

        isReloading = false;
        //currentAmmo = magSize;
        //isReloading = false;
    }

    private void Update()
    {

        //if (currentAmmo <= 0 && isShooting)
        //{
        //    SoundManager.Instance.emptyMagazineSoundPistol.Play();
        //}

        //if (Input.GetMouseButton(1))
        //{
        //    animator.SetBool("isADS", true);
        //}
        //else
        //{
        //    animator.SetBool("isADS", false);
        //}

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("enterADS");
            isADS = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            animator.SetTrigger("exitADS");
            isADS = false;
        }


        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magSize && isReloading == false && magSize > 0)
        {
            Reload();
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
