using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 클래스를 사용하기 위해 필요

public class PlayerController : MonoBehaviour
{
    public float lookSpeed = 2.0f;
    private Vector2 rotation = Vector2.zero;

    // 스코프 줌 관련 변수들
    public Camera playerCamera; // 플레이어 카메라 참조
    public float normalFOV = 60f; // 기본 FOV
    public float zoomedFOV = 10f; // 줌 FOV
    public float zoomSpeed = 2f; // 줌 속도
    public GameObject scopeImage;
    public GameObject scopeImage2;// 스코프 이미지

    void Update()
    {
        Look();
        Scope();
    }

    void Look()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -30f, 30f);
        transform.eulerAngles = lookSpeed * new Vector2(rotation.x, rotation.y);
    }

  

    void Scope()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            playerCamera.fieldOfView = zoomedFOV;
            scopeImage.SetActive(true); // 스코프 이미지를 활성화
            scopeImage2.SetActive(false);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            playerCamera.fieldOfView = normalFOV;
            scopeImage.SetActive(false); // 스코프 이미지를 비활성화
            scopeImage2.SetActive(true);
        }
    }
}