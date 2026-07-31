using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MoveMent
{
    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        HostManager.SetHost(this);
    }
}
