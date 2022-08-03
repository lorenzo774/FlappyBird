using Godot;
using System;

namespace FlappyBird
{
    public class LevelBuilder : Node2D
    {
        public float ObstaclesDelay { get => obstaclesDelay; set { obstaclesDelay = value; GetNode<Timer>("Timer").WaitTime = obstaclesDelay; } }

        [Export]
        private float obstaclesDelay = 4f;
        [Export]
        private int minObstacleHeight = 4;
        [Export]
        private int maxObstacleHeight = 9;

        [Export]
        private float spaceForPlayer = 100f;

        private Pool obstaclesPool;
        private Pool pointsPool;
        private Pool obstaclesHeadPool;

        private Vector2 upPosition;
        private float xBoundary;

        private float height;
        private Random rnd;

        public override void _Ready()
        {
            obstaclesPool = GetNode<Pool>("ObstaclesPool");
            pointsPool = GetNode<Pool>("PointsPool");
            obstaclesHeadPool = GetNode<Pool>("ObstaclesHeadPool");
            upPosition = GetNode<Node2D>("Up").Position;
            height = GetNode<Node2D>("Height").Position.y;
            xBoundary = GetNode<Node2D>("Boundary").Position.x;
            rnd = new Random();
        }

        // Spawn 2 obstacles every obstaclesDelay
        public void OnBuildTime()
        {
            // Build up
            var upObstacle = obstaclesPool.Spawn(upPosition, GetRandomScale()) as Obstacle;
            upObstacle.Boundary = xBoundary;
            var obstacleHead = obstaclesHeadPool.Spawn(new Vector2(upPosition.x, upObstacle.Height), Vector2.One * 2) as Obstacle;
            float obstacleNormalHeight = upObstacle.NormalHeight;
            // Build points    
            pointsPool.Spawn(
                new Vector2(upPosition.x, upObstacle.Height),
                new Vector2(2, spaceForPlayer / obstacleNormalHeight)
            );
            // Build down obstacle
            float yUsed = upObstacle.Height + spaceForPlayer;
            obstaclesHeadPool.Spawn(new Vector2(upPosition.x, yUsed - obstacleHead.Texture.GetHeight() * obstacleHead.Scale.y), Vector2.One * 2);
            var downStartPos = new Vector2(upPosition.x, yUsed);
            var downObstacle = obstaclesPool.Spawn(downStartPos, GetScaleLeft(obstacleNormalHeight, yUsed)) as Obstacle;
            downObstacle.Boundary = xBoundary;
        }

        private Vector2 GetRandomScale()
        {
            float randomY = rnd.Next(minObstacleHeight, maxObstacleHeight);
            return new Vector2(2, randomY);
        }

        private Vector2 GetScaleLeft(float obstacleNormalHeight, float yUsed)
        {
            float yLeft = height - yUsed;
            float scaleLeft = yLeft / obstacleNormalHeight;
            return new Vector2(2, scaleLeft);
        }
    }
}
