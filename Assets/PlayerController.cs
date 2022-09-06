using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;



[SelectionBase]
[ExecuteInEditMode]
public class PlayerController : MonoBehaviour
{
    [SerializeField] InputActionReference _move;
    [SerializeField] float _speed = 6f;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _move.action.started += Move;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        _move.action.Enable();
    }

    void Move(InputAction.CallbackContext ctx)
    {
        Debug.Log("Starting async Func");

        async void MoveCallback()
        {

            do
            {
                Vector3 direction = ctx.action.ReadValue<Vector2>();
                transform.Translate(direction * _speed * Time.fixedDeltaTime);
                await UniTask.WaitForFixedUpdate();
            } while (ctx.action.inProgress);

            Debug.Log("Ended async func");
        }

        MoveCallback();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {

    }


}
