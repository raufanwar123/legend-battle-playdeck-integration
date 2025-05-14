using System.Collections;
using ControlFreak2;
using UnityEngine;
using UnityEngine.UI;

public class RocketLauncher : MonoBehaviour
{
    public TouchButton [] fireButton;
    public GameObject AutoFireBtn;

    public ParticleSystem dustParticleSystem;

    public GameObject missile;
	public Transform spawnPoint;

    public WeaponBehavior rocketLauncherWeaponBehaviour;
    public Animator rocketLauncherAnim;

    public float nextFireTime;
    public bool isFire;

    private WaitForSeconds wfs;
    private void OnDisable()
    {
        isFire = false;
        FireBtnState(true);
        //dummyFireButton.SetActive(false);
        //AutoFireBtn.SetActive(true);

        dustParticleSystem.Stop();

        //if (GameConfiguration.GetAutoFirePref() == 1)
        //{
        //    if (rocketLauncherWeaponBehaviour.FPSPlayerComponent)
        //        rocketLauncherWeaponBehaviour.FPSPlayerComponent.TurnAutoFire = true;

        //}
        //else
        //{
        //    if (rocketLauncherWeaponBehaviour.FPSPlayerComponent)
        //        rocketLauncherWeaponBehaviour.FPSPlayerComponent.TurnAutoFire = false;
        //}
    }

    private void OnEnable()
    {
        //AutoFireBtn.SetActive(false);
        //if(rocketLauncherWeaponBehaviour.FPSPlayerComponent)
        //    rocketLauncherWeaponBehaviour.FPSPlayerComponent.TurnAutoFire = false;
    }
    void FireBtnState(bool status)
    {
        for (int i = 0; i < fireButton.Length; i++)
        {
            fireButton[i].gameObject.SetActive(status);
        }
    }
    private void Start()
    {
        wfs = new WaitForSeconds(nextFireTime);
    }

    private void Update()
    {
        if (rocketLauncherWeaponBehaviour.ammo == 0)
        {
            FireBtnState(false);
            return;
        }
        if (CF2Input.GetButtonDown("Fire") && !isFire && rocketLauncherWeaponBehaviour.ammo > 0)
        {
            isFire = true;
            FireBtnState(false);
            dustParticleSystem.Play();
            GameObject clone = Instantiate(missile);
            clone.transform.parent = spawnPoint;
            clone.transform.localPosition = Vector3.zero;
            clone.transform.localRotation = Quaternion.Euler(90, 0, 0);
            clone.transform.parent = null;
            rocketLauncherAnim.SetTrigger("Reload");
            StartCoroutine(Wait());
        }
    }
    public void RocketLauncherFire()
    {
        if (!isFire && rocketLauncherWeaponBehaviour.ammo > 0)
        {
            isFire = true;
            FireBtnState(false);
            dustParticleSystem.Play();
            GameObject clone = Instantiate(missile);
            clone.transform.parent = spawnPoint;
            clone.transform.localPosition = Vector3.zero;
            clone.transform.localRotation = Quaternion.Euler(90, 0, 0);
            clone.transform.parent = null;
            rocketLauncherAnim.SetTrigger("Reload");
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        yield return wfs;
        isFire = false;
        GameStat.instance.FireBtnDisable(true);
        dustParticleSystem.Stop();
        StopCoroutine(Wait());
    }
}
