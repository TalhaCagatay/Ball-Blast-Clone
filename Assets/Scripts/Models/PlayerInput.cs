using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3 Input
    {
        get
        {
            var mousePos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            var direction = (mousePos - transform.position).normalized;

            return direction;
        }

        private set { }
    }
}