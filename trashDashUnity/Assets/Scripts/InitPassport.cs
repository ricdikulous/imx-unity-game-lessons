using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Immutable.Passport;

public class InitPassport : MonoBehaviour
{
    private Passport passport;

    // Start is called before the first frame update
    async void Start()
    {
        string clientId = "bKf3F5v0G9EF6WfYP5y06NBbTd1YC19x";
        string environment = Immutable.Passport.Model.Environment.SANDBOX;
        passport = await Passport.Init(clientId, environment);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
