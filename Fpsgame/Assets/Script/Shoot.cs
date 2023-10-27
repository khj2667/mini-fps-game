using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public int bulletCount = 5;  // 시작할 때의 탄 수
    public Text bulletCountText;  // 탄 수를 표시할 UI Text 컴포넌트
    public Camera cam;  // 메인 카메라
    public AudioClip gunShotSound; // 총소리 사운드 클립
    private AudioSource audioSource; // 사운드 소스
    public GameObject gameOverUI;

    private void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트를 가져옵니다.
        if (audioSource == null) // 없다면, 추가합니다.
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        UpdateBulletCountText();
    }

    private void Update()
    {
        // 마우스 왼쪽 버튼을 누르면 Raycast를 발사합니다.
        if (Input.GetMouseButtonDown(0) && bulletCount > 0)
        {
            // 총소리 재생
            audioSource.PlayOneShot(gunShotSound);

            RaycastHit hit;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out hit))
            {
                var target = hit.transform.gameObject;

                // 타겟 프리팹을 맞추면 게임을 종료합니다.
                if (target.CompareTag("People"))
                {
                    Destroy(target);
                    // 목표 파괴
                }

                // 탄 수를 하나 감소시킵니다.
                bulletCount--;
                UpdateBulletCountText();

                // 총알 수가 0이 되었을 때 종료 UI를 활성화합니다.
                if (bulletCount == 0)
                {
                    ActivateGameOverUI();
                }
            }
        }
    }

    // 탄 수를 표시하는 UI Text를 업데이트합니다.
    private void UpdateBulletCountText()
    {
        bulletCountText.text = $"Bullets: {bulletCount}";
        
    }
    private void ActivateGameOverUI()
    {
        gameOverUI.SetActive(true);
        
    }
}


