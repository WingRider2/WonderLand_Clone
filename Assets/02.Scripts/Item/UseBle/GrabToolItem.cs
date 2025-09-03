using UnityEngine;
using Newtonsoft.Json.Linq;

/// <summary>
/// GrabToolItem: 도구형 그랩 아이템 (SphereCast로 전방 오브젝트를 잡아당김)
/// </summary>
public class GrabToolItem : MonoBehaviour,IUsableItem , ILoadData
{
    float sphereRadius = 1.0f;
    private bool isHeld = false;

    private float throwForce;

    private void Awake()
    {
        // maxGrabDistance가 0이면 기본값 설정 (임시 해결책)
        if (throwForce <= 0)
        {
            Debug.LogWarning("[GrabTool] throwForce가 0이거나 음수! 기본값 10으로 설정");
            throwForce = 1f;
        }
    }
    public void LoadEffectData(JObject values)
    {
        //Debug.Log($"[GrabTool] LoadEffectData 호출됨! JSON: {values}");

        if (values["throwForce"] == null)
        {
            Debug.LogError("GrabToolItem: JSON에서 필수 설정값 누락!");
            throw new System.Exception("GrabToolItem: 모든 설정값은 JSON에서 필수!");
        }
        throwForce = values["throwForce"].Value<float>();

        // 디버깅용 값 출력
        //Debug.Log($"[GrabTool] 설정값 로드 완료 - throwForce: {throwForce}");
    }


    public void UsePrimary(Transform transform)// 목표물
    {
        //Debug.Log("UsePrimary 호출됨");
        //Debug.Log(isHeld);
        if (!isHeld)
        {
            // [추가] 잡는 효과음 재생
            SoundManager.Instance.PlaySFX(SoundType.grabSound);
            transform.GetComponent<GrabableItem>().UsePrimary();
            isHeld = true;
        }
        else
        {
            // [추가] 던지는 효과음 재생
            SoundManager.Instance.PlaySFX(SoundType.throwSound);
            transform.GetComponent<GrabableItem>().UseSecondary();
            transform.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
            isHeld = false;
        }
    }

    public void UseSecondary(Transform transform)
    {
        transform.GetComponent<GrabableItem>().UseSecondary();
        isHeld = false;
    }
}