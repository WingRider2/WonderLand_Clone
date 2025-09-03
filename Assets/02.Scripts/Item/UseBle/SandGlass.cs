using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모래시계 (부서진 구조물 복구 / 파괴)
public class SandGlass : MonoBehaviour, IUsableItem, ILoadData
{
    public void LoadEffectData(JObject effectValues)
    {
    }

    public void UsePrimary(Transform target)
    {
        if (target == null) return;

        if (target.TryGetComponent<Obstacle>(out var obstacle))
        {
            // [추가] 부수는 효과음 재생
            SoundManager.Instance.PlaySFX(SoundType.breakSound);
            obstacle.Getup();
        }
    }

    public void UseSecondary(Transform target)
    {
        if (target == null) return;

        if (target.TryGetComponent<Obstacle>(out var obstacle))
        {
            // [추가] 복구 효과음 재생
            SoundManager.Instance.PlaySFX(SoundType.returnSound);
            obstacle.Breaks();
        }
    }
}