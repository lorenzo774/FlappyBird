namespace FlappyBird
{
    public class Obstacle : Sprite
    {
        public float Height { get => RegionRect.Size.y * Scale.y; }
        public float Speed { get; set; }

        [Export]
        private float speed = 120f;

        public float NormalHeight { get => RegionRect.Size.y; }
        public float Boundary { get; set; }

        public override void _Process(float delta)
        {
            Translate(Vector2.Left * delta * speed * Speed);
            if (IsOutsideBoundary())
            {
                Disable();
            }
            if (GameManager.Instance.Bird.Position.x > Position.x && GameManager.Instance.Bird.Position.x < GetLastXPosition()
            && GameManager.Instance.Bird.Position.y < -GameManager.Instance.Bird.Height)
            {
                GameManager.Instance.ResetGame();
            }
        }

        private float GetLastXPosition()
        {
            float positionX = Position.x;
            if (Texture != null)
            {
                positionX += (Texture.GetWidth() * Scale.x);
            }
            return positionX;
        }

        private void Disable()
        {
            Visible = false;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            SetProcess(false);
        }

        private bool IsOutsideBoundary()
        {
            return GetLastXPosition() < Boundary;
        }
    }
}
