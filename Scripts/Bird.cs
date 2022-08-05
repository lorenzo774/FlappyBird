namespace FlappyBird
{
    public class Bird : RigidBody2D
    {
        public float Height { get => sprite.Texture.GetHeight() * sprite.Scale.y; }

        [Export]
        private float jump = 10f;

        private Vector2 jumpForce;
        private Sprite sprite;
        private GTimer jumpTimer;

        public override void _Ready()
        {
            sprite = GetNode<Sprite>("Sprite");
            jumpForce = new Vector2(0, -jump);
            jumpTimer = new GTimer(this, 250);
        }

        public override void _Process(float delta)
        {
            if (Input.IsActionJustPressed("player_jump"))
            {
                if (jumpTimer.IsActive())
                    return;
                
                jumpTimer.Start();
                ApplyCentralImpulse(jumpForce);
            }
        }

        public void OnCollision(Node node)
        {
            var parent = node.GetParent();
            if (parent.IsInGroup("Obstacle"))
                GameManager.Instance.ResetGame();
        }

        public void OnAreaEntered(Area2D area)
        {
            var parent = area.GetParent();
            if (parent.IsInGroup("Point"))
                GameManager.Instance.AddPoint();
        }

    }
}
