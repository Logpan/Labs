using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class SC_LoginSystem : MonoBehaviour
{
    public enum CurrentWindow { Login, Register }
    public CurrentWindow currentWindow = CurrentWindow.Login;


    public GameObject panelRegister;
    public GameObject panelLogin;

    public TMP_InputField email;
    public TMP_InputField password;

    public TMP_InputField emailRegister;
    public TMP_InputField username;
    public TMP_InputField password1;
    public TMP_InputField password2;

    public TextMeshProUGUI loginError;
    public TextMeshProUGUI registerError;


    string loginEmail = "";
    string loginPassword = "";
    string registerEmail = "";
    string registerPassword1 = "";
    string registerPassword2 = "";
    string registerUsername = "";
    string errorMessage = "";

    bool isWorking = false;
    bool registrationCompleted = false;
    bool isLoggedIn = false;

    //Logged-in user data
    string userName = "";
    string userEmail = "";

    string rootURL = "http://localhost/School/"; //Path where php files are located

    private void Start()
    {
        if (currentWindow == CurrentWindow.Login)
        {
            panelRegister.SetActive(false);
            panelLogin.SetActive(true);

        }
        else
        {
            panelRegister.SetActive(true);
            panelLogin.SetActive(false);
        }
    }
    
    public void Submit()
    {
        if(currentWindow == CurrentWindow.Login)
        {
            StartCoroutine(LoginEnumerator());
        }
        else
        {
            StartCoroutine(RegisterEnumerator());
        }
    }
    
    public void Register()
    {
        currentWindow = CurrentWindow.Register;
        panelRegister.SetActive(true);
        panelLogin.SetActive(false);

    }

    IEnumerator RegisterEnumerator()
    {
        isWorking = true;
        registrationCompleted = false;
        errorMessage = "";
        registerEmail = emailRegister.text;
        registerUsername = username.text;
        registerPassword1 = password1.text;
        registerPassword2 = password2.text;
        WWWForm form = new WWWForm();
        form.AddField("email", registerEmail);
        form.AddField("username", registerUsername);
        form.AddField("password1", registerPassword1);
        form.AddField("password2", registerPassword2);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                errorMessage = www.error;
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    ResetValues();
                    registrationCompleted = true;
                    currentWindow = CurrentWindow.Login;
                    loginError.text = responseText;
                    panelRegister.SetActive(false);
                    panelLogin.SetActive(true);
                }
                else
                {
                    errorMessage = responseText;
                    registerError.text = errorMessage;
                }
            }
        }

        isWorking = false;
    }

    IEnumerator LoginEnumerator()
    {
        isWorking = true;
        registrationCompleted = false;
        errorMessage = "";

        loginEmail = email.text;
        loginPassword = password.text;

        WWWForm form = new WWWForm();
        form.AddField("email", loginEmail);
        form.AddField("password", loginPassword);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                errorMessage = www.error;
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    string[] dataChunks = responseText.Split('|');
                    userName = dataChunks[1];
                    userEmail = dataChunks[2];
                    isLoggedIn = true;
                    loginError.text = "success";
                    ResetValues();
                }
                else
                {
                    errorMessage = responseText;
                    loginError.text = responseText;
                }
            }
        }

        isWorking = false;
    }

    void ResetValues()
    {
        errorMessage = "";
        loginEmail = "";
        loginPassword = "";
        registerEmail = "";
        registerPassword1 = "";
        registerPassword2 = "";
        registerUsername = "";
        
        emailRegister.text = "";
        username.text = "";
        password1.text = "";
        password2.text = "";
        email.text = "";
        password.text = "";
        registerError.text = "";
        loginError.text = "";
    }
}
