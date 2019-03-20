using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    
    [SerializeField] GameObject goal_;
    private Goal goal_script_;

    private Animator animator_;

    // Use this for initialization
    void Start()
    {
        animator_ = GetComponent<Animator>();
        goal_script_ = goal_.GetComponent<Goal>();
    }

    // Update is called once per frame
    void Update () {
		if (goal_script_.PlayerHasCollidedWithGoal())
        {
            FadeOut();
        }
	}

    private void FadeOut()
    {
        animator_.SetTrigger("FadeOut");
    }

    public void OnFadeOutComplete()
    {
        int next_scene_index = SceneManager.GetActiveScene().buildIndex + 1;

        if (next_scene_index >= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(next_scene_index);
        }
        
    }


}
