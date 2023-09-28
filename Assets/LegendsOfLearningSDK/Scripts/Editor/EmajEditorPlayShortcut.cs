using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
namespace LoL.Editor
{
    public class EmajEditorPlayShortcut
    {
        [MenuItem("Emaj/PlayFrom Init  #&%p")]
        public static void PlayFromSplashScreen()
        {
            EditorSceneManager.OpenScene(EditorBuildSettings.scenes.First(a => a.path.Contains("init")).path);
            EditorApplication.EnterPlaymode();   
        }
    }
}