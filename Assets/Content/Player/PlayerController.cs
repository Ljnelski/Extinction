using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _playerPivot;
    [SerializeField] private float _rotationSpeed = 0.1f;

    [Header("Camera")]
    [SerializeField] private Transform _cameraLookTarget;
    [SerializeField] private Transform _cameraLookPivot;
    [SerializeField] private float _cameraRotationSpeed;

    private PlayerInput _input;
    // Start is called before the first frame update
    private void OnEnable()
    {
        _input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {      
        // Rotate The Camera Look Target as the player moves there mouse
        _cameraLookPivot.Rotate(new Vector3(0f, _input.LookDirection.x *_cameraRotationSpeed, 0f));

        // Rotate the player body to the direction the camera target is facing

        Vector3 lookDirection = Vector3.Slerp(_playerPivot.forward, _cameraLookPivot.forward, _rotationSpeed);

        Quaternion quaternion = Quaternion.LookRotation(lookDirection);

        _playerPivot.rotation = quaternion;

        // Resolve Attack
        if (_input.PrimaryAttack && _input.SecondaryAttack)
        {
            DoBiteAttack();
        }
        else if (_input.PrimaryAttack)
        {
            DoLeftSwipe();
        }
        else if (_input.SecondaryAttack)
        {
            DoRightSwipe();
        }
        else if (_input.WingAttack)
        {
            DoWingFlap();
        }
        else if (_input.BreathAttack)
        {
            DoBreathAttack();
        }
        else if (_input.Roar)
        {
            DoTheRoar();
        }
    }

    private void DoLeftSwipe()
    {
        Debug.Log("Left Swipe");
    }

    private void DoRightSwipe()
    {
        Debug.Log("Right Swipe");
    }

    private void DoBiteAttack()
    {
        Debug.Log("Bite Attack");
    }

    private void DoWingFlap()
    {
        Debug.Log("Flap");
    }

    private void DoBreathAttack()
    {
        Debug.Log("Breath Attack");
    }

    private void DoTheRoar()
    {
        Debug.Log("RRRAAAAAAAWWWWWWWRRRRR!!!!");
    }
}
