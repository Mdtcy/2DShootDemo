using Sirenix.OdinInspector;

namespace GameMain
{
    public sealed partial class GameEntry : SerializedMonoBehaviour
    {
        private void Start()
        {
            InitFrameworkComponents();
            InitCustomComponents();
        }
    }
}
