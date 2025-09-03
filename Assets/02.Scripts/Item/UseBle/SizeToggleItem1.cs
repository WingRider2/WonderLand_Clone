using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

// 크기 토글 아이템
public class SizeToggleItem1 : MonoBehaviour,IUsableItem, ILoadData
{
    public Vector3 smallScale = new(0.5f, 0.5f, 0.5f);
    public Vector3 largeScale = new(2f, 2f, 2f);
    public float scaleDuration = 1f;

    private Vector3 originalScale;
    private enum SizeState { Normal, Small, Large }
    private SizeState state = SizeState.Normal;

    public void LoadEffectData(JObject values)
    {
        smallScale = ParseVector3(values["smallScale"]);
        largeScale = ParseVector3(values["largeScale"]);
        scaleDuration = values["scaleDuration"].Value<float>();
    }

    private void Awake() => originalScale = transform.localScale;

    public void UsePrimary(Transform user)
    {
        // user는 플레이어 Transform
        switch (state)
        {
            case SizeState.Large:
                StartCoroutine(ScaleTo(user, originalScale));
                state = SizeState.Normal;
                break;
            case SizeState.Normal:
                StartCoroutine(ScaleTo(user, smallScale));
                state = SizeState.Small;
                break;
        }
    }

    public void UseSecondary(Transform user)
    {
        switch (state)
        {
            case SizeState.Small:
                StartCoroutine(ScaleTo(user, originalScale));
                state = SizeState.Normal;
                break;
            case SizeState.Normal:
                StartCoroutine(ScaleTo(user, largeScale));
                state = SizeState.Large;
                break;
        }
    }

    private IEnumerator ScaleTo(Transform target, Vector3 targetScale)
    {
        // [추가] 먹는 효과음 재생
        SoundManager.Instance.PlaySFX(SoundType.eatSound);
        Vector3 startScale = target.localScale;
        float elapsed = 0f;

        while (elapsed < scaleDuration)
        {
            target.localScale = Vector3.Lerp(startScale, targetScale, elapsed / scaleDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        target.localScale = targetScale;
    }


    private Vector3 ParseVector3(Newtonsoft.Json.Linq.JToken token)
    {
        var arr = token.ToObject<float[]>();
        return new Vector3(arr[0], arr[1], arr[2]);
    }
}
