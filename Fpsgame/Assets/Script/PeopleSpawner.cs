using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PeopleSpawner : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;  // ������ �����յ��� �迭
    public Sprite[] prefabImages;  // �����յ��� �̹��� �迭
    public int numberOfInstances = 10;  // ������ �������� ��
    private GameObject lastSpawnedPrefab = null;  // ���������� ������ ������

    private List<GameObject> spawnedPrefabs = new List<GameObject>();  // ������ �����յ��� ����Ʈ
    private List<Sprite> spawnedPrefabImages = new List<Sprite>();  // ������ ������ �̹������� ����Ʈ
    public static GameObject target;  // Ÿ��
    public Image targetImageUI;  // Ÿ�� �̹����� ǥ���� UI ������Ʈ

    public GameObject gameOverUI;  // ���� ���� UI

    private void Start()
    {
        for (int i = 0; i < numberOfInstances; i++)
        {
            Vector3 randomPoint;
            // NavMesh���� ��ȿ�� ��ġ�� ã�� ������ �ݺ��մϴ�.
            while (!RandomPoint(transform.position, 30.0f, out randomPoint)) ;

            int selectedIndex;
            do
            {
                // �迭���� ������ �������� �����մϴ�.
                selectedIndex = Random.Range(0, prefabsToSpawn.Length);
            }
            while (prefabsToSpawn[selectedIndex] == lastSpawnedPrefab);  // ������ ������ �����հ� �ٸ� �������� ������ ������ �ݺ�

            // ������ �������� ������ ��ġ�� �����ϰ� ������ �ν��Ͻ��� ����Ʈ�� �߰�
            GameObject instance = Instantiate(prefabsToSpawn[selectedIndex], randomPoint, Quaternion.identity);
            spawnedPrefabs.Add(instance);

            // ������ �������� �̹����� ����Ʈ�� �߰�
            spawnedPrefabImages.Add(prefabImages[selectedIndex]);

            // ���������� ������ �������� ������Ʈ
            lastSpawnedPrefab = prefabsToSpawn[selectedIndex];
        }

        // ������ �����յ� �߿��� �����ϰ� Ÿ���� �����մϴ�.
        int targetIndex = Random.Range(0, spawnedPrefabs.Count);
        target = spawnedPrefabs[targetIndex];

        // Ÿ�� �̹����� UI�� ǥ��
        targetImageUI.sprite = spawnedPrefabImages[targetIndex];
    }

    private void Update()
    {
        if (target == null)
        {
            TargetDestroyed();
        }
    }
    // NavMesh���� ������ ��ġ�� ���ø��ϴ� �޼���
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

    // Ÿ���� �ı��Ǿ��� �� ȣ��
    public void TargetDestroyed()
    {
        // ���� UI�� Ȱ��ȭ�մϴ�.
        gameOverUI.SetActive(true);
        
    }

    // ���� ���� ���·� ��ȯ�ϴ� �޼���
    public void GameOver()
    {
        // ���� UI�� Ȱ��ȭ�մϴ�.
        gameOverUI.SetActive(true);
    }
}
