/*
 * CreditsView:
 * UI view of the Credits screen.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsView : MonoBehaviour
{
    public void Action_CloseCredits()
    {
        SceneManager.UnloadSceneAsync("s_credits");
    }
}
