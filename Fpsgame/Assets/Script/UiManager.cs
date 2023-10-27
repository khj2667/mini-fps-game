using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverUI;  // 게임 오버 UI

    private void Start()
    {
        // 게임 오버 UI를 비활성화합니다.
        gameOverUI.SetActive(false);
    }

    // Retry 버튼 클릭 시 호출될 메서드
    public void OnRetryButtonClicked()
    {
        // 현재 씬을 재로드하여 게임을 다시 시작합니다.
        SceneManager.LoadScene("DemoScene");
        
    }

    // Exit 버튼 클릭 시 호출될 메서드
    public void OnExitButtonClicked()
    {
        // 프로젝트를 종료합니다.
        Application.Quit();
    }
}
