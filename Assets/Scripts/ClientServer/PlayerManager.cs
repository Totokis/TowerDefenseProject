using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] TextMesh text;
    [SerializeField] int id;
    [SerializeField] string username;
    [SerializeField] float health;
    [SerializeField] float maxHealth;
    [SerializeField] MeshRenderer model;

    public float Health
    {
        set
        {
            health = value;

            if (health <= 0f)
            {
                Die();
            }
        }
    }
    private void  Die()
    {
        model.enabled = false;
    }
    public void Respawn()
    {
        model.enabled = true;
        Health = maxHealth;
    }
    void Start()
    {
        text.text = username;
    }

    public void Initialize(int id, string username)
    {
        this.id = id;
        this.username = username;
        this.health = maxHealth;
    }
}
