using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SimpleSlidePanelController : MonoBehaviour
{
    public RectTransform panelContainer;   // 여러 패널을 포함하는 컨테이너 (패널들이 가로로 나열됨)
    public Button nextButton;              // 다음 패널로 이동하는 버튼
    public Button prevButton;              // 이전 패널로 이동하는 버튼
    [SerializeField] private float panelWidth;        // 각 패널의 가로 크기 (슬라이드할 때 이동할 거리)
    [SerializeField] private float panelSpacing;     // 패널 간의 간격 (슬라이드할 때 이동할 거리) // 간격을 넓힘
    [SerializeField] private int totalPanels;           // 총 패널 수 (여기서는 5개의 패널을 예로 듦)
    private int currentPanelIndex = 0;     // 현재 보이는 패널의 인덱스
    private bool isSliding = false;        // 슬라이드 중인지 확인하는 변수

    void Start()
    {
        nextButton.onClick.AddListener(OnNextPanelClicked);  // 다음 버튼 클릭 시
        prevButton.onClick.AddListener(OnPrevPanelClicked);  // 이전 버튼 클릭 시

        // 패널들을 가로로 배치
        PositionPanels();

        // 첫 번째 패널을 화면에 보여줍니다.
        panelContainer.localPosition = Vector3.zero;
    }

    // 패널들을 가로로 배열하는 함수
    void PositionPanels()
    {
        // panelContainer의 크기를 각 패널의 크기와 간격을 고려하여 설정
        float totalWidth = (panelWidth + panelSpacing) * totalPanels;  // 전체 컨테이너의 크기 계산
        panelContainer.sizeDelta = new Vector2(totalWidth, panelContainer.sizeDelta.y);  // panelContainer 크기 설정

        // 각 패널을 수동으로 가로로 배치
        for (int i = 0; i < totalPanels; i++)
        {
            RectTransform panel = panelContainer.GetChild(i) as RectTransform;
            // 패널을 X축으로 이동시키고 간격(panelSpacing)을 더해줌
            panel.localPosition = new Vector3(i * (panelWidth + panelSpacing), 0, 0); // 간격을 추가하여 배치
        }
    }

    // '다음' 버튼 클릭 시
    void OnNextPanelClicked()
    {
        // 슬라이딩 중일 경우 버튼이 작동하지 않게
        if (isSliding) return;

        if (currentPanelIndex < totalPanels - 1)
        {
            currentPanelIndex++;
            MovePanel();
        }
    }

    // '이전' 버튼 클릭 시
    void OnPrevPanelClicked()
    {
        // 슬라이딩 중일 경우 버튼이 작동하지 않게
        if (isSliding) return;

        if (currentPanelIndex > 0)
        {
            currentPanelIndex--;
            MovePanel();
        }
    }

    // 패널을 슬라이드하여 이동하는 함수
    void MovePanel()
    {
        isSliding = true; // 슬라이드 시작

        // 패널을 슬라이드로 이동 (패널 간 이동)
        panelContainer.DOLocalMoveX(-(currentPanelIndex * (panelWidth + panelSpacing)), 0.5f).SetEase(Ease.OutExpo)
            .OnComplete(() => {
                isSliding = false; // 슬라이드 완료 후 상태 리셋
            });
    }
}
