using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public int bulletCount = 5;  // ������ ���� ź ��
    public Text bulletCountText;  // ź ���� ǥ���� UI Text ������Ʈ
    public Camera cam;  // ���� ī�޶�
    public AudioClip gunShotSound; // �ѼҸ� ���� Ŭ��
    private AudioSource audioSource; // ���� �ҽ�
    public GameObject gameOverUI;

    private void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        audioSource = GetComponent<AudioSource>(); // AudioSource ������Ʈ�� �����ɴϴ�.
        if (audioSource == null) // ���ٸ�, �߰��մϴ�.
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        UpdateBulletCountText();
    }

    private void Update()
    {
        // ���콺 ���� ��ư�� ������ Raycast�� �߻��մϴ�.
        if (Input.GetMouseButtonDown(0) && bulletCount > 0)
        {
            // �ѼҸ� ���
            audioSource.PlayOneShot(gunShotSound);

            RaycastHit hit;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out hit))
            {
                var target = hit.transform.gameObject;

                // Ÿ�� �������� ���߸� ������ �����մϴ�.
                if (target.CompareTag("People"))
                {
                    Destroy(target);
                    // ��ǥ �ı�
                }

                // ź ���� �ϳ� ���ҽ�ŵ�ϴ�.
                bulletCount--;
                UpdateBulletCountText();

                // �Ѿ� ���� 0�� �Ǿ��� �� ���� UI�� Ȱ��ȭ�մϴ�.
                if (bulletCount == 0)
                {
                    ActivateGameOverUI();
                }
            }
        }
    }

    // ź ���� ǥ���ϴ� UI Text�� ������Ʈ�մϴ�.
    private void UpdateBulletCountText()
    {
        bulletCountText.text = $"Bullets: {bulletCount}";
        
    }
    private void ActivateGameOverUI()
    {
        gameOverUI.SetActive(true);
        
    }
}


