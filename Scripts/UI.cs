using Godot;

public class UI : CanvasLayer
{
    private Label pointsLbl;
    private Label fps;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        pointsLbl = GetNode<Label>("PointsLbl");
        fps = GetNode<Label>("FPS");
    }

    public override void _Process(float delta)
    {
        pointsLbl.Text = GameManager.Instance.Points.ToString();        
        fps.Text = ((int)(1 / delta)).ToString();
    }
}
