using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpF = 1.5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Transform cam; //오브젝트의 위치, 방향, 크기 옵션에 접근(Transform)

    private Vector3 velocity;   //현재 속도를 의미하는 변수
    private bool isGrounded;



    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        cam = Camera.main.transform; //메인카메라의 transform 정보를 가져와 cam 변수로 세팅하겠다.
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = controller.isGrounded; //charactor controller에 isgrounded가 있기 때문에 이걸 가져옴.(Charactor Controller를 사용할때만)

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10f;
        }
        else
        {
            speed = 5f;
        }

        Vector3 forward = Vector3.forward; //앞을 기준으로 할 것이다.
        Vector3 right = Vector3.right;  //양의 x축 방향을 기준으로 할 것이다.

        if (cam != null)
        {
            Vector3 camForward = cam.forward; //카메라의 앞쪽 방향 설정
            camForward.y = 0f; //수평 성분만 추출
            camForward.Normalize(); // 벡터를 정규화 → 모든 벡터의 크기를 1로 (방향 정보만 추출)

            Vector3 camRight = cam.right;
            camRight.y = 0f;
            camRight.Normalize();

            forward = camForward;
            right = camRight;
        }

        Vector3 moveDir = (right * h + forward * v); //최종 이동방향 계산 
        moveDir.Normalize(); //대각선은 속도가 빨라지기 때문에 보정(정규화) 필요

        controller.Move(moveDir * speed * Time.deltaTime); //Charactor controller는 Move 함수를 제공해줌. 여기에 방향과 속도를 조정.
                                                           //Time.deltaTime → 이전 프레임에서 현재 프레임 까지 걸린 시간. 이를 이용해 사양 차이로 인한 속도 차이를 예방
        jump();

        if (moveDir.sqrMagnitude > 0.001f)
        { 
            transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up); //보는 방향으로 회전
        }

    }

    void jump()
    {
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space) == true)
        {
            velocity.y = Mathf.Sqrt(jumpF * -2f * gravity); //점프 속도 계산. Mathf.sqrt() → 제곱근을 구해주는 함수 
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
}
