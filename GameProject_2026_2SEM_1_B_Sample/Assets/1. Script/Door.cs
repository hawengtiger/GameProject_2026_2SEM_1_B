using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 3f;
    [SerializeField] private Quaternion closedRotation;
    [SerializeField] private Quaternion openRoatation;

    private bool isOpen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closedRotation = transform.localRotation;
        openRoatation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion targetRoatation;

        if (isOpen)
        {
            targetRoatation = openRoatation;
        }
        else
        {
            targetRoatation = closedRotation;
        }

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRoatation, openSpeed * Time.deltaTime);
    }

    public void Interact()          // 인터렉션 함수가 호출 되었을 때
    {
        isOpen = !isOpen;           // bool 값을 뒤집는다.
    }

    public string GetGuideText()    // 가이드 텍스트를 호출 할 때 상황에 따라 텍스트를 반환한다.
    {
        if (isOpen)
        {
            return "[E] 문 닫기";
        }
        return "[E] 문 열기";
    }

}
