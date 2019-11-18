namespace Randomizer.InterfaceAdapters
{
    public class ResetClearViewModel
    {
        public enum ButtonState
        {
            Reset,
            Clear
        }

        public ButtonState State;
        public string Caption => State.ToString();
    }
}