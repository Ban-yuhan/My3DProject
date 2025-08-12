using NUnit.Framework.Constraints;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 2f, -5f); //쫒아갈 대상을 기준으로할 카메라 위치 지정. 대상의 위치가 바뀌면 대상 위치에 이 오프셋값을 더해 최종 위치 세팅
    public float smooth = 0.15f;

    private void LateUpdate() // update함수가 호출된 이후에 호출 → 플레이어가 이동한 다음에 카메라가 따라가도록 하기 위함
    {
        if (target != null)
        { 
            Vector3 desiredPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPos, smooth);

            transform.LookAt(target); //카메라가 바라 볼 대상 지정
        }
    }
}
