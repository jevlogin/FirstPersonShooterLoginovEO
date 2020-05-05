namespace JevLogin
{
    public sealed class PlayerController : BaseController, IExecute
    {
        #region Fields

        private readonly IMotor _motor;

        #endregion


        #region ClassLifeCycles

        public PlayerController(IMotor motor)
        {
            _motor = motor;
        }

        #endregion


        #region Methods

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }
            _motor.Move();
        }

        #endregion
    }
}
