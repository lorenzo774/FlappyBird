using Godot;
using System.Collections.Generic;

namespace FlappyBird
{
    public class Pool : Node2D
    {
        [Export]
        private int maxObjects = 10;
        [Export]
        private PackedScene scene;

        private List<Node2D> objects = new List<Node2D>();

        public override void _Ready()
        {
            // Create obstacles
            for (int i = 0; i < maxObjects; i++)
            {
                var obstacle = (Node2D)scene.Instance();
                obstacle.Visible = false;
                obstacle.SetProcess(false);
                objects.Add(obstacle);
                AddChild(obstacle);
            }
        }

        public Node2D Spawn(Vector2 position, Vector2 scale)
        {
            foreach (Obstacle objectPool in objects)
            {
                if (!objectPool.Visible)
                {
                    // var obstacle = obstacles.Dequeue();
                    objectPool.Visible = true;
                    objectPool.Position = position;
                    objectPool.Scale = scale;
                    objectPool.SetProcess(true);
                    objectPool.Speed = GameManager.Instance.Speed;
                    return objectPool;
                }
            }
            return null;
        }
    }
}
