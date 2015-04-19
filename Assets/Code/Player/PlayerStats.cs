using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStats : MonoBehaviour 
{
    public float Health, InvunPeriod;
    private float lastHitTime;
    private SpriteRenderer spriRen;
    private GameObject fader;

	// Use this for initialization
	void Start () 
    {
        spriRen = GetComponent<SpriteRenderer>();
        fader = GameObject.Find("FadeOut");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Health <= 0)
            Die();
	}

    public void ApplyDamage(float damage)
    {
         if (Time.time > lastHitTime + InvunPeriod)
        {
            Health -= damage;

            lastHitTime = Time.time;
            StartCoroutine(damageTaken()); 
        }
	}

    public void Die()
    {
        fader.SendMessage("Fade", Application.loadedLevelName);
    }

    public IEnumerator damageTaken()
    {
        spriRen.enabled = false;
        yield return new WaitForSeconds(0.1f);
        spriRen.enabled = true;
    }
}
