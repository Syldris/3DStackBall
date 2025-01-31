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

        // �ϳ��� ����ϱ� �����ٸ� �� �ڵ带 Ȱ��ȭ�ϰ� 6~7���� ���� ������ ����
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
