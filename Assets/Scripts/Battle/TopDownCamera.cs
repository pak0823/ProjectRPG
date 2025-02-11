using UnityEngine;
using UnityEngine.EventSystems;


public class TopDownCamera : MonoBehaviour
{
    public Transform player; // 추적할 캐릭터
    private float distanceFromPlayer = 5f; // 캐릭터와 카메라 간 거리
    private float height = 3f; // 카메라의 높이
    private float followSpeed = 5f; // 카메라 이동 속도
    private float rotationSpeed = 120f; // 카메라 회전 속도
    private float currentRotationY = 0f; // 현재 Y축 회전 각도
    private float currentRotationX = 0f; // 현재 X축 회전 각도

    private void Start()
    {
        UpdateCameraPosition();
    }

    private void LateUpdate()
    {
        //마우스 오른쪽 클릭 시 카메라 회전
        RotateWithMouse();

        // 카메라 위치 업데이트
        UpdateCameraPosition();

        // 마우스 휠로 카메라 거리 조정
        AdjustCameraDistance();
    }

    private void UpdateCameraPosition()
    {
        // 회전 각도에 따라 카메라 위치 계산
        Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);
        Vector3 positionOffset = rotation * new Vector3(0, height, -distanceFromPlayer);
        transform.position = player.position + positionOffset;

        // 카메라가 플레이어를 바라보도록 설정
        transform.LookAt(player.position);
    }

    private void RotateWithMouse()
    {
        // 마우스 이동 입력 감지
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 회전 각도 계산
        currentRotationY += mouseX * rotationSpeed * Time.deltaTime;
        currentRotationX -= mouseY * rotationSpeed * Time.deltaTime;

        currentRotationX = Mathf.Clamp(currentRotationX, -30f, 25f);

        if (currentRotationY >= 360f || currentRotationY <= -360f)
            currentRotationY = 0f;
    }

    private void AdjustCameraDistance()
    {
        // 마우스 휠 입력 감지
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            // 스크롤에 따라 거리 조정
            distanceFromPlayer -= scrollInput * 2f; // 원하는 속도로 조정 (2f는 조정 비율)
            distanceFromPlayer = Mathf.Clamp(distanceFromPlayer, 2f, 5f); // 최소 및 최대 거리 제한
        }
    }
}