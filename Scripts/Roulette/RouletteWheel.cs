using Godot;

namespace casino.Scripts.Roulette
{
    public sealed class RouletteWheel : Spatial
    {
        private RigidBody _wheel;
        private Button _button;
        private const float MaxSpinSpeed = 10;
        private bool _spinning;

        public async override void _Ready()
        {
            _wheel = GetNode<RigidBody>("Wheel");
            _button = GetNode<Button>("Button");
            _button.Connect(nameof(Button.ButtonPressed), this, nameof(OnButtonPressed));

            await ToSignal(GetTree(), "idle_frame");
            BuildCollisionShape();
        }

        private void BuildCollisionShape()
        {
            var combiner = GetNode<CSGCombiner>("Wheel/CSGCombiner");
            var collision = GetNode<CollisionShape>("Wheel/CollisionShape");
            var meshes = combiner.GetMeshes();

            if (meshes.Count == 2 && meshes[1] is Mesh mesh)
                collision.Shape = mesh.CreateTrimeshShape();
            else
                throw new System.Exception("Roulette wheel CSG mesh has not been built");
        }

        private void OnButtonPressed() => ToggleSpin();

        private void ToggleSpin()
        {
            _spinning = !_spinning;
        }

        public override void _PhysicsProcess(float delta)
        {
            if (Input.IsActionJustPressed("ui_left")) BuildCollisionShape();

            if (_spinning)
                _wheel.RotateY(MaxSpinSpeed * 10);
        }
    }
}
