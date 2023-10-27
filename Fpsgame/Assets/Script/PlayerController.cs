using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI Ŭ������ ����ϱ� ���� �ʿ�

public class PlayerController : MonoBehaviour
{
    public float lookSpeed = 2.0f;
    private Vector2 rotation = Vector2.zero;

    // ������ �� ���� ������
    public Camera playerCamera; // �÷��̾� ī�޶� ����
    public float normalFOV = 60f; // �⺻ FOV
    public float zoomedFOV = 10f; // �� FOV
    public float zoomSpeed = 2f; // �� �ӵ�
    public GameObject scopeImage;
    public GameObject scopeImage2;// ������ �̹���

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
            scopeImage.SetActive(true); // ������ �̹����� Ȱ��ȭ
            scopeImage2.SetActive(false);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            playerCamera.fieldOfView = normalFOV;
            scopeImage.SetActive(false); // ������ �̹����� ��Ȱ��ȭ
            scopeImage2.SetActive(true);
        }
    }
}