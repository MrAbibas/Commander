namespace App.Core.FSM
{
    public interface IState
    {
        void Enter();
        void Update();
        void Exit();
    }
}