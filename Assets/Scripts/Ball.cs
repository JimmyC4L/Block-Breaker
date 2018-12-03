using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class Ball : MonoBehaviour
{
    // configuration parameters
    [FormerlySerializedAs("paddle1")] [SerializeField]
    private Paddle _paddle1;

    [FormerlySerializedAs("xPush")] [SerializeField]
    private float _xPush = 2f;

    [FormerlySerializedAs("yPush")] [SerializeField]
    private float _yPush = 20f;

    [FormerlySerializedAs("ballCollisionSounds")] [SerializeField]
    private AudioClip[] _ballCollisionSounds;

    [FormerlySerializedAs("randomFactor")] [SerializeField]
    private float _randomFactor = 0.2f;

    // state
    private Vector2 _paddleToBallDistance;

    [FormerlySerializedAs("ballFired")] [SerializeField]
    private bool _ballFired = false;

    // Cached component references
    private AudioSource _myAudioSource;
    private Rigidbody2D _myRigidbody2D;

    // Use this for initialization
    private void Start()
    {
        _paddleToBallDistance = transform.position - _paddle1.transform.position;
        _myAudioSource = GetComponent<AudioSource>();
        _myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_ballFired) return;
        LockBallToPaddle();
        LaunchOnMouseClick();
    }

    private void LaunchOnMouseClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        _myRigidbody2D.velocity = new Vector2(_xPush, _yPush);
        _ballFired = true;
    }

    private void LockBallToPaddle()
    {
        var paddlePosition = new Vector2(_paddle1.transform.position.x, _paddle1.transform.position.y);
        transform.position = paddlePosition + _paddleToBallDistance;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var velocityTweak = new Vector2(UnityEngine.Random.Range(0, _randomFactor), UnityEngine.Random.Range(0, _randomFactor));
        if (!_ballFired) return;
        var clip = _ballCollisionSounds[UnityEngine.Random.Range(0, _ballCollisionSounds.Length)];
        _myAudioSource.PlayOneShot(clip);
        _myRigidbody2D.velocity += velocityTweak;
    }
}