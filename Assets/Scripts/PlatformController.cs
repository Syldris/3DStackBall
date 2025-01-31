using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField]
    private PlatformPartController[] parts;
    [SerializeField]
    private float removeDuration = 1;

    public bool IsCollision { private set; get; } = false;

/*    private void Start()
    {
        Invoke(nameof(BreakAllParts), Mathf.Abs(transform.position.y));
    }*//*    private void Start()
    {
        Invoke(nameof(BreakAllParts), Mathf.Abs(transform.position.y));
    }*/

    public void BreakAllParts()
    {
        if(IsCollision == false)
        {
            IsCollision = true;
        }

        if(transform.parent != null)
        {
            transform.parent = null;
        }

        // 하나씩 등록하기 귀찮다면 이 코드를 활성화하고 6~7줄의 변수 선언은 삭제
        foreach(PlatformPartController part in parts)
        {
            part.BreakingPart();
        }

        StartCoroutine(RemoveParts());
    }


    private IEnumerator RemoveParts()
    {
        yield return new WaitForSeconds(removeDuration);

        gameObject.SetActive(false);
    }
}
