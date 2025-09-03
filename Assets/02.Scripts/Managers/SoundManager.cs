using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inspector에서 편하게 키-값 쌍으로 오디오 설정
/// </summary>
[Serializable]
public struct audioStruct
{
    public SoundType key;
    public AudioClip value;
}

/// <summary>
/// 싱글톤 기반의 사운드 매니저. BGM/효과음 분리.
/// </summary>
public class SoundManager : Singleton<SoundManager>
{
    // AudioClip을 타입별로 저장
    public Dictionary<SoundType, List<AudioClip>> audioClips = new Dictionary<SoundType, List<AudioClip>>();
    // Inspector에서 등록하기 위한 리스트
    public List<audioStruct> audioSources = new List<audioStruct>();

    private AudioSource bgmSource; // BGM 재생용
    private float sfxVolume = 0.5f; // SFX 전역 볼륨

    private void Awake()
    {
        audioClips.Clear();
        foreach (var item in audioSources)
        {
            AddClip(item.key, item.value);
        }
        PlayBGM();
    }

    /// <summary>
    /// AudioClip을 타입별로 등록
    /// </summary>
    public void AddClip(SoundType key, AudioClip clip)
    {
        if (!audioClips.ContainsKey(key))
            audioClips.Add(key, new List<AudioClip>());
        audioClips[key].Add(clip);
    }

    /// <summary>
    /// 타입에 맞는 AudioClip 리스트 반환
    /// </summary>
    public List<AudioClip> FindClip(SoundType key)
    {
        return audioClips.ContainsKey(key) ? audioClips[key] : null;
    }

    /// <summary>
    /// BGM 1곡을 반복재생
    /// </summary>
    private void PlayBGM()
    {
        var bgmList = FindClip(SoundType.BGM);

        if (bgmList != null && bgmList.Count > 0)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.clip = bgmList[0];
            bgmSource.loop = true;
            bgmSource.volume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
            bgmSource.playOnAwake = false;
            bgmSource.Play();
        }
        else
        {
            Debug.LogError("BGM이 설정되지 않았습니다");
        }
    }

    /// <summary>
    /// BGM 1곡을 정지
    /// </summary>
    public void StopBGM()
    {
        if (bgmSource != null && bgmSource.isPlaying)
            bgmSource.Stop();
    }

    /// <summary>
    /// 슬라이더에서 BGM 볼륨 변경
    /// </summary>
    public void SetBGMVolume(float value)
    {
        if (bgmSource != null)
            bgmSource.volume = value;
    }

    /// <summary>
    /// 슬라이더에서 SFX 볼륨 변경 (실제 효과음 재생할 때 사용)
    /// </summary>
    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
    }

    /// <summary>
    /// 현재 BGM 볼륨 반환 (슬라이더 초기화용)
    /// </summary>
    public float GetBGMVolume()
    {
        return bgmSource != null ? bgmSource.volume : 0.5f;
    }

    /// <summary>
    /// 현재 SFX 볼륨 반환 (슬라이더 초기화용)
    /// </summary>
    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    /// <summary>
    /// 효과음 재생. SFX 볼륨 반영!
    /// </summary>
    public void PlaySFX(SoundType type)
    {
        var sfxList = FindClip(type);
        if (sfxList != null && sfxList.Count > 0)
        {
            var sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.clip = sfxList[0];
            sfxSource.volume = sfxVolume; // 슬라이더 값 반영
            sfxSource.Play();
            Destroy(sfxSource, sfxSource.clip.length);
        }
    }

    internal void PlaySFX(object bookSound)
    {
        throw new NotImplementedException();
    }
}
