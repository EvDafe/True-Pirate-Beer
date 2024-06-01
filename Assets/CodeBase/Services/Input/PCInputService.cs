using UnityEngine;
namespace CodeBase.Services.Input
{
    public class PCInputService : IInputService
    {
        private const string Horizontal = nameof(Horizontal);
        private const string Vertical = nameof(Vertical);

        public Vector2 MoveAxis => 
            new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));

        public bool IsAttack() =>
            UnityEngine.Input.GetMouseButtonDown(0);
    }
}
