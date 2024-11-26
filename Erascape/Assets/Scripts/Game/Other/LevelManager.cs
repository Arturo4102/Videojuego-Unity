namespace Game.Other
{
    public class LevelManager
    {
        public void NextLevel()
        {
            // Trova l'indice della scena corrente
            int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            // Carica la scena successiva
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}