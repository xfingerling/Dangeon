using UnityEngine;

public class Player : Mover
{
    [SerializeField] private float _speedX = 1;
    [SerializeField] private float _speedY = 1;

    private Vector3 _input;

    private void Start()
    {
        //Set start position
        transform.position = Game.GetInteractor<SpawnPositionInteractor>().SpawpPosition.position;
    }

    private void FixedUpdate()
    {
        HundleInput();
        Move(_input);
    }

    private void HundleInput()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        _input = new Vector3(horizontalInput * _speedX, verticalInput * _speedY, 0);
    }
}
