using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Immutable.Passport;

public class PassportService
{
    public static async Task<List<string>> FetchPlayerAccounts()
    {
        Passport passport = Passport.Instance;
        await passport.ConnectEvm();
        List<string> accounts = await passport.ZkEvmRequestAccounts();
        Debug.Log("Players accounts: " + String.Join(", ", accounts)); 
        return accounts;
    }
}