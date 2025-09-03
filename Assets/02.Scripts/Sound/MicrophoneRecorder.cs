using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// 마이크로 녹음한 오디오를 clipKey(SoundType 이름)에 맞춰
/// 파일로 저장, 재생, 초기화까지 처리하는 컴포넌트.
/// AudioSource, UI와 연결해서 사용.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class MicrophoneRecorder : MonoBehaviour
{
    [Tooltip("SoundType enum과 이름 일치 (예: walkSound, jumpSound)")]
    public string clipKey;

    [Tooltip("true면 3D 사운드, false면 2D 사운드")]
    public bool playAs3D = false; // 인스펙터에서 선택

    public Toggle recordToggle;
    public Button playButton;
    public Button resetButton;
    public TMP_Text statusText;

    private AudioSource audioSource;
    private string micDevice;
    private AudioClip clip;

    void Awake()
    {
        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.spatialBlend = playAs3D ? 1f : 0f;

            if (Microphone.devices.Length > 0)
                micDevice = Microphone.devices[0];
            else
                Debug.LogError("마이크가 연결되어 있지 않습니다!");

            recordToggle.onValueChanged.AddListener(OnRecordToggle);
            playButton.onClick.AddListener(OnPlayButton);
            resetButton.onClick.AddListener(OnResetButton);

            // (추가) 자동 복구: 파일이 없으면 StreamingAssets에서 복사
            string currentPath = GetCurrentSoundPath();
            string originalPath = GetOriginalSoundPath();
            if (!File.Exists(currentPath) && File.Exists(originalPath))
            {
                File.Copy(originalPath, currentPath);
                Debug.Log($"[초기화] {clipKey}.wav 파일이 없어서 StreamingAssets에서 복사함");
            }

            // 시작 시 오디오 로드
            StartCoroutine(LoadClip(currentPath));
            statusText.text = "Record 버튼을 눌러 녹음을 하고 \n한번 더 눌러서 녹음을 종료하세요!";
        }
    }

    /// <summary>
    /// 토글 ON: 녹음 시작 / OFF: 녹음 종료 및 파일 저장
    /// </summary>
    void OnRecordToggle(bool isOn)
    {
        if (isOn)
        {
            clip = Microphone.Start(micDevice, false, 60, 44100);
            statusText.text = "녹음중...";
        }
        else
        {
            Microphone.End(micDevice);
            string filePath = GetCurrentSoundPath();

            // AudioClip → WAV 저장
            SavWav.Save(filePath, clip);

            // 파일에서 다시 AudioClip 로드해서 등록
            StartCoroutine(LoadClip(filePath));
            statusText.text = "녹음완료!";
            Debug.Log("녹음 저장/덮어쓰기 완료!");
        }
    }

    /// <summary>
    /// 재생 버튼: 현재 AudioClip 재생 (없으면 파일에서 다시 로드)
    /// </summary>
    void OnPlayButton()
    {
        audioSource.spatialBlend = playAs3D ? 1f : 0f; // 매 재생마다 적용
        if (audioSource.clip != null)
        {
            audioSource.Play();
            statusText.text = "재생중...";
        }
        else
        {
            StartCoroutine(LoadClip(GetCurrentSoundPath(), true));
            statusText.text = "클립 로드 후 재생중...";
        }
    }

    /// <summary>
    /// 초기화 버튼: 원본 WAV 파일로 복구(덮어쓰기) + AudioSource/SoundManager에 등록
    /// </summary>
    void OnResetButton()
    {
        string originalPath = GetOriginalSoundPath();
        string filePath = GetCurrentSoundPath();

        if (File.Exists(originalPath))
        {
            File.Copy(originalPath, filePath, true);
            StartCoroutine(LoadClip(filePath));
            statusText.text = "초기화 완료!";
            Debug.Log("복구(초기화) 완료!");
        }
        else
        {
            statusText.text = "원본 파일이 없음!!!";
            Debug.LogWarning("원본 파일이 없습니다: " + originalPath);
        }
    }

    /// <summary>
    /// 현재 사운드 파일 경로 (persistentDataPath)
    /// </summary>
    string GetCurrentSoundPath()
    {
        return Path.Combine(Application.persistentDataPath, clipKey + ".wav");
    }

    /// <summary>
    /// 원본 사운드 파일 경로 (StreamingAssets)
    /// </summary>
    string GetOriginalSoundPath()
    {
        return Path.Combine(Application.streamingAssetsPath, "OriginalSounds", clipKey + ".wav");
    }

    /// <summary>
    /// WAV 파일을 AudioClip으로 로드해서 AudioSource/SoundManager에 등록
    /// 최신 UnityWebRequest 사용 (경고 X)
    /// </summary>
    IEnumerator LoadClip(string filePath, bool playAfterLoad = false)
    {
        string url = "file://" + filePath;
        using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV))
        {
            yield return uwr.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
            if (uwr.result != UnityWebRequest.Result.Success)
#else
            if (uwr.isNetworkError || uwr.isHttpError)
#endif
            {
                Debug.LogError("Audio load failed: " + uwr.error);
            }
            else
            {
                AudioClip newClip = DownloadHandlerAudioClip.GetContent(uwr);
                audioSource.clip = newClip;
                audioSource.spatialBlend = playAs3D ? 1f : 0f; // 불러올 때도 2D/3D 반영

                // clipKey → SoundType 자동 변환
                SoundType sType = (SoundType)System.Enum.Parse(typeof(SoundType), clipKey, true);

                // 기존 리스트 비우고 새 소리만 등록
                if (SoundManager.Instance.audioClips.ContainsKey(sType))
                    SoundManager.Instance.audioClips[sType].Clear();
                SoundManager.Instance.AddClip(sType, newClip);

                if (playAfterLoad)
                    audioSource.Play();
            }
        }
    }
}