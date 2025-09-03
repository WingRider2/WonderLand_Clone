using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 슬라이더로 BGM/SFX 볼륨을 조절하는 UI 관리용 스크립트
/// </summary>
public class AudioSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    void Start()
    {
        // PlayerPrefs에 저장된 값 또는 현재 SoundManager 값으로 슬라이더 초기화
        float bgm = PlayerPrefs.GetFloat("BGMVolume", SoundManager.Instance.GetBGMVolume());
        float sfx = PlayerPrefs.GetFloat("SFXVolume", SoundManager.Instance.GetSFXVolume());

        bgmSlider.value = bgm;
        sfxSlider.value = sfx;

        // 슬라이더 값 반영
        SoundManager.Instance.SetBGMVolume(bgm);
        SoundManager.Instance.SetSFXVolume(sfx);

        // 값 변경 리스너 등록
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetBGMVolume(float value)
    {
        SoundManager.Instance.SetBGMVolume(value);
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        SoundManager.Instance.SetSFXVolume(value);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}
