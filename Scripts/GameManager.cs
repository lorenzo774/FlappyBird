global using Godot;

namespace FlappyBird;

public class GameManager : Node2D
{
    [Export] private float speed = 1f;

    public float Speed { get => speed; }

    public int Points { get; private set; }
    public Bird Bird { get; private set; }

    public static GameManager Instance
    {
        get
        {
            return istance;
        }
    }

    private static GameManager istance;

    const string birdPath = "Environment/Bird";
    const string groundMoverPath = "Environment/Ground/GroundMover";
    const string levelBuilderPath = "Environment/LevelBuilder";


    public void AddPoint() => Points += 1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        OS.WindowMaximized = false;
        istance = this;

        Points = 0;
        Bird = GetNode<Bird>(birdPath);
        GetNode<GroundMover>(groundMoverPath).Speed *= speed;
        GetNode<GroundMover>(groundMoverPath).Speed *= speed;
        GetNode<LevelBuilder>(levelBuilderPath).ObstaclesDelay /= speed;
    }

    public void ResetGame() => GetTree().ReloadCurrentScene();
}
