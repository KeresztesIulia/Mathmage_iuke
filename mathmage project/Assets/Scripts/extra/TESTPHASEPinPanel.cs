using System;
using TMPro;
using UnityEngine;

public class TESTPHASEPinPanel : Closeable
{
    [SerializeField] TextMeshProUGUI code;

    private void Start()
    {
        using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
        {
            byte[] usernameBytes = System.Text.Encoding.UTF8.GetBytes(GameFunctions.usernameString);
            byte[] hashBytes = sha256.ComputeHash(usernameBytes);

            //code.text = Encoding.UTF8.GetString(hashBytes); // gives some weird stuff - no font compatibility

            code.text = BitConverter.ToString(hashBytes).Replace("-", "");
        }
    }
}
