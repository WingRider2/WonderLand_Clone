using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera subCamera;
    public Transform player; // 플레이어가 누구인지 연결

    // 서브 카메라로 전환
    public void SwitchToSubCamera()
    {
        mainCamera.enabled = false;
        subCamera.enabled = true;

        // 플레이어를 바라보도록 회전
        subCamera.transform.LookAt(player);
    }

    // 다시 메인 카메라로 전환
    public void SwitchToMainCamera()
    {
        subCamera.enabled = false;
        mainCamera.enabled = true;
    }
}