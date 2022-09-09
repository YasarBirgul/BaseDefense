namespace Abstract
{
    public interface IState
    {
        void UpdateIState();

        void OnEnter();

        void OnExit();
    }
}