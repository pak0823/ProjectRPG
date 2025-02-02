using UnityEngine;
using UnityEngine.EventSystems;

public class TopDownCamera : MonoBehaviour
{
    public Transform player; // 추적할 캐릭터
    public float distanceFromPlayer = 5f; // 캐릭터와 카메라 간 거리
    public float height = 7f; // 카메라의 높이
    public float followSpeed = 10f; // 카메라 이동 속도
    public float rotationSpeed = 270f; // 카메라 회전 속도
    private float currentRotationY = 0f; // 현재 Y축 회전 각도

    private void Start()
    {
        UpdateCameraPosition();
    }

    private void LateUpdate()
    {
        //마우스 오른쪽 클릭 시 카메라 회전
        //RotateWithMouse();

        // 카메라 위치 업데이트
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        // 회전 각도에 따라 카메라 위치 계산
        Quaternion rotation = Quaternion.Euler(0, currentRotationY, 0);
        Vector3 positionOffset = rotation * new Vector3(0, height, -distanceFromPlayer);
        transform.position = player.position + positionOffset;

        // 카메라가 항상 캐릭터를 바라보도록 설정
        transform.LookAt(player);
    }

    private void RotateWithMouse()
    {
        if (Input.GetMouseButton(1))
        {
            // 마우스 이동 입력 감지
            float mouseX = Input.GetAxis("Mouse X");

            // 회전 각도 계산
            currentRotationY += mouseX * rotationSpeed * Time.deltaTime;


            // 현재 회전 상태에 회전 추가
            Quaternion deltaRotation = Quaternion.Euler(0, mouseX * rotationSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.rotation * deltaRotation, rotationSpeed * Time.deltaTime);
        }
    }
}