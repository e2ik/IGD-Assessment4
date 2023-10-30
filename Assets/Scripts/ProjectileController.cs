using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    [SerializeField] private string firedDirection;
    private PacStudentController ps;
    private Tweener tweener;
    private Vector3 startPosition;
    private Vector3 endPosition;

    void Start() {
        GameObject zombie = GameObject.Find("Zombie");
        if (zombie != null) {
            ps = zombie.GetComponent<PacStudentController>();
        }

        if (ps != null) {
            firedDirection = ps.lastInput;
        }
        tweener = GetComponent<Tweener>();
        startPosition = transform.position;
    }

    void Update() {
        switch (firedDirection) {
            case "Right": MoveRight(); break;
            case "Left": MoveLeft(); break;
            case "Up": MoveUp(); break;
            case "Down": MoveDown(); break;
        }
    }

    void MoveRight() {
        endPosition = startPosition;
        endPosition += Vector3.right * 1.0f;
        StartCoroutine(nameof(Move));
    }

    void MoveLeft() {
        endPosition = startPosition;
        endPosition += Vector3.left * 1.0f;
        StartCoroutine(nameof(Move));
    }

    void MoveUp() {
        endPosition = startPosition;
        endPosition += Vector3.up * 1.0f;
        StartCoroutine(nameof(Move));
    }

    void MoveDown() {
        endPosition = startPosition;
        endPosition += Vector3.down * 1.0f;
        StartCoroutine(nameof(Move));
    }

    IEnumerator Move() {
        float distanceToMove = Vector3.Distance(transform.position, endPosition);
        float animDuration = distanceToMove / 10f;
        if (tweener != null) {
            tweener.AddTween(transform, transform.position, endPosition, animDuration);
        }
        startPosition = endPosition;
        yield return new WaitForSeconds(animDuration);
    }

    void OnCollisionEnter2D(Collision2D other) {
        string otherName = other.gameObject.name.ToLower();
        if (otherName.Contains("wall")) {
            Destroy(gameObject);
        } else if (otherName.Contains("hunter")) {
            Destroy(gameObject);
        }
    }

}
