using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverUI;  // ���� ���� UI

    private void Start()
    {
        // ���� ���� UI�� ��Ȱ��ȭ�մϴ�.
        gameOverUI.SetActive(false);
    }

    // Retry ��ư Ŭ�� �� ȣ��� �޼���
    public void OnRetryButtonClicked()
    {
        // ���� ���� ��ε��Ͽ� ������ �ٽ� �����մϴ�.
        SceneManager.LoadScene("DemoScene");
        
    }

    // Exit ��ư Ŭ�� �� ȣ��� �޼���
    public void OnExitButtonClicked()
    {
        // ������Ʈ�� �����մϴ�.
        Application.Quit();
    }
}
