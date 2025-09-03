using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsController : MonoBehaviour
{
    public RectTransform settingsPanel;    // 세팅 화면 (팝업 UI)의 RectTransform
    public CanvasGroup settingsCanvasGroup; // CanvasGroup을 사용하여 알파(투명도) 조절
    public Button settingsButton;          // 세팅 화면을 띄울 세팅 버튼
    public Button closeButton;             // 세팅 화면을 닫을 X 버튼
    public Vector3 hiddenScale = Vector3.zero;  // 세팅 화면의 숨겨져 있을 때 크기 (크기 0)
    public Vector3 visibleScale = Vector3.one;  // 세팅 화면의 보일 때 크기 (크기 1)

    private bool isSettingsVisible = false;  // 세팅 화면이 보이는지 여부를 체크하는 변수

    void Start()
    {
        // 세팅 버튼 클릭 시 세팅 화면을 나타내거나 숨기도록 연결
        settingsButton.onClick.AddListener(ToggleSettings);

        // X 버튼 클릭 시 세팅 화면을 숨기도록 연결
        closeButton.onClick.AddListener(CloseSettings);

        // 세팅 화면을 처음에는 숨겨둡니다.
        settingsPanel.localScale = hiddenScale;
        settingsCanvasGroup.alpha = 0; // 초기 알파 값은 0 (투명)
    }

    void ToggleSettings()
    {
        if (isSettingsVisible)
        {
            // 세팅 화면이 보일 때는 숨김 처리
            CloseSettings();
        }
        else
        {
            // 세팅 화면이 안 보일 때는 보이게 처리
            ShowSettings();
        }

        // 세팅 화면 상태를 반전시킴
        isSettingsVisible = !isSettingsVisible;
    }

    void ShowSettings()
    {
        // 세팅 화면을 화면 가운데로 확장하고 알파를 1로 변경하여 나타나게 함
        settingsPanel.DOScale(visibleScale, 0.5f).SetEase(Ease.OutBack); // 0.5초 동안 크기 확장
        settingsCanvasGroup.DOFade(1, 0.5f); // 0.5초 동안 알파 값이 1로 변경
    }

    void CloseSettings()
    {
        // 세팅 화면을 사라지게 할 때는 크기 0으로 줄이고, 알파는 0으로 줄여서 사라지게 함
        settingsPanel.DOScale(hiddenScale, 0.5f).SetEase(Ease.InBack) // 0.5초 동안 크기 축소
            .OnKill(() =>
            {
                // 애니메이션이 끝난 후 상태를 갱신
                isSettingsVisible = false;
            });
        settingsCanvasGroup.DOFade(0, 0.5f); // 0.5초 동안 알파 값이 0으로 변경
    }
}
