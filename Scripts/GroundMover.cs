using Godot.Collections;

namespace FlappyBird
{
    public class GroundMover : Node2D
    {
        [Export]
        private float speed = 100f;

        public float Speed { get => speed; set => speed = value; }

        private Array sprites;
        private float spriteWidth;


        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            sprites = GetChildren();
            var lastSprite = GetFartherSprite();
            spriteWidth = lastSprite.Texture.GetWidth() * lastSprite.Scale.x;
        }

        private Sprite GetFartherSprite()
        {
            float maxXPos = -100;
            Sprite result = null;
            
            foreach (Sprite sprite in sprites)
            {
                if (sprite.Position.x > maxXPos)
                {
                    maxXPos = sprite.Position.x;
                    result = sprite;
                }
            }

            return result;
        }

        public override void _Process(float delta)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.Translate(Vector2.Left * speed * delta);

                if (sprite.Position.x < -spriteWidth - 300)
                {
                    var lastSprite = GetFartherSprite();
                    if (lastSprite == null) continue;
                    sprite.Position = new Vector2
                    (
                        lastSprite.Position.x + spriteWidth - (speed * delta),
                        sprite.Position.y
                    );
                }
            }
        }
    }
}
