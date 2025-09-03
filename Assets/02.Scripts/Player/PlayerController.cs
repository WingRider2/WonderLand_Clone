using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;

    private Vector2 mouseDelta;

    private Inventory inventory;

    public Animator playerAnimator;

    [SerializeField] private float groundCheckDistance = 0.2f;

    [SerializeField] private float stepInterval = 0.5f; // 발소리 반복 간격 (초)

    private float stepTimer = 0f;                       // 타이머

    private bool canJump = true;

    private bool canMove = true;
    private bool canLook = true;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();

        rigidbody = GetComponent<Rigidbody>();
        inventory = FindObjectOfType<Inventory>();

        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        //Debug.Log(canMove.ToString() + canLook.ToString());
        PlayFootstepSound();
    }

    private void FixedUpdate()
    {
        if(canMove) Move();
    }

    private void LateUpdate()
    {
        if(canLook) CameraLook();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {           
            Player.Instance.CurMovementInput = context.ReadValue<Vector2>();

            playerAnimator.SetBool("IsMove", true);
        }

        else if (context.phase == InputActionPhase.Canceled)
        {
            Player.Instance.CurMovementInput = Vector2.zero;

            playerAnimator.SetBool("IsMove", false);
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && canJump)
        {
            Jump();
        }
    }

    #region 인벤토리 슬롯 사용 입력 메서드 모음
    public void OnUseSlot1(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if(inventory == null)
            {
                Debug.LogError("inventory is null!");
            }

            else
            {
                inventory?.SelectSlot(0);
            }
        }
    }

    public void OnUseSlot2(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            inventory?.SelectSlot(1);
        }
    }

    public void OnUseSlot3(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            inventory?.SelectSlot(2);
        }
    }
    #endregion

    private void Move()
    {
        Vector3 dir = transform.forward * Player.Instance.CurMovementInput.y + transform.right * Player.Instance.CurMovementInput.x;
        Vector3 velocity = dir * Player.Instance.speed;
        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;
    }

    private void CameraLook()
    {
        Player.Instance.CamCurXRot += mouseDelta.y * Player.Instance.lookSensitivity;
        Player.Instance.CamCurXRot = Mathf.Clamp(Player.Instance.CamCurXRot, Player.Instance.minXLook, Player.Instance.maxXLook);

        Player.Instance.cameraContainer.localEulerAngles = new Vector3(-Player.Instance.CamCurXRot, 0f, 0f);

        transform.Rotate(Vector3.up * mouseDelta.x * Player.Instance.lookSensitivity);
    }

    private void Jump()
    {
        // 점프 실행
        rigidbody.AddForce(Vector3.up * Player.Instance.jumpForce, ForceMode.Impulse);

        // 점프 사운드 재생 (추가)
        SoundManager.Instance.PlaySFX(SoundType.jumpSound);

        // 점프 연타 방지
        canJump = false;
        StartCoroutine(EnableJumpAfterDelay());
    }

    private IEnumerator EnableJumpAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);

        while (!IsGrounded())
        {
            yield return null;
        }

        // 착지했을 때 Drop Sound 재생
        SoundManager.Instance.PlaySFX(SoundType.landSound);

        canJump = true;
    }


    private bool IsGrounded()
    {
        //  LayerMask 없이 Raycast 구현
        Vector3 rayOrigin = Player.Instance.transform.position;
        float rayLength = 1.0f;

        return Physics.Raycast(rayOrigin, Vector3.down, rayLength);
    }
    private void PlayFootstepSound()
    {
        // 걷는 중(IsMove==true) && 땅에 있음 && 키 입력이 있음
        if (playerAnimator.GetBool("IsMove") && IsGrounded() && Player.Instance.CurMovementInput.magnitude > 0.1f)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                SoundManager.Instance.PlaySFX(SoundType.walkSound);
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = stepInterval; // 멈추면 바로 타이머 리셋
        }
    }

    public void OnCanMoveLook()
    {
        canMove = true;
        canLook = true;
    }
    public void OffCanMoveLook()
    {
        canMove = false;
        canLook = false;
    }
}
