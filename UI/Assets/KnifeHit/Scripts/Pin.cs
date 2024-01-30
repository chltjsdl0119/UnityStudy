using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pin : MonoBehaviour
{
    private Movement movement;

    [SerializeField] private Transform hitEffSpawnPoint;
    [SerializeField] private GameObject hitEffPrefab;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            movement.MoveTo(Vector3.zero);

            transform.SetParent(other.transform);
            
            
            other.GetComponent<KnifeTarget>().Hit();

            Instantiate(hitEffPrefab, hitEffSpawnPoint.position, hitEffSpawnPoint.rotation);

            Camera.main.GetComponent<ShakeCamera>().Shake(1, 1);
            
            Destroy(this);
            
            GameManager.Instance.AddScore(1);

        }
        else if (other.gameObject.CompareTag("Pin"))
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
