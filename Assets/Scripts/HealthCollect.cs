using UnityEngine;

public class HealthCollect : MonoBehaviour
{

    [SerializeField] AudioSource healthCollect;

    private FirstAidKit firstAidKit;

    private void Start()
    {
        firstAidKit = GetComponent<FirstAidKit>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Player player = other.GetComponent<Player>();

        //don't heal if already full
        if (player.HP >= 100)
            return;

        //heal playr
        player.HP += firstAidKit.healAmount;

        //clamp to 100 hp
        if (player.HP > 100)
        {
            player.HP = 100;   
        }

        //update ui
        player.playerHealthUI.text = $"Health: {player.HP}";

        GetComponent<BoxCollider>().enabled = false;

        AudioSource.PlayClipAtPoint(healthCollect.clip, transform.position);

        Destroy(gameObject);
    }
}
