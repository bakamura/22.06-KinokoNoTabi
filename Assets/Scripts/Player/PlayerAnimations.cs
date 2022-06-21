using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    private AnimationHandler _handler;

    [Header("AnimationNames")]

    const string PlayerIdle = "PlayerIdle";

    const string PlayerMove = "PlayerMove";
    const string PlayerBreak = "PlayerBreak";

    const string PlayerDoubleJump = "PlayerDoubleJump";
    const string PlayerFall = "PlayerFall";

    const string PlayerShotAttack = "PlayerShotAttack";

    private void Awake() {
        _handler = GetComponent<AnimationHandler>();
    }

    private void Update() {
        if (_handler.GetCurrentAnimationName() != PlayerShotAttack) {
            if (PlayerData.rbPlayer.velocity.x != 0 && PlayerMovement.Instance.canMove && PlayerInputs.canInput) PlayerData.srPlayer.flipX = PlayerData.rbPlayer.velocity.x < 0;
            PlayerData.srPlayer.flipY = PlayerData.rbPlayer.velocity.y >= -0.1f && _handler.GetCurrentAnimationName() == PlayerDoubleJump;

            // Movement
            if (PlayerMovement.Instance.isGrounded && PlayerMovement.Instance.canMove) {
                if (PlayerData.rbPlayer.velocity.x == 0 && PlayerInputs.horizontalAxis == 0) _handler.ChangeAnimation(PlayerIdle);
                else if (Mathf.Sign(PlayerData.rbPlayer.velocity.x) == PlayerInputs.horizontalAxis) _handler.ChangeAnimation(PlayerMove);
                else _handler.ChangeAnimation(PlayerBreak);
            }
            else if (PlayerData.rbPlayer.velocity.y <= -0.1f) _handler.ChangeAnimation(PlayerFall); //

        }
    }

    public void FromAnyTo(string animationName) {
        _handler.ChangeAnimation(animationName);
    }

}
