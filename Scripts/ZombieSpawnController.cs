using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class ZombieSpawnController : MonoBehaviour
{
    public int initialZombiesPerWave = 5;
    public int currentZombiesPerWave;

    public float spawnDelay = 0.5f; // delay between spawning each zombie in a w ave

    public int currentWave = 0;
    public float waveCooldown = 10.0f; // time in seconds between waves

    public bool inCooldown;
    public float cooldownCounter = 0; // only for testing and ui

    public List<Zombie> currentZombiesAlive;

    public GameObject zombiePrefab;

    public TextMeshProUGUI waveOverUI;
    public TextMeshProUGUI cooldownCounterUI;

    public TextMeshProUGUI currentWaveUI;

    private void Start()
    {
        currentZombiesPerWave = initialZombiesPerWave;

        StartNextWave();
    }

    private void StartNextWave()
    {
        currentZombiesAlive.Clear();

        currentWave++;

        currentWaveUI.text = "Wave: " + currentWave.ToString();

        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i =0; i < currentZombiesPerWave; i++)
        {
            //generate ran dom offset within range 
            Vector3 spawnOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            Vector3 spawnPosition = transform.position + spawnOffset;

            //insatntiate xombie
            var zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);

            //get zombie script
            Zombie zombieScript = zombie.GetComponent<Zombie>();

            //track ths zombie
            currentZombiesAlive.Add(zombieScript);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void Update()
    {
        //get all dead zo,bies
        List<Zombie> zombiesToRemove = new List<Zombie>();
        foreach (Zombie zombie in currentZombiesAlive)
        {
            if (zombie.IsDead)
            {
                zombiesToRemove.Add(zombie);
            }
        }

        //aciutally remove a;l dead zombies
        foreach (Zombie zombie in zombiesToRemove)
        {
            currentZombiesAlive.Remove(zombie);
        }

        zombiesToRemove.Clear();

        //start cooldown if all zomnbies dead 
        if (currentZombiesAlive.Count == 0 && inCooldown == false)
        {
            //strart cooldown for nextr wave 
            StartCoroutine(WaveCooldown());
        }

        //run cpooldown counter 
        if (inCooldown)
        {
            cooldownCounter -= Time.deltaTime;
        } else
        {
            //reset counter
            cooldownCounter = waveCooldown;
        }

        cooldownCounterUI.text = cooldownCounter.ToString("F0");
    }

    private IEnumerator WaveCooldown()
    {
        inCooldown = true;
        waveOverUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(waveCooldown);

        inCooldown = false;
        waveOverUI.gameObject.SetActive(false);

        currentZombiesPerWave *= 2; //if initial was five zombies per wave , next wave has ten zombies, then has 20 ||| can multiply by 1 if want to stay the eame 

        StartNextWave();
    }
}
