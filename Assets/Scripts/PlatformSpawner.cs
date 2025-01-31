using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField]
    private PlatformShape[] allPlatforms; //모든 플랫폼 정보
    [SerializeField]
    private Transform lastPlatform; //마지막 플랫폼 정보

    /// <summary>
    /// 플랫폼 생성 및 설정, 마지막 플랫폼 위치 선정
    /// </summary>
    public int SpawnPlatforms()
    {
        // 같은 모양의 플랫폼만 출력되도록 플랫폼 셋 선택
        Transform[] platforms = SetupPlatformFigure();
        // 플랫폼 생성 개수 설정
        int platformCount = SetupPlatformCount();
        // 선택된 플랫폼들의 시작, 종료 인덱스(난이도 조절)
        var indexs = SetupStartAndEndIndex(platforms);

        //스테이지에 배치된는 일반 플랫폼들을 생성하고, 위치, 회전, 부모 설정
        for(int i = 0; i< platformCount; ++i)
        {
            Transform platform = Instantiate(platforms[Random.Range(indexs.Item1, indexs.Item2)]);

            platform.position = new Vector3(0, -i * 0.5f, 0);
            platform.eulerAngles = new Vector3(0, -i * 5, 0);

            // 매 5번째 플랫폼마다 50%의 확률로 180도 더 회전한다(난이도 조절)
            if(i != 0 && i % 5 ==0 && Random.Range(0, 2) == 1)
            {
                platform.eulerAngles += Vector3.up * 180;
            }

            platform.SetParent(transform);
        }

        //마지막 플랫폼의 위치 설정
        lastPlatform.position = new Vector3(0, -platformCount * 0.5f, 0);

        return platformCount;
    }

    /// <summary>
    /// 플랫폼 배치에 사용할 도형 선택
    /// </summary>
    private Transform[] SetupPlatformFigure()
    {
        int index = Random.Range(0, allPlatforms.Length - 1); //

        Transform[] selectedPlatforms = new Transform[allPlatforms[index].platforms.Length];
        for(int i = 0; i < allPlatforms[index].platforms.Length; ++i)
        {
            selectedPlatforms[i] = allPlatforms[index].platforms[i];
        }

        return selectedPlatforms;
    }

    /// <summary>
    /// 레벨에 따라 생성되는 플랫폼 난이도 설정
    /// </summary>
    private(int,int) SetupStartAndEndIndex(Transform[] platforms)
    {
        int level = PlayerPrefs.GetInt("LEVEL");

        float startDuraction = 0.05f;
        float endDuration = 0.1f;

        // level 0 ~ 19 : 0
        // level 20 ~ 39 : 1
        // level 40 ~ 59 : 2
        // ..
        int startIndex = Mathf.Min((int)(level * startDuraction), platforms.Length - 1);

        //level 0 ~ 9 : 2
        //level 10 ~ 19 : 3
        //level 20 ~ 29 : 4
        //...
        int endIndex = Mathf.Min((int)(level * endDuration) + 2, platforms.Length);

        return (startIndex, endIndex);
    }

    /// <summary>
    /// 현재 렙레에 생성되는 플랫폼 개수 설정
    /// </summary>
    private int SetupPlatformCount()
    {
        int level = PlayerPrefs.GetInt("LEVEL");
        int baseCount = 20;
        // bastCount * ((level+10)/10): 10스테이지마다 baseCount에 설정된 개수인 20개씩 증가
        // (int)(level%10 * 1.5f): 매 스테이지 마다 0, 1, 3, 4, 6.. 개수 증가
        int platformCount = baseCount * ((level + 10) / 10) + (int)(level % 10 * 1.5f);

        return platformCount;
    }
    // 플랫폼의 모양에 따라 최대 개수가 다르기 때문에 모양별로 저장할 수 있도록 구조체 정의
    [System.Serializable]
    private struct PlatformShape
    {
        public Transform[] platforms;
    }
}
