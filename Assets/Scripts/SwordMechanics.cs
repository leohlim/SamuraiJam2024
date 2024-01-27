using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Cinemachine;

public class SwordMechanics : MonoBehaviour
{
    public bool isAttacking = false;
    public bool isNextAttack = false;
    public bool shouldAttackClose = false;
    public bool canAttack = true;
    public bool swordHitboxActive = false;
    private bool isParrying = false;

    public float hitDistance;

    public float hitstopCameraFOV = 25f;

    public float hitstopAnimatorSpeed = 0.005f;

    [Range(0.01f, 2.0f)]
    public float hitstopDuration = 0.2f;

    [Range(0.01f, 5.0f)]
    public float hitstopShakeIntensity = 1.0f;

    public LayerMask _layerMask;

    private Animator swordAnimator;

    public GameObject swordCollider;
    public GameObject sparksFX;

    private AudioSource swordSoundSource;
    private AudioSource _parrySource;

    public AudioClip swordSound;
    public AudioClip parrySound;
    // Start is called before the first frame update
    void Start()
    {
        swordAnimator = GetComponent<Animator>();
        swordSoundSource = gameObject.AddComponent<AudioSource>();
        _parrySource = gameObject.AddComponent<AudioSource>();
        swordSoundSource.clip = swordSound;
        _parrySource.clip = parrySound;
    }

    // Update is called once per frame
    void Update()
    {
        swordAnimator.SetBool("CanAttack", canAttack);
        swordAnimator.SetBool("IsParrying", isParrying);

        SlashSword();

        Parry();
    }

    public void SlashSword()
    {
        if (!canAttack) return;

        if (isParrying) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            swordAnimator.SetTrigger("Slash");

            swordSoundSource.pitch = Random.Range(0.75f, 1f);

            swordSoundSource.Play();

            canAttack = false;

            //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

            //RaycastHit hit;

            //if (Physics.Raycast(ray, out hit, hitDistance, _layerMask))
            //{
            //    hit.collider.gameObject.GetComponent<Hittable>().DoJitter();
            //}
        }
    }

    public void Parry()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isParrying = true;
        }
        else
        {
            isParrying = false;
        }
    }

    public void SetCanAttack()
    {
        canAttack = true;
    }

    //public void SetSwordHitbox()
    //{
    //    swordCollider.SetActive(true);
    //}

    //public void UnsetSwordHitbox()
    //{
    //    swordCollider.SetActive(false);
    //}

    public void HitObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hitDistance, _layerMask))
        {
            if(hit.collider.gameObject.GetComponent<BasicProjectile>() != null)
            {
                HitStop(hit.transform, hitstopShakeIntensity, hitstopDuration);

                sparksFX.transform.position = hit.collider.transform.position;

                sparksFX.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                hit.collider.gameObject.GetComponent<Hittable>().DoJitter();

                sparksFX.transform.position = hit.collider.transform.position;

                sparksFX.GetComponent<ParticleSystem>().Play();
            }
        }
    }

    public void HitStop(Transform objectTransform, float intensity, float duration)
    {
        StartCoroutine(ShakeCoroutine(objectTransform, intensity, duration));
    }

    IEnumerator ShakeCoroutine(Transform hitObject, float shakeIntensity, float shakeDuration)
    {
        _parrySource.volume = 0.25f;

        _parrySource.Play();

        //save the original speed of the animator
        float originalAnimatorSpeed = swordAnimator.speed;

        //save the original camera FOV
        float originalCameraFOV = Camera.main.fieldOfView;

        //save the original speed of the projectile
        float originalProjectileMoveSpeed = hitObject.GetComponent<BasicProjectile>().moveSpeed;

        //zoom in the camera's FOV
        Camera.main.fieldOfView = hitstopCameraFOV;

        //slow down the player significantly
        swordAnimator.speed = hitstopAnimatorSpeed;

        //save the original position of the projectile before it performs the hitstop shake
        Vector3 originalPosition = hitObject.position;

        //set elapsed hitstop time to zero, since it hasn't started yet
        float elapsedTime = 0f;

        BasicProjectile projectileScript = hitObject.GetComponent<BasicProjectile>();

        while (elapsedTime < shakeDuration)
        {
            projectileScript.moveSpeed = 0;

            float randomAngle = Random.Range(0f, 360f);
            Vector2 randomOffset = Quaternion.Euler(0, 0, randomAngle) * Vector2.up * shakeIntensity;

            hitObject.position = originalPosition + new Vector3(randomOffset.x, 0, randomOffset.y);

            elapsedTime += Time.unscaledDeltaTime;

            yield return null;
        }

        hitObject.position = originalPosition;

        hitObject.forward = Camera.main.transform.forward;

        yield return new WaitForSeconds(0.1f); // A small delay to ensure the projectile stops before updating the direction

        // Switch the projectile type to 'simple' and set the initial direction


        // Explicitly set the forward direction based on the initial direction

        projectileScript.SwitchToSimple();

        projectileScript.moveSpeed = originalProjectileMoveSpeed;

        swordAnimator.speed = originalAnimatorSpeed;

        Camera.main.fieldOfView = originalCameraFOV;
    }
}
