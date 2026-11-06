using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FrostRunPlayer : MonoBehaviour
{
    public float laneChangeSpeed = 13f;
    public float jumpForce = 8.5f;
    public float gravity = -24f;
    public float slideDuration = 0.8f;

    private CharacterController controller;
    private int lane = 1;
    private float verticalVelocity;
    private float slideTimer;
    private const float NormalHeight = 2f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        controller.height = NormalHeight;
        controller.center = Vector3.zero;
    }

    private void Update()
    {
        FrostRunGame game = FrostRunGame.Instance;
        if (game == null || !game.IsPlaying || game.IsPaused || game.IsGameOver) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            lane = Mathf.Max(0, lane - 1);
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            lane = Mathf.Min(2, lane + 1);

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) ||
             Input.GetKeyDown(KeyCode.Space)) && controller.isGrounded)
            verticalVelocity = jumpForce;

        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) &&
            controller.isGrounded && slideTimer <= 0f)
        {
            slideTimer = slideDuration;
            controller.height = 1.05f;
            controller.center = new Vector3(0f, -0.475f, 0f);
        }

        if (slideTimer > 0f)
        {
            slideTimer -= Time.deltaTime;
            if (slideTimer <= 0f)
            {
                controller.height = NormalHeight;
                controller.center = Vector3.zero;
            }
        }

        float targetX = (lane - 1) * game.laneWidth;
        float newX = Mathf.MoveTowards(transform.position.x, targetX, laneChangeSpeed * Time.deltaTime);
        if (controller.isGrounded && verticalVelocity < 0f) verticalVelocity = -2f;
        verticalVelocity += gravity * Time.deltaTime;

        controller.Move(new Vector3(newX - transform.position.x,
            verticalVelocity * Time.deltaTime, game.forwardSpeed * Time.deltaTime));
    }

    public void ResetPlayer()
    {
        if (controller == null) controller = GetComponent<CharacterController>();
        controller.enabled = false;
        transform.position = new Vector3(0f, 1f, 2f);
        transform.rotation = Quaternion.identity;
        lane = 1;
        verticalVelocity = 0f;
        slideTimer = 0f;
        controller.height = NormalHeight;
        controller.center = Vector3.zero;
        controller.enabled = true;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.GetComponent<FrostRunObstacle>() != null)
            FrostRunGame.Instance?.EndRun();
    }

    private void OnTriggerEnter(Collider other)
    {
        FrostRunCoin coin = other.GetComponent<FrostRunCoin>();
        if (coin != null) coin.Collect();
    }
}
