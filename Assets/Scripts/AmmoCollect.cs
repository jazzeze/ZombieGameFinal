using UnityEngine;

public class AmmoCollect : MonoBehaviour
{

    [SerializeField] AudioSource ammoCollect;

    private AmmoBox ammoBox;

    private void Start()
    {
        ammoBox = GetComponent<AmmoBox>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Gun[] guns = FindObjectsByType<Gun>(FindObjectsSortMode.None);

        foreach (Gun gun in guns)
        {
            switch (ammoBox.ammoType)
            {
                case AmmoBox.AmmoType.PistolAmmo:
                    if (gun.thisWeaponModel == Gun.WeaponModel.Pistol)
                    {
                        gun.magSize += ammoBox.ammoAmount;
                    }
                    break;

                case AmmoBox.AmmoType.AssaultAmmo:
                    if (gun.thisWeaponModel == Gun.WeaponModel.Assault)
                    {
                        gun.magSize += ammoBox.ammoAmount;
                    }
                    break;
            }
        }

        GetComponent<BoxCollider>().enabled = false;

        AudioSource.PlayClipAtPoint(ammoCollect.clip, transform.position);

        Destroy(gameObject);
    }
}
