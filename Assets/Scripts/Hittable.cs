using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Hittable : MonoBehaviour
{
    private AudioSource _source;
    public AudioClip _clip;
    public AudioClip _destroyClip;

    public float maxHP = 5f;
    public float currentHP;
    public float hitDistance = 2f;

    private Material _material;

    private LayerMask _layerMask;

    private StarterAssetsInputs _inputs;

    private void Start()
    {
        _inputs = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();

        _source = gameObject.AddComponent<AudioSource>();

        _source.clip = _clip;

        currentHP = maxHP;

        Renderer _renderer;

        if(GetComponent<Renderer>() != null)
        {
            _renderer = GetComponent<Renderer>();
        }
        else
        {
            _renderer = transform.GetChild(0).GetComponent<Renderer>();
        }

        _material = _renderer.material;

        _layerMask = LayerMask.GetMask("hittable");
    }

    public void DoJitter()
    {
        StartCoroutine(SmallJitter());
    }

    IEnumerator SmallJitter()
    {
        _source.pitch = Random.Range(0.75f, 1f);

        _source.Play();

        currentHP -= 1;

        if(currentHP <= 0)
        {
            StartCoroutine(PlayDestroySound());
        }

        //Color change functionality
        float normalizedHP = Mathf.Clamp01(1 - (currentHP / maxHP));
        Color targetColor = Color.Lerp(Color.white, new Color(0.1f, 0.1f, 0.1f), normalizedHP);
        _material.color = targetColor;

        transform.position = new Vector3(transform.position.x, transform.position.y + 0.075f, transform.position.z);

        yield return new WaitForSeconds(0.05f);

        transform.position = new Vector3(transform.position.x, transform.position.y - 0.075f, transform.position.z);
    }

    private IEnumerator PlayDestroySound()
    {
        if (_source != null && _clip != null)
        {
            _source.pitch = 1f;
            _source.clip = _destroyClip;

            _source.PlayOneShot(_destroyClip);

            if(GetComponent<Renderer>() != null)
            {
                GetComponent<Renderer>().enabled = false;
            }
            else
            {
                transform.GetChild(0).GetComponent<Renderer>().enabled = false;
            }

            GetComponent<Collider>().enabled = false;

            yield return new WaitForSeconds(_destroyClip.length);

            Destroy(gameObject);
        }
    }
}
