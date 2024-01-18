using System;
using UnityEngine;
using TMPro;
using Immutable.Passport;

// Prefill the info on the player data, as they will be used to populate the leadboard.
public class Profile : MonoBehaviour
{
    public TMP_Text emailText;
	public TMP_Text addressText;
    private Passport passport;

	public void Open()
	{
		gameObject.SetActive(true);
		if (Passport.Instance != null) {
            passport = Passport.Instance;
            Populate();
        } else {
            Debug.Log("Passport Instance is null");
            emailText.text = "Could not connect to Passport";
        }
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}

	public void Populate()
	{
		PopulateEmail();
		PopulateAddress();
	}

	public async void PopulateEmail()
    {
		try {
			string? email = await passport.GetEmail();
			emailText.text = email;
		} catch (Exception e){
			emailText.text = "Unable to get the email";
            Debug.Log($"Unable to get email: {e.Message}");
        }
    }

	public async void PopulateAddress()
    {
		try {
			string? address = await passport.GetAddress(); 
			addressText.text = address;
		} catch (Exception e){
			addressText.text = "Unable to get the address";
            Debug.Log($"Unable to get address: {e.Message}");
        }
    }

	public async void Logout() {
		LoadoutState loadoutState = GameManager.instance.topState as LoadoutState;
		await loadoutState.Logout();
		Close();
	}
}
