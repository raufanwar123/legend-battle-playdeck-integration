using System.Collections;
using UnityEngine;

public class MissileAddForce : MonoBehaviour
{
    public GameObject particleEffect;
    public GameObject missileModel;
    

    public float velocity;
    public bool isDisableMissile = true;
    private AudioSource missileDestroySound;
    private Rigidbody rb;
    private ExplosiveObject ExplosiveObjectComponent;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ExplosiveObjectComponent = GetComponent<ExplosiveObject>();
        missileDestroySound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if(!isDisableMissile)
            StartCoroutine(DisableMissile());
    }

    private void FixedUpdate()
    {
        if(isDisableMissile)
            rb.velocity = transform.forward * velocity * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision: " + collision.gameObject.name);
        missileModel.SetActive(false);
        missileDestroySound.Play();
        ExplosiveObjectComponent.ApplyDamage(ExplosiveObjectComponent.hitPoints + 1.0f);//detonate grenade
        particleEffect.SetActive(true);
        StartCoroutine(DisableParticleEffect());
    }

    IEnumerator DisableParticleEffect()
    {
        yield return new WaitForSeconds(2.25f);
        particleEffect.SetActive(false);
        StopCoroutine(DisableParticleEffect());
        StartCoroutine(DisableMissile(2));
    }

    IEnumerator DisableMissile(float time = 5f)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}
