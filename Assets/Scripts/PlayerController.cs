using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private GameController gameController;

    [Header("Parameters")]
    [SerializeField]
    private float bounceForce = 5; //������
    [SerializeField]
    private float dropForce = -10; //�÷��� �ı��� ���� �Ʒ��� �����ϴ� ��

    [Header("SFX")]
    [SerializeField]
    private AudioClip bounceClip; //���� ����
    [SerializeField]
    private AudioClip normalBreakClip; // �Ϲ� ���¿��� �÷��� �ı��ϴ� ����

    [Header("VFX")]
    [SerializeField]
    private Material playerMaterial; //�÷��̾ �����ϴ� material ����
    [SerializeField]
    private Transform splashImage; // �÷��̾ �÷����� �浹������ �÷�����
    [SerializeField]
    private ParticleSystem[] splashParticles; //�÷��̾ �÷����� �浹������ �����ϴ� ��ƼŬ

    private new Rigidbody rigidbody;
    private AudioSource audioSource;

    // Splash Image, Particle�� ���� ��ġ ������
    private Vector3 splashWeight = new Vector3(0, 0.22f, 0.1f);
    private bool isClicked = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!gameController.IsGamePlay) return;

        UpdateMouseButton();
        UpdateDropToSmash();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!isClicked)
        {
            if (rigidbody.velocity.y > 0) return;
            OnJumpProcess(collision);
        }
        else
        {
            if(collision.gameObject.CompareTag("BreakPart"))
            {
                var platform = collision.transform.parent.GetComponent<PlatformController>();

                if(platform.IsCollision == false)
                {
                    platform.BreakAllParts();
                    PlaySound(normalBreakClip);
                    gameController.OnCollisionWithPlatform();
                }
            }
            else if(collision.gameObject.CompareTag("NonBreakPart"))
            {
                rigidbody.isKinematic = true;
                Debug.Log("Game Over");
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (rigidbody.velocity.y > 0)
            return;
        OnJumpProcess(collision);
    }

    private void OnJumpProcess(Collision collision)
    {
        rigidbody.velocity = new Vector3(0, bounceForce, 0);
        PlaySound(bounceClip);
        OnSplashImage(collision.transform);
        OnSplashParticle();
    }

    private void UpdateMouseButton()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isClicked = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isClicked = false;
        }
    }

    private void UpdateDropToSmash()
    {
        if(Input.GetMouseButton(0) && isClicked)
        {
            rigidbody.velocity = new Vector3(0, dropForce, 0);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void OnSplashImage(Transform target)
    {
        // ���÷��� �̹����� �����ϰ�, target�� �ڽ����� ��ġ
        Transform image = Instantiate(splashImage, target);
        // ���÷��� �̹����� ��ġ, ȸ��, ũ�� ����
        image.position = transform.position - splashWeight;
        image.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        float randomScale = Random.Range(0.3f, 0.5f);
        image.localScale = new Vector3(randomScale, randomScale, 1);
        // ���÷��� �̹����� ���� ����
        image.GetComponent<MeshRenderer>().material.color = playerMaterial.color;

    }

    private void OnSplashParticle()
    {
        for(int i = 0; i < splashParticles.Length; ++i)
        {
            if (splashParticles[i].gameObject.activeSelf)
                continue;

            splashParticles[i].gameObject.SetActive(true);
            splashParticles[i].transform.position = transform.position - splashWeight;

            var mainModule = splashParticles[i].main;
            mainModule.startColor = playerMaterial.color;
            break;
        }
    }
}
