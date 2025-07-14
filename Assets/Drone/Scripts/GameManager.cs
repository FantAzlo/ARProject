namespace Drone
{
    public class GameManager
    {
        private static GameManager _instance;
        public static GameManager Instance 
        { 
            get 
            {
                if (_instance == null) return _instance = new GameManager();
                else return _instance;
            } 
        }

        public float Score;
    }
}