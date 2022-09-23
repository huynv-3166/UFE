using FPLibrary;

namespace TOHDragonFight3D
{
    public class ButtonSequenceRecord
    {
        #region trackable definitions
        public ButtonPress[] buttonPresses;
        public Fix64 chargeTime;
        #endregion

        public ButtonSequenceRecord(ButtonPress[] buttonPresses, Fix64 chargeTime)
        {
            this.buttonPresses = buttonPresses;
            this.chargeTime = chargeTime;
        }
    }
}