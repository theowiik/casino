using Godot;

namespace casino.Scripts.Roulette
{
    public sealed class RouletteWheel : Spatial
    {
        private RigidBody _wheel;
        private Button _button;
        private const float MaxSpinSpeed = 10;
        private bool _spinning;

        public override void _Ready()
        {
            _wheel = GetNode<RigidBody>("Wheel");
            _button = GetNode<Button>("Button");
            _button.Connect(nameof(Button.ButtonPressed), this, nameof(OnButtonPressed));
            // BuildCollisionShape();
        }

        private void BuildCollisionShape()
        {
            var csg = GetNode<CSGCombiner>("Wheel/CSGCombiner");
            var collision = GetNode<CollisionShape>("Wheel/CollisionShape");
            var x = csg.GetMeshes();
            var y = x[1] as Mesh;

            collision.Shape = y.CreateTrimeshShape();
        }

        private void OnButtonPressed() => ToggleSpin();

        private void ToggleSpin()
        {
            _spinning = !_spinning;
        }

        public override void _PhysicsProcess(float delta)
        {
            if (Input.IsActionJustPressed("ui_left"))
            {
                BuildCollisionShape();
            }

            if (_spinning)
                _wheel.RotateY(MaxSpinSpeed * 10);
        }
    }
}
