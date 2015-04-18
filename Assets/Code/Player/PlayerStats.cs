using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour 
{
    public float Health, InvunPeriod;
    private float lastHitTime;
    private SpriteRenderer spriRen;

	// Use this for initialization
	void Start () 
    {
        spriRen = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
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

    public IEnumerator damageTaken()
    {
        spriRen.enabled = false;
        yield return new WaitForSeconds(0.1f);
        spriRen.enabled = true;
    }
}
