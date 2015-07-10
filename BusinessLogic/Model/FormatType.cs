namespace BusinessLogic
{
    public class FormatType
    {
        public FormatType(string displayName, FormatTypeEnum internalValue)
        {
            DisplayName = displayName;
            InternalValue = internalValue;
        }

        public enum FormatTypeEnum
        {
            SjPrio,
            Swedbank,
            Seb,
            Coop,
            ICA,
            SwedbankOld,
            Peugeot
        }

        public string DisplayName { get; set; }
        public FormatTypeEnum InternalValue { get; set; }
    }
}