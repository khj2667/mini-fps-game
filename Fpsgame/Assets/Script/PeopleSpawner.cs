using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PeopleSpawner : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;  // 스폰할 프리팹들의 배열
    public Sprite[] prefabImages;  // 프리팹들의 이미지 배열
    public int numberOfInstances = 10;  // 스폰할 프리팹의 수
    private GameObject lastSpawnedPrefab = null;  // 마지막으로 스폰한 프리팹

    private List<GameObject> spawnedPrefabs = new List<GameObject>();  // 생성된 프리팹들의 리스트
    private List<Sprite> spawnedPrefabImages = new List<Sprite>();  // 생성된 프리팹 이미지들의 리스트
    public static GameObject target;  // 타겟
    public Image targetImageUI;  // 타겟 이미지를 표시할 UI 컴포넌트

    public GameObject gameOverUI;  // 게임 오버 UI

    private void Start()
    {
        for (int i = 0; i < numberOfInstances; i++)
        {
            Vector3 randomPoint;
            // NavMesh에서 유효한 위치를 찾을 때까지 반복합니다.
            while (!RandomPoint(transform.position, 30.0f, out randomPoint)) ;

            int selectedIndex;
            do
            {
                // 배열에서 랜덤한 프리팹을 선택합니다.
                selectedIndex = Random.Range(0, prefabsToSpawn.Length);
            }
            while (prefabsToSpawn[selectedIndex] == lastSpawnedPrefab);  // 이전에 생성한 프리팹과 다른 프리팹을 선택할 때까지 반복

            // 선택한 프리팹을 랜덤한 위치에 생성하고 생성된 인스턴스를 리스트에 추가
            GameObject instance = Instantiate(prefabsToSpawn[selectedIndex], randomPoint, Quaternion.identity);
            spawnedPrefabs.Add(instance);

            // 선택한 프리팹의 이미지를 리스트에 추가
            spawnedPrefabImages.Add(prefabImages[selectedIndex]);

            // 마지막으로 스폰한 프리팹을 업데이트
            lastSpawnedPrefab = prefabsToSpawn[selectedIndex];
        }

        // 생성된 프리팹들 중에서 랜덤하게 타겟을 설정합니다.
        int targetIndex = Random.Range(0, spawnedPrefabs.Count);
        target = spawnedPrefabs[targetIndex];

        // 타겟 이미지를 UI에 표시
        targetImageUI.sprite = spawnedPrefabImages[targetIndex];
    }

    private void Update()
    {
        if (target == null)
        {
            TargetDestroyed();
        }
    }
    // NavMesh에서 랜덤한 위치를 샘플링하는 메서드
    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }

    // 타겟이 파괴되었을 때 호출
    public void TargetDestroyed()
    {
        // 종료 UI를 활성화합니다.
        gameOverUI.SetActive(true);
        
    }

    // 게임 오버 상태로 전환하는 메서드
    public void GameOver()
    {
        // 종료 UI를 활성화합니다.
        gameOverUI.SetActive(true);
    }
}
