using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    public static HUDManager Instance { get; set; }

    public PlayerShooting playerShooting;
    public WeaponSwitching weaponSwitching;

    [Header("Ammo")]
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public Image ammoTypeUI;

    [Header("Weapon")]
    public Image activeWeaponUI;
    public Image unActiveWeaponUI;

    [Header("Throwables")]
    public Image lethalUI;
    public TextMeshProUGUI lethalAmountUI;

    public Image tacticalUI;
    public TextMeshProUGUI tacticalAmountUI;

    public Sprite emptySlot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {

        if (playerShooting == null || playerShooting.gun == null)
        {
            return;
        }
        Gun activeGun = playerShooting.gun;
        Gun unActiveWeapon = null;

        //magazineAmmoUI.text = $"{activeGun.currentAmmo}";
        //totalAmmoUI.text = $"{activeGun.magSize}";

        magazineAmmoUI.text = activeGun.currentAmmo.ToString();
        totalAmmoUI.text = activeGun.magSize.ToString();

        //active weapon ui
        activeWeaponUI.sprite = activeGun.weaponSprite;
        ammoTypeUI.sprite = activeGun.ammoSprite;

        //find first inactive weapon
        if (weaponSwitching != null && weaponSwitching.allWeapons != null)
        {
            foreach (Gun gun in weaponSwitching.allWeapons)
            {
                if (gun != null && gun != activeGun)
                {
                    unActiveWeapon = gun;
                    break;
                }
            }
        }

        //inctive weapon ui
        if (unActiveWeapon != null)
        {
            unActiveWeaponUI.sprite = unActiveWeapon.weaponSprite;
            unActiveWeaponUI.enabled = true;
        }
        else
        {
            unActiveWeaponUI.sprite = emptySlot;
        }

        //Gun.WeaponModel model = activeGun.thisWeaponModel;
        //ammoTypeUI.sprite = GetAmmoSprite(model);

        //activeWeaponUI.sprite = GetWeaponSprite(model);

    //    //inactuve weapon
    //    foreach (Gun gun in weaponSwitching.allWeapons)
    //    {
    //        if (gun != activeGun)
    //        {
    //            unActiveWeapon = gun;
    //            break;

    //            //unActiveWeaponUI.sprite = gun.weaponSprite;
    //        }
    //    }

    //    if (unActiveWeapon != null)
    //    {
    //        unActiveWeaponUI.sprite = GetWeaponSprite(unActiveWeapon.thisWeaponModel);
    //    }
    //}

    //private Sprite GetWeaponSprite(Gun.WeaponModel model)
    //{
    //    switch (model)
    //    {
    //        case Gun.WeaponModel.Pistol:
    //            return Instantiate(Resources.Load<GameObject>("Pistol_Weapon")).GetComponent<SpriteRenderer>().sprite;


    //        case Gun.WeaponModel.Assault:
    //            return Instantiate(Resources.Load<GameObject>("Assault_Weapon")).GetComponent<SpriteRenderer>().sprite;

    //        default:
    //            return null;
    //    }
    //}

    //private Sprite GetAmmoSprite(Gun.WeaponModel model)
    //{
    //    switch(model)
    //    {
    //        case Gun.WeaponModel.Pistol:
    //            return Instantiate(Resources.Load<GameObject>("Pistol_Ammo")).GetComponent<SpriteRenderer>().sprite;

    //        case Gun.WeaponModel.Assault:
    //            return Instantiate(Resources.Load<GameObject>("Assault_Ammo")).GetComponent<SpriteRenderer>().sprite;

    //        default:
    //            return null;
    //    }
    }
}
